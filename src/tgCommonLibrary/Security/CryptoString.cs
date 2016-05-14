using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace TreeGecko.Library.Common.Security
{
    /// <summary>
    /// Summary description for CryptoString.
    /// </summary>
    public class CryptoString
    {
        private byte[] m_SavedKey;
        private byte[] m_SavedIV;

        public CryptoString()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public CryptoString(
            string _key,
            string _iv)
        {
            Key = _key;
            IV = _iv;
        }

        public string Key
        {
            get
            {
                if (m_SavedKey != null)
                {
                    return Convert.ToBase64String(m_SavedKey);
                }

                return null;
            }

            set
            {
                if (value != null)
                {
                    m_SavedKey = Convert.FromBase64String(value);
                }
                else
                {
                    m_SavedKey = null;
                }
            }
        }

        public string IV
        {
            get
            {
                if (m_SavedIV != null)
                {
                    return Convert.ToBase64String(m_SavedIV);
                }

                return null;
            }

            set
            {
                if (value != null)
                {
                    m_SavedIV = Convert.FromBase64String(value);
                }
                else
                {
                    m_SavedIV = null;
                }
            }
        }

        private void RdGenerateSecretKey(RijndaelManaged _provider)
        {
            if (m_SavedKey == null)
            {
                _provider.KeySize = 256;
                _provider.GenerateKey();
                m_SavedKey = _provider.Key;
            }
        }

        private void RdGenerateSecretInitVector(RijndaelManaged _provider)
        {
            if (m_SavedIV == null)
            {
                _provider.GenerateIV();
                m_SavedIV = _provider.IV;
            }
        }

        public string Encrypt(string _originalString)
        {
            if ((m_SavedKey == null) || (m_SavedIV == null))
            {
                throw new Exception("savedKey and savedIV must be non-null.");
            }

            byte[] originalStringAsBytes = Encoding.ASCII.GetBytes(_originalString);

            MemoryStream ms = new MemoryStream(originalStringAsBytes.Length);
            RijndaelManaged provider = new RijndaelManaged();

            RdGenerateSecretKey(provider);
            RdGenerateSecretInitVector(provider);

            ICryptoTransform transform = provider.CreateEncryptor(
                (byte[]) m_SavedKey.Clone(),
                (byte[]) m_SavedIV.Clone());

            CryptoStream cs = new CryptoStream(ms, transform, CryptoStreamMode.Write);

            //Write encrypted data to the memory stream
            cs.Write(originalStringAsBytes, 0, originalStringAsBytes.Length);
            cs.FlushFinalBlock();
            byte[] originalBytes = ms.ToArray();

            //Release all resources
            ms.Close();
            cs.Close();
            transform.Dispose();
            provider.Clear();

            //Convert encrypted string
            return Convert.ToBase64String(originalBytes);
        }

        public string Decrypt(string _encryptedString)
        {
            byte[] encryptedStringAsBytes = Convert.FromBase64String(_encryptedString);
            byte[] initialText = new byte[encryptedStringAsBytes.Length];

            RijndaelManaged provider = new RijndaelManaged();
            MemoryStream ms = new MemoryStream(encryptedStringAsBytes);

            if ((m_SavedKey == null) || (m_SavedIV == null))
            {
                throw new Exception("savedKey and savedIV must be non-null.");
            }

            ICryptoTransform transform = provider.CreateDecryptor(
                (byte[]) m_SavedKey.Clone(),
                (byte[]) m_SavedIV.Clone());

            CryptoStream cs = new CryptoStream(ms, transform, CryptoStreamMode.Read);

            cs.Read(initialText, 0, initialText.Length);

            ms.Close();
            cs.Close();
            transform.Dispose();
            provider.Clear();

            string temp = Encoding.ASCII.GetString(initialText);
            return temp.Trim("\0".ToCharArray());
        }

        public static string MakeKey()
        {
            RijndaelManaged provider = new RijndaelManaged();
            provider.GenerateKey();
            byte[] keyData = provider.Key;

            return Convert.ToBase64String(keyData);
        }

        public static string MakeIV()
        {
            RijndaelManaged provider = new RijndaelManaged();
            provider.GenerateIV();
            byte[] ivData = provider.IV;

            return Convert.ToBase64String(ivData);
        }
    }
}
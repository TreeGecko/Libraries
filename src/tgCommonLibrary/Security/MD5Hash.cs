using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace TreeGecko.Library.Common.Security
{
    /// <summary>
    /// Summary description for RandomString.
    /// </summary>
    public static class MD5Hash
    {
        public static string GetMD5HashFromFile(string _fileName)
        {
            if (File.Exists(_fileName))
            {
                using (MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider())
                {
                    using (FileStream fs = new FileStream(_fileName, FileMode.Open))
                    {
                        byte[] data = md5Hasher.ComputeHash(fs);

                        return ConvertToString(data);
                    }
                }
            }

            return null;
        }

        public static string GetMD5HashFromString(string _input)
        {
            if (_input != null)
            {
                // Create a new instance of the MD5CryptoServiceProvider object.
                using (MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider())
                {
                    // Convert the input string to a byte array and compute the hash. 
                    byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(_input));

                    return ConvertToString(data);
                }
            }

            return null;
        }

        private static string ConvertToString(byte[] _data)
        {
            // Create a new Stringbuilder to collect the bytes 
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data  
            // and format each one as a hexadecimal string. 
            for (int i = 0; i < _data.Length; i++)
            {
                sBuilder.Append(_data[i].ToString("x2"));
            }

            // Return the hexadecimal string. 
            return sBuilder.ToString();
        }
    }
}
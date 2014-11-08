using System;
using System.Security.Cryptography;
using System.Text;
using TreeGecko.Library.Common.Objects;

namespace TreeGecko.Library.Net.Objects
{
    public class TGUserPassword : AbstractTGObject
    {
        public Guid UserGuid { get; set; }
        public string Salt { get; set; }
        public String HashedPassword { get; set; }

        public override TGSerializedObject GetTGSerializedObject()
        {
            TGSerializedObject tgs = base.GetTGSerializedObject();

            tgs.Add("UserGuid", UserGuid);
            tgs.Add("HashedPassword", HashedPassword);
            tgs.Add("Salt", Salt);

            return tgs;
        }

        public override void LoadFromTGSerializedObject(TGSerializedObject _tgs)
        {
            base.LoadFromTGSerializedObject(_tgs);

            UserGuid = _tgs.GetGuid("UserGuid");
            HashedPassword = _tgs.GetString("HashedPassword");
            Salt = _tgs.GetString("Salt");
        }


        public static string GenerateSalt(string _username)
        {
            byte[] saltSalt = Guid.NewGuid().ToByteArray();
            Rfc2898DeriveBytes hasher = new Rfc2898DeriveBytes(_username, saltSalt, 10000);
            return Convert.ToBase64String(hasher.GetBytes(25));
        }

        public static string HashPassword(string _salt, string _plainTextPassword)
        {
            byte[] saltBytes = Encoding.UTF8.GetBytes(_salt);

            Rfc2898DeriveBytes hasher = new Rfc2898DeriveBytes(_plainTextPassword, saltBytes, 10000);
            return Convert.ToBase64String(hasher.GetBytes(25));
        }
    }
}

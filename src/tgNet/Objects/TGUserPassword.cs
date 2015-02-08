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


        public static string GenerateSalt(string _username, int _size = 25)
        {
            byte[] saltSalt = Guid.NewGuid().ToByteArray();
            Rfc2898DeriveBytes hasher = new Rfc2898DeriveBytes(_username, saltSalt, 10000);
            return Convert.ToBase64String(hasher.GetBytes(_size));
        }

        public static string HashPassword(string _salt, string _plainTextPassword, int _size = 25)
        {
            byte[] saltBytes = Encoding.UTF8.GetBytes(_salt);

            Rfc2898DeriveBytes hasher = new Rfc2898DeriveBytes(_plainTextPassword, saltBytes, 10000);
            return Convert.ToBase64String(hasher.GetBytes(_size));
        }

        public static TGUserPassword GetNew(Guid _userGuid, string _userName, string _password)
        {
            TGUserPassword userPassword = new TGUserPassword
            {
                Guid = _userGuid,
                UserGuid = _userGuid, 
                Salt = GenerateSalt(_userName)
            };
            userPassword.HashedPassword = HashPassword(userPassword.Salt, _password);

            return userPassword;
        } 
    }
}

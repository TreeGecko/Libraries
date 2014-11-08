using System;
using TreeGecko.Library.Common.Objects;
using TreeGecko.Library.Common.Security;

namespace TreeGecko.Library.Net.Objects
{
    public class TGUserAuthorization : AbstractTGObject
    {
        /// <summary>
        /// 
        /// </summary>
        public string AuthorizationToken { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime AuthorizationDateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime ExpirationDateTime { get; private set; }

        public bool IsExpired()
        {
            if (ExpirationDateTime.ToUniversalTime() > DateTime.UtcNow)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        public string DeviceType { get; set; }

        public static TGUserAuthorization GetNew(Guid _userGuid, string _deviceType)
        {
            TGUserAuthorization authorization = new TGUserAuthorization
            {
                AuthorizationDateTime = DateTime.UtcNow,
                DeviceType = _deviceType,
                ParentGuid = _userGuid,
                AuthorizationToken = GetToken()
            };
            authorization.UpdateExpirationDate();
            return authorization;
        }

        public bool ValidateAuthorizationToken(string _testToken)
        {
            if (_testToken != null
                && AuthorizationToken != null
                && AuthorizationToken.Equals(_testToken))
            {
                return true;
            }

            return false;
        }

        public override TGSerializedObject GetTGSerializedObject()
        {
            TGSerializedObject tgs = base.GetTGSerializedObject();

            tgs.Add("AuthorizationToken", AuthorizationToken);
            tgs.Add("AuthorizationDateTime", AuthorizationDateTime);
            tgs.Add("ExpirationDateTime", ExpirationDateTime);
            tgs.Add("DeviceType", DeviceType);
            
            return tgs;
        }

        public override void LoadFromTGSerializedObject(TGSerializedObject _tgs)
        {
            base.LoadFromTGSerializedObject(_tgs);

            AuthorizationToken = _tgs.GetString("AuthorizationToken");
            AuthorizationDateTime = _tgs.GetDateTime("AuthorizationDateTime");
            ExpirationDateTime = _tgs.GetDateTime("ExpirationDateTime");
            DeviceType = _tgs.GetString("DeviceType");
        }

        public static string GetToken()
        {
            string token = RandomString.GetRandomString(64);

            return token;
        }

        public void UpdateExpirationDate()
        {
            ExpirationDateTime = DateTime.UtcNow.AddDays(30);
        }
    }
}

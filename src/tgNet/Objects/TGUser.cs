using TreeGecko.Library.Common.Attributes;
using TreeGecko.Library.Common.Objects;

namespace TreeGecko.Library.Net.Objects
{
    public class TGUser : AbstractTGObject
    {
        /// <summary>
        /// 
        /// </summary>
        [Identifier]
        public string Username { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string EmailAddress { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string GivenName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string FamilyName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsVerified { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool EulaAccepted { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override TGSerializedObject GetTGSerializedObject()
        {
            TGSerializedObject obj = base.GetTGSerializedObject();

            obj.Add("Username", Username);
            obj.Add("EmailAddress", EmailAddress);
            obj.Add("FamilyName", FamilyName);
            obj.Add("GivenName", GivenName);
            obj.Add("IsVerified", IsVerified);
            obj.Add("DisplayName", DisplayName);
            obj.Add("EulaAccepted", EulaAccepted);

            return obj;
        }

        public override void LoadFromTGSerializedObject(TGSerializedObject _tgs)
        {
            base.LoadFromTGSerializedObject(_tgs);

            Username = _tgs.GetString("Username");
            EmailAddress = _tgs.GetString("EmailAddress");
            FamilyName = _tgs.GetString("FamilyName");
            GivenName = _tgs.GetString("GivenName");
            IsVerified = _tgs.GetBoolean("IsVerified");
            DisplayName = _tgs.GetString("DisplayName");
            EulaAccepted = _tgs.GetBoolean("EulaAccepted");
        }
    }
}

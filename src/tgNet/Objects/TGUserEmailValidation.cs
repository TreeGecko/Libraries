using System;
using TreeGecko.Library.Common.Objects;

namespace TreeGecko.Library.Net.Objects
{
    /// <summary>
    /// 
    /// </summary>
    public class TGUserEmailValidation : AbstractTGObject
    {
        public string EmailAddress { get; set; }
        public string ValidationText { get; set; }

        public TGUserEmailValidation()
        {
            ValidationText = GetValidationText();
        }

        public TGUserEmailValidation(TGUser _user)
        {
            ParentGuid = _user.Guid;
            EmailAddress = _user.EmailAddress;
            ValidationText = GetValidationText();
        }
        
        public static string GetValidationText()
        {
            Guid guid1 = Guid.NewGuid();
            Guid guid2 = Guid.NewGuid();

            return guid1.ToString("N") + guid2.ToString("N");
        }
        
        

        public override TGSerializedObject GetTGSerializedObject()
        {
            TGSerializedObject tgs = base.GetTGSerializedObject();
            tgs.Add("ValidationText", ValidationText);
            tgs.Add("EmailAddress", EmailAddress);

            return tgs;
        }


        public override void LoadFromTGSerializedObject(TGSerializedObject _tgs)
        {
            base.LoadFromTGSerializedObject(_tgs);

            ValidationText = _tgs.GetString("ValidationText");
        }
    }
}

using System;
using TreeGecko.Library.Common.Objects;

namespace TreeGecko.Library.Net.Objects
{
    public class TGEulaAgreement : AbstractTGObject
    {
        /// <summary>
        /// 
        /// </summary>
        public Guid UserGuid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid EulaGuid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime AgreementDateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override TGSerializedObject GetTGSerializedObject()
        {
            TGSerializedObject tgs = base.GetTGSerializedObject();

            tgs.Add("UserGuid", UserGuid);
            tgs.Add("EulaGuid", EulaGuid);
            tgs.Add("AgreementDateTime", AgreementDateTime);

            return tgs;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_tgs"></param>
        public override void LoadFromTGSerializedObject(TGSerializedObject _tgs)
        {
            base.LoadFromTGSerializedObject(_tgs);

            UserGuid = _tgs.GetGuid("UserGuid");
            EulaGuid = _tgs.GetGuid("EulaGuid");
            AgreementDateTime = _tgs.GetDateTime("AgreementDateTime");
        }
    }
}

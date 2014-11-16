using System;
using System.Net;
using System.Xml.Serialization;
using TreeGecko.Library.Common.Objects;
using TreeGecko.Library.Net.Enums;

namespace TreeGecko.Library.Net.Objects
{
    public class CannedEmail : TGEmail
    {
        public string Name { get; set; }
        public string Description { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override TGSerializedObject GetTGSerializedObject()
        {
            TGSerializedObject tgs = base.GetTGSerializedObject();

            tgs.Add("Name", Name);
            tgs.Add("Description", Description);

            return tgs;
        }


        public override void LoadFromTGSerializedObject(TGSerializedObject _tgs)
        {
            base.LoadFromTGSerializedObject(_tgs);

            Name = _tgs.GetString("Name");
            Description = _tgs.GetString("Description");
        }
    }
}

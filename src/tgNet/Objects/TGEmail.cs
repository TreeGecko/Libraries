using System;
using System.Collections.Generic;
using TreeGecko.Library.Common.Helpers;
using TreeGecko.Library.Common.Objects;
using TreeGecko.Library.Net.Enums;

namespace TreeGecko.Library.Net.Objects
{
    public class TGEmail : AbstractTGObject
    {
        /// <summary>
        /// 
        /// </summary>`
        public string From { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<string> To { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ReplyTo { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// html or text
        /// </summary>
        public EmailBodyType BodyType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override TGSerializedObject GetTGSerializedObject()
        {
            TGSerializedObject tgs = new TGSerializedObject();
            
            tgs.Add("From", From);
            tgs.Add("To", To);
            tgs.Add("ReplyTo", ReplyTo);
            tgs.Add("Body", Body);
            tgs.Add("Subject", Subject);
            tgs.Add("BodyType", Enum.GetName(typeof(EmailBodyType), BodyType));

            return tgs;
        }

        public override void LoadFromTGSerializedObject(TGSerializedObject _tgs)
        {
            From = _tgs.GetString("From");
            To = _tgs.GetListOfStrings("To");
            ReplyTo = _tgs.GetString("ReplyTo");
            Body = _tgs.GetString("Body");
            Subject = _tgs.GetString("Subject");
            BodyType = (EmailBodyType)Enum.Parse(typeof(EmailBodyType), _tgs.GetString("BodyType"));
        }

        /// <summary>
        /// 
        /// </summary>
        public string BCNObjectType
        {
            get { return ReflectionHelper.GetTypeName(GetType()); }
        }
    }
}

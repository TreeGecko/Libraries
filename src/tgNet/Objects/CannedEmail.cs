using System;
using System.Net;
using System.Xml.Serialization;
using TreeGecko.Library.Common.Objects;
using TreeGecko.Library.Net.Enums;

namespace TreeGecko.Library.Net.Objects
{
    public class CannedEmail : AbstractTGObject
    {
        private string m_From;
        private string m_ReplyTo;
        private string m_To;
        private string m_Subject;
        private string m_Body;

        [XmlElement]
        public string From
        {
            get
            {
                return WebUtility.HtmlDecode(m_From);
            }
            set
            {
                m_From = WebUtility.HtmlEncode(value);
            }
        }

        [XmlElement]
        public string To
        {
            get
            {
                return WebUtility.HtmlDecode(m_To);
            }
            set
            {
                m_To = WebUtility.HtmlEncode(value);
            }
        }

        [XmlElement]
        public string ReplyTo
        {
            get
            {
                return WebUtility.HtmlDecode(m_ReplyTo);
            }
            set
            {
                m_ReplyTo = WebUtility.HtmlEncode(value);
            }
        }

        [XmlElement]
        public string Subject
        {
            get
            {
                return WebUtility.HtmlDecode(m_Subject);
            }
            set
            {
                m_Subject = WebUtility.HtmlEncode(value);
            }
        }

        [XmlElement]
        public string Body
        {
            get
            {
                return WebUtility.HtmlDecode(m_Body);
            }
            set
            {
                m_Body = WebUtility.HtmlEncode(value);
            }
        }

        [XmlElement]
        public EmailBodyType BodyType { get; set; }
        

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override TGSerializedObject GetTGSerializedObject()
        {
            TGSerializedObject tgs = base.GetTGSerializedObject();
            

            tgs.Add("From", m_From);
            tgs.Add("To", m_To);
            tgs.Add("Body", m_Body);
            tgs.Add("Subject", m_Subject);
            tgs.Add("ReplyTo", m_ReplyTo);
            tgs.Add("BodyType", Enum.GetName(typeof(EmailBodyType), BodyType));

            return tgs;
        }


        public override void LoadFromTGSerializedObject(TGSerializedObject _tgs)
        {
            base.LoadFromTGSerializedObject(_tgs);

            m_From = _tgs.GetString("From");
            m_To = _tgs.GetString("To");
            m_Body = _tgs.GetString("Body");
            m_Subject = _tgs.GetString("Subject");
            m_ReplyTo = _tgs.GetString("ReplyTo");
            BodyType = (EmailBodyType)Enum.Parse(typeof(EmailBodyType), _tgs.GetString("BodyType"));
        }
    }
}

using System;
using TreeGecko.Library.Common.Objects;
using TreeGecko.Library.Net.Enums;

namespace TreeGecko.Library.Net.Objects
{
    public class WebLogEntry : AbstractTGObject
    {
        /// <summary>
        /// 
        /// </summary>
        public string SourceApplication { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public LogMessageType WebLogType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid UserGuid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime MessageDateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override TGSerializedObject GetTGSerializedObject()
        {
            TGSerializedObject tgs = base.GetTGSerializedObject();

            tgs.Add("WebLogType", (Int32)WebLogType);
            tgs.Add("Message", Message);
            tgs.Add("UserGuid", UserGuid);
            tgs.Add("MessageDateTime", MessageDateTime);
            tgs.Add("SourceApplication", SourceApplication);
            tgs.Add("MessageTicks", MessageDateTime.Ticks);

            return tgs;
        }

        public override void LoadFromTGSerializedObject(TGSerializedObject _tgs)
        {
            base.LoadFromTGSerializedObject(_tgs);
        
            WebLogType = (LogMessageType) _tgs.GetInt32("WebLogType");
            Message = _tgs.GetString("Message");
            UserGuid = _tgs.GetGuid("UserGuid");
            MessageDateTime = _tgs.GetDateTime("MessageDateTime");
            SourceApplication = _tgs.GetString("SourceApplication");
            MessageDateTime = new DateTime(_tgs.GetInt64("MessageTicks"));
        }



        public override string ToString()
        {
            TGSerializedObject obj = GetTGSerializedObject();

            return obj.ToString();
        }
    }
}

using System.Collections.Generic;
using System.Diagnostics;
using System.Timers;
using RestSharp;
using TreeGecko.Library.Common.Helpers;
using TreeGecko.Library.Loggly.Objects;

namespace TreeGecko.Library.Loggly.TraceListeners
{
    public class LogglyTraceListener : TraceListener
    {
        private readonly object m_Lock = new object();
        private readonly List<string> m_Message = new List<string>();
        private readonly Timer m_Timer;

        private string LogglyHost { get; set; }
        private string LogglyResource { get; set; }
        private string LogglyToken { get; set; }

        private int FlushEveryMilliseconds { get; set; }

        public LogglyTraceListener()
        {
            LogglyHost = Config.GetSettingValue("LogglyHost", "http://logs-01.loggly.com");
            LogglyResource = Config.GetSettingValue("LogglyResource", "/inputs/{token}/tag/http/");
            LogglyToken = Config.GetSettingValue("LogglyToken");
            FlushEveryMilliseconds = Config.GetIntValue("LogglyFlush", 10000);

            m_Timer = new Timer(FlushEveryMilliseconds);
            m_Timer.Elapsed += Timer_Elapsed;
            m_Timer.AutoReset = false;
            m_Timer.Start();
        }

        void Timer_Elapsed(object _sender, ElapsedEventArgs e)
        {
            string[] messages = null;

            lock (m_Lock)
            {
                if (m_Message.Count > 0)
                {
                    messages = m_Message.ToArray();
                    m_Message.Clear();
                }
            }

            if (messages != null)
            {
                RestClient client = new RestClient(LogglyHost);

                foreach (string stringMessage in messages)
                {
                    try
                    {
                        var lm = new LogglyMessage
                        {
                            message = stringMessage
                        };
                        
                        var request = new RestRequest(LogglyResource, Method.POST);
                        request.AddUrlSegment("token", LogglyToken);

                        // easily add HTTP Headers
                        request.AddHeader("content-type", "application/x-www-form-urlencoded");

                        request.AddJsonBody(lm);
                        client.Execute(request);
                    }
                    catch
                    {
                        //Eat the error
                    }

                }
            }

            m_Timer.Start();
        }

        public override void Write(string _message)
        {
            lock (m_Lock)
            {
                m_Message.Add(_message);
            }
        }

        public override void WriteLine(string _message)
        {
            lock (m_Lock)
            {
                m_Message.Add(_message);
            }
        }

        protected override void Dispose(bool _disposing)
        {
            base.Dispose(_disposing);

            m_Timer.Dispose();
        }
    }
}

using System;
using System.Diagnostics;
using System.Text;
using TreeGecko.Library.Common.Tracing;
using TraceLevel = TreeGecko.Library.Common.Enums.TraceLevel;


namespace TreeGecko.Library.Common.Helpers
{
	/// <summary>
	/// Summary description for TraceFileHelper.
	/// </summary>
    /// 
	public static class TraceFileHelper
	{
		
        private static TraceLevel m_MinTraceLevel = TraceLevel.Verbose;
		private static string m_TraceFolder = "";

        public static void FlushMessages()
        {
            Trace.Flush();
        }

        public static string TraceFolder
        {
            get { return m_TraceFolder; }
            set { m_TraceFolder = value; }
        }

        public static TraceLevel MinTraceLevel
        {
            get { return m_MinTraceLevel; }
            set { m_MinTraceLevel = value; }
        }
		
        /// <summary>
        /// Write out Error-level message
        /// </summary>
        /// <param name="_message">Message to write</param>
        public static void Error(string _message)
        { WriteLineLevel(_message, "ERR", TraceLevel.Error); }

        /// <summary>
        /// Write out Warning-level message
        /// </summary>
        /// <param name="_message">Message to write</param>
        public static void Warning(string _message)
        { WriteLineLevel(_message, "WRN", TraceLevel.Warning); }

        /// <summary>
        /// Write out Exception-level message
        /// </summary>
        /// <param name="_message">Message to write</param>
        public static void Exception(string _message)
        { 
            WriteLineLevel(_message, "EXP", TraceLevel.Error); 
        }

        public static void Exception(Exception _ex)
        {
            WriteLineLevel(_ex.ToString(), "EXP", TraceLevel.Error);
        }

        /// <summary>
        /// Write out Info-level message
        /// </summary>
        /// <param name="_message">Message to write</param>
        public static void Info(string _message)
        { 
            WriteLineLevel(_message, "INF", TraceLevel.Info); 
        }

        /// <summary>
        /// Write out Verbose-level message
        /// </summary>
        /// <param name="_message">Message to write</param>
        public static void Verbose(string _message)
        { 
            WriteLineLevel(_message, "VER", TraceLevel.Verbose); 
        }
        
        private static void WriteLineLevel(string _message, 
            string _category, TraceLevel _traceLevel)
        {

            //Don't log if current TraceLevel too low
            if (_traceLevel > m_MinTraceLevel)
                return;

            if (_message == "")
            { 
				Trace.WriteLine("");  
				return; 
			}

            StringBuilder sb = new StringBuilder();

            sb.Append(DateTime.Now.ToString("s"));
            sb.Append(" ");

            if (_message.Length < 32000)
            {
                sb.Append(_message);
            }
            else
            {
                sb.Append("Partial Message - ");
                sb.Append(_message.Substring(0, 32000));
            }
			
            Trace.WriteLine(sb.ToString(), _category);
        }
    
        public static void SetupLogging()
        {
            m_TraceFolder = Config.GetSettingValue("LogFolder", @"\logs\");
            RollingTraceLogListener rtll = new RollingTraceLogListener("RollingTraceLogListener", m_TraceFolder);
            Trace.Listeners.Add(rtll);
        }

        public static void TearDownLogging()
        {
            Trace.Listeners.Clear();
        }

     }
}

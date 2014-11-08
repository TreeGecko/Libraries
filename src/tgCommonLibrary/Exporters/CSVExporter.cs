using System.Collections.Generic;
using System.IO;
using System.Text;
using TreeGecko.Library.Common.Interfaces;
using TreeGecko.Library.Common.Objects;

namespace TreeGecko.Library.Common.Exporters
{
    /// <summary>
    /// 
    /// </summary>
    public class CSVExporter : IExporter
    {
        public bool IncludeHeaderRow { get; set; }
        public string Delimiter { get; set; }
        public bool EncloseValuesInQuotes { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_fileName"></param>
        /// <param name="_items"></param>
        public void Export(string _fileName, List<TGSerializedObject> _items)
        {
            if (File.Exists(_fileName))
            {
                File.Delete(_fileName);
            }

            using (FileStream fs = new FileStream(_fileName, FileMode.OpenOrCreate))
            {
                using (StreamWriter writer = new StreamWriter(fs))
                {
                    if (_items != null
                        && _items.Count>0)
                    {
                        if (IncludeHeaderRow)
                        {
                            string header = GetHeader(_items[0]);
                            writer.WriteLine(header);
                        }

                        foreach (TGSerializedObject item in _items)
                        {
                            string row = GetRow(item);
                            writer.WriteLine(row);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_fileName"></param>
        /// <param name="_items"></param>
        public void Export(string _fileName, List<ITGSerializable> _items)
        {
            List<TGSerializedObject> bcns = new List<TGSerializedObject>();

            foreach (ITGSerializable item in _items)
            {
                bcns.Add(item.GetTGSerializedObject());
            }

            Export(_fileName, bcns);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_tg"></param>
        /// <returns></returns>
        private string GetRow(TGSerializedObject _tg)
        {
            StringBuilder sb = new StringBuilder();

            foreach (string key in _tg.Properties.Keys)
            {
                TGSerializedProperty property = _tg.Properties[key];

                if (EncloseValuesInQuotes)
                {
                    sb.AppendFormat(@"""{0}""", property.SerializedValue);
                }
                else
                {
                    sb.AppendFormat(@"{0}", property.SerializedValue);
                }

                sb.Append(Delimiter);
            }

            //Remove the final delimiter
            if (sb.Length > 0)
            {
                sb.Remove(sb.Length - Delimiter.Length, Delimiter.Length);
            }

            return sb.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_tg"></param>
        /// <returns></returns>
        private string GetHeader(TGSerializedObject _tg)
        {
            StringBuilder sb = new StringBuilder();

            foreach (string key in _tg.Properties.Keys)
            {
                if (EncloseValuesInQuotes)
                {
                    sb.AppendFormat(@"""{0}""", key);
                }
                else
                {
                    sb.AppendFormat(@"{0}", key);
                }

                sb.Append(Delimiter);
            }

            //Remove the final comma
            if (sb.Length > 0)
            {
                sb.Remove(sb.Length - Delimiter.Length, Delimiter.Length);
            }

            return sb.ToString();
        }
    }

}
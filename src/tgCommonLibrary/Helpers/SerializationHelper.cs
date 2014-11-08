using System;
using System.IO;
using System.Xml.Serialization;

namespace TreeGecko.Library.Common.Helpers
{

    public class SerializationHelper
    {
        public static string SerializeObject<T>(T _input)
        {
            XmlSerializer ser = new XmlSerializer(typeof(T));

            MemoryStream ms = new MemoryStream();
            ser.Serialize(ms, _input);

            long len = ms.Length;
            byte[] data = new byte[len];
            ms.Position = 0;
            ms.Read(data, 0, Convert.ToInt32(len));

            return System.Text.Encoding.ASCII.GetString(data);
        }
        public static string SerializeObject(Type _inputType, object _input)
        {
            XmlSerializer ser = new XmlSerializer(_inputType);

            MemoryStream ms = new MemoryStream();
            ser.Serialize(ms, _input);

            long len = ms.Length;
            byte[] data = new byte[len];
            ms.Position = 0;
            ms.Read(data, 0, Convert.ToInt32(len));

            return System.Text.Encoding.ASCII.GetString(data);
        }
        public static object DeserializeObject(Type _outputType, string _xml)
        {
            XmlSerializer ser = new XmlSerializer(_outputType);
            MemoryStream ms = null;

            Object output = null;

            try
            {
                byte[] arr = System.Text.Encoding.ASCII.GetBytes(_xml);

                ms = new MemoryStream(arr, false);

                output = ser.Deserialize(ms);
            }
            catch (Exception ex)
            {
                TraceFileHelper.Exception(ex.ToString());
            }
            finally
            {
                if (ms != null)
                {
                    ms.Close();
                }
            }

            return output;
        }
        public static object DeserializeObject(Type _outputType, Stream _data)
        {
            XmlSerializer ser = new XmlSerializer(_outputType);

            Object output = null;

            try
            {
                output = ser.Deserialize(_data);
            }
            catch (Exception ex)
            {
                TraceFileHelper.Exception(ex.ToString());
            }
            finally
            {
                // Don't close stream here the calling method is responsible
            }

            return output;
        }

        public static T DeserializeObject<T>(string _xml)
        {
            XmlSerializer ser = new XmlSerializer(typeof(T));
            MemoryStream ms = null;

            T output = default(T);

            try
            {
                byte[] arr = System.Text.Encoding.ASCII.GetBytes(_xml);

                ms = new MemoryStream(arr, false);

                output = (T)ser.Deserialize(ms);
            }
            catch (Exception ex)
            {
                TraceFileHelper.Exception(ex.ToString());
            }
            finally
            {
                if (ms != null)
                {
                    ms.Close();
                }
            }

            return output;
        }

    }
}

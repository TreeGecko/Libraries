using System;
using System.Text;

namespace TreeGecko.Library.Common.Helpers
{
    public class StringHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_delimiter"></param>
        /// <param name="_value"></param>
        /// <returns></returns>
        public static string[] GetArray(string _delimiter, string _value)
        {
            return _value.Split(_delimiter.ToCharArray());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_value"></param>
        /// <returns></returns>
        public static string GetBase64(string _value)
        {
            byte[] temp;

            if (_value == null)
            {
                temp = Encoding.UTF8.GetBytes("");
            }
            else
            {
                temp = Encoding.UTF8.GetBytes(_value);
            }

            return Convert.ToBase64String(temp);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_base64Value"></param>
        /// <returns></returns>
        public static string GetStringFromBase64(string _base64Value)
        {
            byte[] temp = Convert.FromBase64String(_base64Value);

            return Encoding.UTF8.GetString(temp);
        }
    }
}
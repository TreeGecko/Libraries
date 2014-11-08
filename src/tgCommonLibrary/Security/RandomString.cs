using System;
using System.Security.Cryptography;
using System.Text;

namespace TreeGecko.Library.Common.Security
{
	/// <summary>
	/// Summary description for RandomString.
	/// </summary>
	public static class RandomString
	{
        //modified Base64 for regexps 
        private static readonly string[] Characters =
        {"A", "B", "C", "D", "E", "F", "G", "H", "I", "J", 
            "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", 
            "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d",
            "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", 
            "o", "p", "q", "r", "s", "t", "u", "v", "w", "x",
            "y", "z", "1", "2", "3", "4", "5", "6", "7", "8",
            "9", "0", "!", "-"};

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_length"></param>
        /// <returns></returns>
		public static string GetRandomString(int _length)
		{
            StringBuilder sb = new StringBuilder();

            Byte[] array = GetRandomArray(_length);

            for (int i = 0; i < _length; i++)
            {
                int b = array[i] % 64;

                sb.Append(Characters[b]);
            }

            return sb.ToString();            
		}

       

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_length"></param>
        /// <returns></returns>
		private static byte[] GetRandomArray(long _length)
		{
			byte[] arr = new byte[_length];

			RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            
			rng.GetBytes(arr);

			return arr;
		}
	}
}

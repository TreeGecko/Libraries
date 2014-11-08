using System.Configuration;

namespace TreeGecko.Library.Common.Helpers
{
    public static class Config
    {
        public static string GetSettingValue(string _name)
        {
            return ConfigurationManager.AppSettings.Get(_name);
		}

        public static string GetSettingValue(string _name, string _defaultValue)
        {			
            string result = ConfigurationManager.AppSettings.Get(_name);

            if (result == null)
            {
                return _defaultValue;
            }

            return result;		
        }

        public static int GetIntValue(string _name, int _defaultValue)
        {
            string temp = ConfigurationManager.AppSettings.Get(_name);

            if (temp == null)
            {
                return _defaultValue;
            }

            int output;
            int.TryParse(temp, out output);

            return output;
        }

        public static bool GetBooleanValue(string _name, bool _defaultValue)
        {
            string temp = ConfigurationManager.AppSettings.Get(_name);

            if (temp == null)
            {
                return _defaultValue;
            }

            bool output;
            bool.TryParse(temp, out output);

            return output;
        }
    }
}

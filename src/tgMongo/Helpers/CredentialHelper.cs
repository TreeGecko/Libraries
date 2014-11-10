using System;
using System.Collections.Generic;
using TreeGecko.Library.Common.Helpers;
using TreeGecko.Library.Mongo.Objects;

namespace TreeGecko.Library.Mongo.Helpers
{
    public static class CredentialHelper
    {
        public static DBCredentials GetCredentials(string _name)
        {
            DBCredentials credentials = new DBCredentials();

            string[] tempServers = Config.GetSettingValue(_name + "_DBServers", "localhost")
                                         .Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            credentials.Servers = new List<string>(tempServers);

            credentials.Port = Config.GetIntValue(_name + "_DBPort", 0);
            credentials.DBName = Config.GetSettingValue(_name + "_DBName");
            credentials.AuthenticationDBName = Config.GetSettingValue(_name + "_AuthDBName", "admin");

            credentials.UseServiceCredentials = Config.GetBooleanValue(_name + "_UseServiceCredentials", true);

            //TODO - Replace windows Authentication
            //Can't store clear text passwords.
            credentials.UserName = Config.GetSettingValue(_name + "_DBUser");
            credentials.Password = Config.GetSettingValue(_name + "_DBPassword");

            return credentials;
        }
    }
}

using System.Collections.Generic;

namespace TreeGecko.Library.Mongo.Objects
{
    public class DBCredentials
    {
        public string Name { get; set; }

        public IEnumerable<string> Servers { get; set; }

        public int Port { get; set; }

        public string DBName { get; set; }

        public bool UseServiceCredentials { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }
    }
}

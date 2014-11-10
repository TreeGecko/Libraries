using System.Collections.Generic;
using MongoDB.Driver;
using TreeGecko.Library.Mongo.Helpers;
using TreeGecko.Library.Mongo.Objects;

namespace TreeGecko.Library.Mongo.Managers
{
    public abstract class AbstractMongoManager
    {
        private MongoClient m_MongoClient;
        private MongoServer m_MongoServer;

        protected MongoDatabase MongoDB { get; private set; }

        protected AbstractMongoManager(MongoClient _mongoClient, string _database)
        {
            m_MongoClient = _mongoClient;
            m_MongoServer = m_MongoClient.GetServer();
            MongoDB = m_MongoServer.GetDatabase(_database);
        }

        protected AbstractMongoManager(MongoDatabase _mongoDB)
        {
            MongoDB = _mongoDB;
        }

        protected AbstractMongoManager(string _name)
        {
            DBCredentials credentials = CredentialHelper.GetCredentials(_name);

            Initialize(credentials);
        }

        protected AbstractMongoManager(DBCredentials _credentials)
        {
            Initialize(_credentials);
        }

        private void Initialize(DBCredentials _credentials)
        {
            if (_credentials.Port <= 0)
            {
                _credentials.Port = 27017;
            }

            //TODO - Fix so uses ServiceCredentials
            MongoCredential credential = MongoCredential.CreateMongoCRCredential(_credentials.AuthenticationDBName,
                                                                                 _credentials.UserName,
                                                                                 _credentials.Password);

            List<MongoServerAddress> serverAddresses = new List<MongoServerAddress>();
            
            foreach (string serverName in _credentials.Servers)
            {
                MongoServerAddress msa = new MongoServerAddress(serverName, _credentials.Port);
                serverAddresses.Add(msa);
            }

            MongoClientSettings mcs = new MongoClientSettings
            {
                Servers = serverAddresses,
                Credentials = new[] { credential }
            };

            m_MongoClient = new MongoClient(mcs);
            m_MongoServer = m_MongoClient.GetServer();
            MongoDB = m_MongoServer.GetDatabase(_credentials.DBName);
        }


        protected AbstractMongoManager(IEnumerable<string> _servers,
                                       int _port,
                                       string _userName,
                                       string _password,
                                       string _database)
        {
            DBCredentials credentials = new DBCredentials
                {
                    Servers = _servers,
                    Port = _port,
                    UserName = _userName,
                    Password = _password,
                    DBName = _database
                };

            Initialize(credentials);

        }
    }
}

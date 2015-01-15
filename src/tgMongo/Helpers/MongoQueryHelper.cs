using System;
using System.Collections.Specialized;
using MongoDB.Bson;
using MongoDB.Driver;

namespace TreeGecko.Library.Mongo.Helpers
{
    public static class MongoQueryHelper
    {
        /// <summary>
        /// Returns a basic string query with equal operators
        /// </summary>
        /// <param name="_name"></param>
        /// <param name="_value"></param>
        /// <returns></returns>
        public static IMongoQuery GetQuery(string _name, string _value)
        {
            BsonValue bsonValue = new BsonString(_value);

            IMongoQuery query = new QueryDocument(_name, bsonValue);
            return query;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static IMongoQuery GetQuery()
        {
            IMongoQuery query = new QueryDocument();
            return query;
        }



        /// <summary>
        /// Returns a basic string query with equals operators
        /// </summary>
        /// <param name="_nameValueCollection"></param>
        /// <returns></returns>
        public static IMongoQuery GetQuery(NameValueCollection _nameValueCollection)
        {
            if (_nameValueCollection != null)
            {
                QueryDocument query = new QueryDocument();

                foreach (string key in _nameValueCollection)
                {
                    BsonValue value = new BsonString(_nameValueCollection[key]);

                    query.Add(key, value);
                }

                return query;
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_guid"></param>
        /// <returns></returns>
        public static IMongoQuery GetIdQuery(Guid _guid)
        {
            IMongoQuery query = new QueryDocument("_id", new BsonBinaryData(_guid.ToByteArray()));

            return query;
        }
    }
}

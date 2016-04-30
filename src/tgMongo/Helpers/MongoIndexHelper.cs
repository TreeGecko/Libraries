using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace TreeGecko.Library.Mongo.Helpers
{
    public static class MongoIndexHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_columns"></param>
        /// <returns></returns>
        public static IMongoIndexKeys GetIndexKeys(IEnumerable<string> _columns)
        {
            string[] columns = _columns.ToArray();
            IndexKeysBuilder ikb = new IndexKeysBuilder();
            ikb.Ascending(columns);

            return ikb;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_indexName"></param>
        /// <param name="_isUnique"></param>
        /// <param name="_isSparce"></param>
        /// <returns></returns>
        public static IMongoIndexOptions GetIndexOptions(string _indexName, 
            bool _isUnique, bool _isSparce = false)
        {
            IndexOptionsBuilder iob = new IndexOptionsBuilder();

            iob.SetName(_indexName);
            iob.SetUnique(_isUnique);
            iob.SetSparse(_isSparce);

            return iob;
        }

        public static void BuildGeospatialIndex(MongoCollection _table,
                             string _column,
                             string _indexName)
        {
            IndexKeysBuilder ikb = IndexKeys.GeoSpatialSpherical(_column);
            IMongoIndexOptions options = GetIndexOptions(_indexName, false);

            _table.CreateIndex(ikb, options);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="_table"></param>
        /// <param name="_columns"></param>
        /// <param name="_indexName"></param>
        /// <param name="_sparce"></param>
        public static void BuildUniqueIndex(MongoCollection _table,
                                            IEnumerable<string> _columns,
                                            string _indexName,
                                            bool _sparce = false)
        {
            IMongoIndexKeys keys = GetIndexKeys(_columns);
            IMongoIndexOptions options = GetIndexOptions(_indexName, true, _sparce);
            
            _table.CreateIndex(keys, options);
        }


        public static void BuildUniqueIndex(MongoCollection _table,
                                            string _column,
                                            string _indexName)
        {
            BuildUniqueIndex(_table, new[] { _column }, _indexName);
        }

        public static void BuildUniqueSparceIndex(MongoCollection _table,
                                            string _column,
                                            string _indexName)
        {
            BuildUniqueIndex(_table, new[] {_column}, _indexName, true);
        }

        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_table"></param>
        /// <param name="_columns"></param>
        /// <param name="_indexName"></param>
        public static void BuildNonuniqueIndex(MongoCollection _table,
                                               IEnumerable<string> _columns,
                                               string _indexName)
        {
            IMongoIndexKeys keys = GetIndexKeys(_columns);
            IMongoIndexOptions options = GetIndexOptions(_indexName, false);

            _table.CreateIndex(keys, options);
        }

        public static void BuildNonuniqueIndex(MongoCollection _table,
                                            string _column,
                                            string _indexName)
        {
            BuildNonuniqueIndex(_table, new[] { _column }, _indexName);
        }
    }
}

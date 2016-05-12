using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using TreeGecko.Library.Common.Interfaces;
using TreeGecko.Library.Common.Objects;
using TreeGecko.Library.Mongo.Helpers;

namespace TreeGecko.Library.Mongo.DAOs
{
    public abstract class AbstractMongoDAO<T> where T : AbstractTGObject, new()
    {
        private readonly MongoClient m_MongoClient;
        private readonly MongoServer m_MongoServer;
        private readonly MongoDatabase m_MongoDB;

        protected readonly MongoCollection<BsonDocument> MongoCollection;

        //Abstract Properties and Methods
        public abstract string TableName { get; }

        protected virtual void PostPersist(T _object)
        {
            
        }

        public virtual bool UniqueName
        {
            get { return true; }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool HasParent { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public virtual void BuildTable()
        {
            if (HasParent)
            {
                BuildNonuniqueIndex("ParentGuid", "PARENTGUID");
                BuildNonuniqueIndex(new[] { "ParentGuid", "PersistedDateTime" }, "PARENT_PERSISTEDDATETIME");
            }
            else
            {
                BuildNonuniqueIndex("PersistedDateTime", "PERSISTEDDATETIME");
            }
            if (typeof (T).GetInterfaces().Contains(typeof (INamedObject)))
            {
                if (UniqueName)
                {
                    BuildUniqueIndex("Name", "NAME");
                }
                else
                {
                    BuildNonuniqueIndex("Name", "NAME");
                }
            }

            BuildUniqueIndex("Guid", "GUID");
        }

        protected AbstractMongoDAO(MongoClient _mongoClient, string _database)
        {
            HasParent = true;

            m_MongoClient = _mongoClient;
            m_MongoServer = m_MongoClient.GetServer();
            m_MongoDB = m_MongoServer.GetDatabase(_database);
            if (TableName != null)
            {
                MongoCollection = m_MongoDB.GetCollection<BsonDocument>(TableName);
            }
        }

        protected AbstractMongoDAO(MongoDatabase _mongoDB)
        {
            HasParent = true;

            m_MongoDB = _mongoDB;
            if (TableName != null)
            {
                MongoCollection = m_MongoDB.GetCollection<BsonDocument>(TableName);
            }
        }

        protected AbstractMongoDAO(IEnumerable<string> _servers,
                                   int _port,
                                   string _userName,
                                   string _password,
                                   string _database)
        {
            if (_port <= 0)
            {
                _port = 27017;
            }

            MongoCredential credential = MongoCredential.CreateCredential(_database, _userName, _password);

            List<MongoServerAddress> serverAddresses = new List<MongoServerAddress>();
            foreach (string serverName in _servers)
            {
                MongoServerAddress msa = new MongoServerAddress(serverName, _port);
                serverAddresses.Add(msa);
            }

            MongoClientSettings mcs = new MongoClientSettings
            {
                Servers = serverAddresses,
                Credentials = new[] { credential }
            };

            m_MongoClient = new MongoClient(mcs);
            m_MongoServer = m_MongoClient.GetServer();
            m_MongoDB = m_MongoServer.GetDatabase(_database);

            if (TableName != null)
            {
                MongoCollection = m_MongoDB.GetCollection<BsonDocument>(TableName);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_objectIdentifier"></param>
        /// <returns></returns>
        public T Get(Guid _objectIdentifier)
        {
            BsonBinaryData id = new BsonBinaryData(_objectIdentifier.ToByteArray());

            BsonDocument doc = MongoCollection.FindOneById(id);
            
            return BsonHelper.Get<T>(doc);
        }

        public List<T> GetByQuery(Guid _objectParentIdentifier)
        {
            List<T> objRet = new List<T>();
            var query = new QueryDocument { { "ParentGuid", _objectParentIdentifier.ToString() } };

            foreach (BsonDocument doc in MongoCollection.Find(query))
            {
                objRet.Add(BsonHelper.Get<T>(doc));
            }
            
            return objRet;
        }

        public List<T> GetByQuery(Guid _objectParentIdentifier, string[] _objectVersionIdentifiers)
        {
            List<T> objRet = new List<T>();

            foreach (string objectVersionIdentifier in _objectVersionIdentifiers)
            {
                var query = new QueryDocument { { "ParentGuid", _objectParentIdentifier.ToString() }, { "VersionGuid", objectVersionIdentifier } };
                foreach (BsonDocument doc in MongoCollection.Find(query))
                {
                    objRet.Add(BsonHelper.Get<T>(doc));
                }
            }

            return objRet;
        }

        public List<T> GetByQuery(Guid _objectParentIdentifier, Guid _objectDeviceIdentifier)
        {
            List<T> objRet = new List<T>();
            var query = new QueryDocument { { "ParentGuid", _objectParentIdentifier.ToString() }, { "DeviceGuid", _objectDeviceIdentifier.ToString() } };

            foreach (BsonDocument doc in MongoCollection.Find(query))
            {
                objRet.Add(BsonHelper.Get<T>(doc));
            }

            return objRet;
        }

        public List<T> GetByQuery(Guid _objectParentIdentifier, Guid _objectDeviceIdentifier, string[] _objectVersionIdentifiers)
        {
            List<T> objRet = new List<T>();

            foreach (string _objectVersionIdentifier in _objectVersionIdentifiers)
            {
                var query = new QueryDocument { { "ParentGuid", _objectParentIdentifier.ToString() }, { "DeviceGuid", _objectDeviceIdentifier.ToString() }, { "VersionGuid", _objectVersionIdentifier } };
                foreach (BsonDocument doc in MongoCollection.Find(query))
                {
                    objRet.Add(BsonHelper.Get<T>(doc));
                }
            }

            return objRet;
        }

        public MongoCursor<BsonDocument> GetCursor(IMongoQuery _objectIdentifier)
        {
            //string _objectIdentifier.
            //BsonBinaryData id = new BsonBinaryData(_objectIdentifier.ToByteArray());

            MongoCursor<BsonDocument> Cursor = MongoCollection.Find(_objectIdentifier);
           
            return Cursor;
        }

        public MongoCursor<BsonDocument> GetCursorSorted(IMongoQuery _objectIdentifier)
        {
            //string _objectIdentifier.
            //BsonBinaryData id = new BsonBinaryData(_objectIdentifier.ToByteArray());

            MongoCursor<BsonDocument> Cursor = MongoCollection.Find(_objectIdentifier).SetSortOrder(SortBy.Descending("PersistedDateTime"));

            return Cursor;
        }

        public WriteConcernResult UpdateCursor(IMongoQuery _objectQuery, IMongoUpdate _objectUpdate)
        {
            //string _objectIdentifier.
            //BsonBinaryData id = new BsonBinaryData(_objectIdentifier.ToByteArray());
            WriteConcernResult result;
            result = MongoCollection.Update(_objectQuery, _objectUpdate);

            return result;
        }
        /// <summary>
        /// Modifies the version guid prior to saving.
        /// </summary>
        /// <param name="_obj"></param>
        public void Persist(T _obj)
        {
            _obj.VersionGuid = Guid.NewGuid();
            _obj.VersionTimeStamp = DateTime.UtcNow.ToString("u");

            PersistFromPropagation(_obj);
        }

        /// <summary>
        /// Does not modify the version guid.
        /// </summary>
        /// <param name="_obj"></param>
        public void PersistFromPropagation(T _obj)
        {
            if (HasParent
                && _obj.ParentGuid == null)
            {
                throw new Exception("Parent Guid not specified.");
            }

            _obj.PersistedDateTime = DateTime.UtcNow;

            BsonDocument document = BsonHelper.Get(_obj);

            MongoCollection.Save(document);

            PostPersist(_obj);
        }

        /// <summary>
        /// Unconfirmed persist of data.  Should only be used on low value data.
        /// </summary>
        /// <param name="_obj"></param>
        public void LazyPersist(T _obj)
        {
            BsonDocument document = BsonHelper.Get(_obj);

            MongoCollection.Save(document, WriteConcern.Unacknowledged);
        }

        /// <summary>
        /// Gets the children of a given parent identifier.
        /// </summary>
        /// <param name="_parentIndentifer">Parent Guid</param>
        /// <returns>Returns null if the object doesn't have a parent.</returns>
        public List<T> GetChildrenOf(Guid _parentIndentifer)
        {
            if (HasParent)
            {
                return GetList("ParentGuid", _parentIndentifer.ToString());
            }

            return null;
        }

        public List<T> GetList(string _columnName, string _columnValue)
        {
            IMongoQuery query = GetQuery(_columnName, _columnValue);
            return GetList(query);
        }

        public List<T> GetList(IMongoQuery _query)
        {
            MongoCursor cursor = MongoCollection.Find(_query);
            return GetList(cursor);
        }  

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<T> GetAll()
        {
            MongoCursor cursor = MongoCollection.FindAll();

            return GetList(cursor);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<T> GetActive()
        {
            IMongoQuery query = GetQuery("Active", Convert.ToString(true));

            return GetList(query);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<T> GetInactive()
        {
            IMongoQuery query = GetQuery("Active", Convert.ToString(false));

            return GetList(query);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public long GetCount()
        {
            return MongoCollection.Count();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_query"></param>
        /// <returns></returns>
        public long GetCount(IMongoQuery _query)
        {
            return MongoCollection.Count(_query);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_cursor"></param>
        /// <returns></returns>
        public T GetFirst(MongoCursor _cursor)
        {
            return GetOneItem<T>(_cursor);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_cursor"></param>
        /// <returns></returns>
        public List<T> GetList(MongoCursor _cursor)
        {
            List<T> items = new List<T>();

            foreach (BsonDocument doc in _cursor)
            {
                items.Add(BsonHelper.Get<T>(doc));
            }

            return items;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_sortColumn"></param>
        /// <param name="_query"></param>
        /// <param name="_columns"></param>
        /// <returns></returns>
        protected DataTable GetDataTable(string _sortColumn, IMongoQuery _query, params string[] _columns)
        {
            DataTable table = new DataTable();
            foreach (string column in _columns)
            {
                table.Columns.Add(column);
            }

            MongoCursor cursor = MongoCollection.Find(_query).SetFields(_columns).SetSortOrder(_sortColumn);

            foreach (BsonDocument document in cursor)
            {
                DataRow row = table.NewRow();

                foreach (string column in _columns)
                {
                    row[column] = document.GetValue(column).AsString;
                }

                table.Rows.Add(row);
            }

            return table;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_name"></param>
        /// <returns></returns>
        protected string GetIndexName(string _name)
        {
            return "IDX_" + TableName + "_" + _name;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_column"></param>
        /// <param name="_indexName"></param>
        protected void BuildUniqueIndex(string _column, string _indexName)
        {
            MongoIndexHelper.BuildUniqueIndex(MongoCollection,
                                              _column,
                                              GetIndexName(_indexName));
        }

        protected void BuildUniqueSparceIndex(string _column, string _indexName)
        {
            MongoIndexHelper.BuildUniqueIndex(MongoCollection,
                new [] {_column},
                GetIndexName(_indexName),
                true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_column"></param>
        /// <param name="_indexName"></param>
        protected void BuildNonuniqueIndex(string _column, string _indexName)
        {
            MongoIndexHelper.BuildNonuniqueIndex(MongoCollection,
                                              _column,
                                              GetIndexName(_indexName));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_columns"></param>
        /// <param name="_indexName"></param>
        protected void BuildUniqueIndex(IEnumerable<string> _columns, string _indexName)
        {
            MongoIndexHelper.BuildUniqueIndex(MongoCollection,
                                              _columns,
                                              GetIndexName(_indexName));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_columns"></param>
        /// <param name="_indexName"></param>
        protected void BuildUniqueSparceIndex(IEnumerable<string> _columns, string _indexName)
        {
            MongoIndexHelper.BuildUniqueIndex(MongoCollection,
                _columns,
                GetIndexName(_indexName), true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_column"></param>
        /// <param name="_indexName"></param>
        protected void BuildGeospatialIndex(string _column, string _indexName)
        {
            MongoIndexHelper.BuildGeospatialIndex(MongoCollection,
                _column,
                GetIndexName(_indexName));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_columns"></param>
        /// <param name="_indexName"></param>
        protected void BuildNonuniqueIndex(IEnumerable<string> _columns, string _indexName)
        {
            MongoIndexHelper.BuildNonuniqueIndex(MongoCollection,
                                             _columns,
                                             GetIndexName(_indexName));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_name"></param>
        /// <param name="_value"></param>
        /// <returns></returns>
        public IMongoQuery GetQuery(string _name, string _value)
        {
            return MongoQueryHelper.GetQuery(_name, _value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_nameValueCollection"></param>
        /// <returns></returns>
        public IMongoQuery GetQuery(NameValueCollection _nameValueCollection)
        {
            return MongoQueryHelper.GetQuery(_nameValueCollection);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_guid"></param>
        /// <returns></returns>
        public IMongoQuery GetIdQuery(Guid _guid)
        {
            return MongoQueryHelper.GetIdQuery(_guid);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_obj"></param>
        public void Delete(T _obj)
        {
            Delete(_obj.Guid);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_objGuid"></param>
        public void Delete(Guid _objGuid)
        {
            IMongoQuery deleteQuery = GetIdQuery(_objGuid);

            MongoCollection.Remove(deleteQuery, WriteConcern.Acknowledged);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_parentGuid"></param>
        protected void DeleteByParent(Guid _parentGuid)
        {
            IMongoQuery deleteQuery = GetQuery("ParentGuid", _parentGuid.ToString());

            MongoCollection.Remove(deleteQuery, WriteConcern.Acknowledged);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="U"></typeparam>
        /// <param name="_mongoQuery"></param>
        /// <returns></returns>
        public U GetOneItem<U>(IMongoQuery _mongoQuery) where U : AbstractTGObject, new()
        {
            BsonDocument doc = MongoCollection.FindOne(_mongoQuery);

            if (doc != null)
            {
                return BsonHelper.Get<U>(doc);
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="U"></typeparam>
        /// <param name="_mongoCursor"></param>
        /// <returns></returns>
        public U GetOneItem<U>(MongoCursor _mongoCursor) where U : AbstractTGObject, new()
        {
            MongoCursor cursor = _mongoCursor.SetLimit(1);

            foreach (BsonDocument doc in cursor)
            {
                U obj = BsonHelper.Get<U>(doc);
                return obj;
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="U"></typeparam>
        /// <param name="_columnName"></param>
        /// <param name="_value"></param>
        /// <returns></returns>
        public U GetOneItem<U>(string _columnName, string _value) where U : AbstractTGObject, new()
        {
            IMongoQuery query = GetQuery(_columnName, _value);

            return GetOneItem<U>(query);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="U"></typeparam>
        /// <param name="_parameters"></param>
        /// <returns></returns>
        public U GetOneItem<U>(NameValueCollection _parameters) where U : AbstractTGObject, new()
        {
            IMongoQuery query = GetQuery(_parameters);

            return GetOneItem<U>(query);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<T> GetAllSorted(string _sortColumn)
        {
            MongoCursor cursor = MongoCollection.FindAll().SetSortOrder(SortBy.Ascending(_sortColumn));
            return GetList(cursor);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_columnName"></param>
        /// <param name="_columnValue"></param>
        /// <param name="_sortColumn"></param>
        /// <returns></returns>
        public List<T> GetSorted(string _columnName, string _columnValue, string _sortColumn)
        {
            IMongoQuery query = GetQuery(_columnName, _columnValue);     
            MongoCursor cursor = MongoCollection.Find(query).SetSortOrder(SortBy.Ascending(_sortColumn));
            return GetList(cursor);
        }

        public List<T> GetSortedByPersisted(DateTime _startDateTime, int _rows, Guid? _parentGuid = null)
        {
            IMongoQuery query;

            if (HasParent
                && _parentGuid != null)
            {
                query = Query.And(Query.EQ("ParentGuid", new BsonString(_parentGuid.Value.ToString())),
                                  Query.GT("PersistedDateTime", new BsonString(_startDateTime.ToString("u"))));
            }
            else
            {
                query = Query.GT("PersistedDateTime", new BsonString(_startDateTime.ToString("u")));
            }

            
            MongoCursor cursor = MongoCollection.Find(query).SetSortOrder(SortBy.Ascending("PersistedDateTime")).SetLimit(_rows);
            return GetList(cursor);
        }
    }
}

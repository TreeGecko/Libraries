using MongoDB.Driver;
using TreeGecko.Library.Mongo.DAOs;

namespace tgTests.Mongo.Daos
{
    public class MockMongoDao : AbstractMongoDAO<MockObject>
    {
        public MockMongoDao(MongoDatabase _mongoDb) 
            : base(_mongoDb)
        {
            HasParent = false;
        }

        /// <summary>
        /// 
        /// </summary>
        public override string TableName
        {
            get { return "MockObjects"; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public MockObject GetFirstByName()
        {
            return GetOneItem<MockObject>("Name");
        }
    }
}

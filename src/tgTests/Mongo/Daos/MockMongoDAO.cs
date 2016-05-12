using MongoDB.Driver;
using TreeGecko.Library.Mongo.DAOs;

namespace tgTests.Mongo.Daos
{
    public class MockMongoDao : AbstractMongoDAO<MockObject>
    {

        public MockMongoDao(MongoDatabase _mongoDb) 
            : base(_mongoDb)
        {
        }


        public override string TableName
        {
            get { return "MockObjects"; }
        }

        //public MockObject GetFirstByName()
        //{
        //    IMongoQuery query = GetQuery()
        //    MongoCursor cursor = GetCursor()
        //}
    }
}

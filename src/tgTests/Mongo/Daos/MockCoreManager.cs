using System;
using MongoDB.Driver;
using TreeGecko.Library.Net.Managers;

namespace tgTests.Mongo.Daos
{
    public class MockCoreManager : AbstractCoreManager
    {
        public MockCoreManager() : base("TG")
        {
        }

        public void Persist(MockObject _object)
        {
            MockMongoDao dao = new MockMongoDao(MongoDB);
            dao.Persist(_object);
        }

        public MockObject GetMockObject(Guid _mockObjectGuid)
        {
            MockMongoDao dao = new MockMongoDao(MongoDB);
            return dao.Get(_mockObjectGuid);
        }

        //public MockObject GetFirstMockObject()
        //{
        //    MockMongoDao dao = new MockMongoDao(MongoDB);

        //    return dao.GetFirstByName();
        //}
    }
}

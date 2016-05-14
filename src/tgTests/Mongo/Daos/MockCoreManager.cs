using System;
using System.Collections.Generic;
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

        public MockObject GetFirstByName()
        {
            MockMongoDao dao = new MockMongoDao(MongoDB);
            return dao.GetFirstByName();
        }

        public List<MockObject> GetMockObjects()
        {
            MockMongoDao dao = new MockMongoDao(MongoDB);
            return dao.GetAll();
        }

        public List<MockObject> GetActiveMockObjects()
        {
            MockMongoDao dao = new MockMongoDao(MongoDB);
            return dao.GetActive();
        }
    }
}

using NUnit.Framework;

namespace tgTests.Mongo.Daos
{
    [TestFixture]
    public class AbstractMongoDaoTests 
    {
        readonly MockCoreManager mcm = new MockCoreManager();

        [OneTimeSetUp]
        public void Setup()
        {
            MockObject m1 = new MockObject { Active = true, Name = "A", Description = "Description of A" };
            MockObject m2 = new MockObject { Active = true, Name = "B", Description = "Description of B" };
            MockObject m3 = new MockObject { Active = true, Name = "C", Description = "Description of C" };
            MockObject m4 = new MockObject { Active = true, Name = "D", Description = "Description of D" };
            MockObject m5 = new MockObject { Active = true, Name = "E", Description = "Description of E" };

            
            mcm.Persist(m5);
            mcm.Persist(m4);
            mcm.Persist(m3);
            mcm.Persist(m2);
            mcm.Persist(m1);
        }

        [Test]
        public void TestGetFirst()
        {
            MockObject m1 = mcm.GetFirstByName();
            Assert.IsNotNull(m1);
            Assert.AreEqual("A", m1.Name);
        }


    }
}

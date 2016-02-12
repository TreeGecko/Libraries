using System;
using NUnit.Framework;
using TreeGecko.Library.Common.Helpers;
using TreeGecko.Library.Common.Objects;

namespace tgTests.Common.Objects
{
    [TestFixture]
    public class AbstractTGObjectTests
    {
        class testClass : AbstractTGObject
        {
            
        }

        [Test]
        public void SerializationTest()
        {
            var tc = new testClass
            {
                Active = true,
                Guid = Guid.NewGuid(),
                LastModifiedBy = Guid.NewGuid(),
                LastModifiedDateTime = DateTime.Now,
                ParentGuid = Guid.NewGuid(),
                PersistedDateTime = DateTime.Now,
                VersionGuid = Guid.NewGuid(),
                VersionTimeStamp = DateHelper.GetCurrentTimeStamp()
            };

            TGSerializedObject tg = tc.GetTGSerializedObject();
            var newTc = TGSerializedObject.GetTGSerializable<testClass>(tg);

            Assert.AreEqual(tc.Active, newTc.Active);
            Assert.AreEqual(tc.Guid, newTc.Guid);
            Assert.AreEqual(tc.LastModifiedBy, newTc.LastModifiedBy);
            Assert.AreEqual(tc.LastModifiedDateTime, newTc.LastModifiedDateTime);
            Assert.AreEqual(tc.ParentGuid, newTc.ParentGuid);
            Assert.AreEqual(tc.PersistedDateTime, newTc.PersistedDateTime);
            Assert.AreEqual(tc.VersionGuid, newTc.VersionGuid);

            Assert.Less(tc.VersionTimeStamp, newTc.VersionTimeStamp);
        }

    }
}

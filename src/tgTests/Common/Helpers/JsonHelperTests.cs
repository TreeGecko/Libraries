using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using tgTests.Mongo.Daos;
using TreeGecko.Library.Common.Helpers;

namespace tgTests.Common.Helpers
{
    [TestFixture]
    public class JsonHelperTests
    {
        [Test]
        public void Serialize()
        {
            MockObject mo = new MockObject();
            mo.Guid = Guid.NewGuid();
            mo.Name = "Test";
            mo.Description = "Description";

            string json = JSONHelper.GetJson(mo);
            Assert.IsNotNull(json);
        }

        [Test]
        public void Deserialize()
        {
            MockObject mo = new MockObject();
            mo.Guid = Guid.NewGuid();
            mo.Name = "Test";
            mo.Description = "Description";

            string json = JSONHelper.GetJson(mo);

            var mo2 = JSONHelper.GetObject<MockObject>(json);
            Assert.IsNotNull(mo2);

            Assert.AreEqual(mo.Guid, mo2.Guid);
            Assert.AreEqual(mo.Name, mo2.Name);
            Assert.AreEqual(mo.Description, mo2.Description);
        }

    }
}

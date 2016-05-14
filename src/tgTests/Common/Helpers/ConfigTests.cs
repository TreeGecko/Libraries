using NUnit.Framework;
using TreeGecko.Library.Common.Helpers;

namespace tgTests.Common.Helpers
{
    [TestFixture]
    public class ConfigTests
    {
        [Test]
        public void StringTest()
        {
            string test = Config.GetSettingValue("TestA", "TestA1");
            Assert.IsNotNull(test);
            Assert.AreEqual("TestA1", test);

            string test2 = Config.GetSettingValue("TestStringValue", "notfound");
            Assert.IsNotNull(test2);
            Assert.AreEqual("found", test2);
        }

        [Test]
        public void IntTest()
        {
            int testVal = Config.GetIntValue("TestIntValue", 12);
            Assert.AreEqual(testVal, 11);

            int testVal2 = Config.GetIntValue("MissingTestIntValue", 14);
            Assert.AreEqual(testVal2, 14);
        }

        [Test]
        public void BoolTest()
        {
            bool test = Config.GetBooleanValue("TestBoolValue", false);
            Assert.IsTrue(test);

            bool test2 = Config.GetBooleanValue("MissingTestBoolValue", true);
            Assert.IsTrue(test2);
        }

    }
}

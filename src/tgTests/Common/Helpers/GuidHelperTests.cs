using System;
using NUnit.Framework;
using TreeGecko.Library.Common.Helpers;

namespace tgTests.Common.Helpers
{
    [TestFixture]
    public class GuidHelperTests
    {
        [Test]
        public void TestOne()
        {
            string temp = Guid.NewGuid().ToString();

            bool result = GuidHelper.IsValidGuidString(temp);
            Assert.IsTrue(result);
        }

        [Test]
        public void TestTwo()
        {
            string temp = "test";

            bool result = GuidHelper.IsValidGuidString(temp);
            Assert.IsFalse(result);
        }

        [Test]
        public void TestThree()
        {
            bool result = GuidHelper.IsValidGuidString(null);
            Assert.IsFalse(result);
        }
    }
}

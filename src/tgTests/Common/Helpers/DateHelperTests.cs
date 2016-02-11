using System;
using NUnit.Framework;
using TreeGecko.Library.Common.Helpers;

namespace tgTests.Common.Helpers
{
    [TestFixture]
    public class DateHelperTests
    {
        [Test]
        public void ParseDateTime()
        {
            DateTime now = new DateTime();
            string nowString = now.ToString("o");

            DateTime outputDateTime = DateHelper.ParseDateTimeString(nowString);

            Assert.AreEqual(now, outputDateTime);
        }

        [Test]
        public void ParseDateTimeUFormat()
        {
            DateTime now = new DateTime();
            string nowString = now.ToString("u");

            DateTime outputDateTime = DateHelper.ParseDateTimeString(nowString);

            Assert.AreEqual(now, outputDateTime);
        }
    }
}

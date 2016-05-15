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
        public void ParseDateTimeNull()
        {
            try
            {
                DateTime outputDateTime = DateHelper.ParseDateTimeString(null);
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOf<ArgumentNullException>(ex);
            }
        }

        [Test]
        public void ParseString()
        {
            DateTime time = DateTime.UtcNow;

            DateTime? value = DateHelper.ParseDate(time.ToShortDateString());

            Assert.IsNotNull(value);
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

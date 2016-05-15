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
                Assert.Fail("Should have had an exception");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOf<ArgumentNullException>(ex);
            }
        }

        [Test]
        public void ParseDateTimeNow()
        {
            DateTime? outputDateTime = DateHelper.ParseDate("now");
        }

        [Test]
        public void ParseDateTimeToday()
        {
            DateTime? outputDateTime = DateHelper.ParseDate("today");
        }

        [Test]
        public void ParseDateTimeYesterday()
        {
            DateTime? outputDateTime = DateHelper.ParseDate("yesterday");
        }

        [Test]
        public void ParseDateTimeTomorrow()
        {
            DateTime? outputDateTime = DateHelper.ParseDate("tomorrow");
        }

        [Test]
        public void ParseDateTimeNowMinus()
        {
            DateTime? outputDateTime = DateHelper.ParseDate("now-1");
        }

        [Test]
        public void ParseDateTimeTodayMinus()
        {
            DateTime? outputDateTime = DateHelper.ParseDate("today-2");
        }

        [Test]
        public void ParseDateTimeYesterdayMinus()
        {
            DateTime? outputDateTime = DateHelper.ParseDate("yesterday-2");
        }

        [Test]
        public void ParseDateTimeTomorrowMinus()
        {
            DateTime? outputDateTime = DateHelper.ParseDate("tomorrow-2");
        }

        [Test]
        public void ParseDateTimeNowPlus()
        {
            DateTime? outputDateTime = DateHelper.ParseDate("now+1");
        }

        [Test]
        public void ParseDateTimeTodayPlus()
        {
            DateTime? outputDateTime = DateHelper.ParseDate("today+2");
        }

        [Test]
        public void ParseDateTimeYesterdayPlus()
        {
            DateTime? outputDateTime = DateHelper.ParseDate("yesterday+2");
        }

        [Test]
        public void ParseDateTimeTomorrowPlus()
        {
            DateTime? outputDateTime = DateHelper.ParseDate("tomorrow+2");
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

using System;
using NUnit.Framework;
using TreeGecko.Library.Common.Objects;

namespace tgTests.Common.Objects
{
    [TestFixture]
    public class TGSerializedObjectTests
    {
        [Test]
        public void LocalDateTimeTest()
        {
            TGSerializedObject tg = new TGSerializedObject();

            DateTime now = DateTime.Now;

            tg.Add("LocalDateValue", now);

            DateTime retrievedValue = tg.GetDateTime("LocalDateValue");

            Assert.AreEqual(now, retrievedValue);
        }

        [Test]
        public void UTCDateTimeTest()
        {
            TGSerializedObject tg = new TGSerializedObject();

            DateTime now = DateTime.UtcNow;

            tg.Add("UtcDateValue", now);

            DateTime retrievedValue = tg.GetDateTime("UtcDateValue");

            Assert.AreEqual(now, retrievedValue);
        }


    }
}

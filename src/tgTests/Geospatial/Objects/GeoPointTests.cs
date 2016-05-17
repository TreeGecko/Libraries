using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using TreeGecko.Library.Common.Objects;
using TreeGecko.Library.Geospatial.Objects;

namespace tgTests.Geospatial.Objects
{
    [TestFixture]
    public class GeoPointTests
    {
        [Test]
        public void SerializationTest()
        {
            GeoPoint gp = new GeoPoint(-104, 39);
            TGSerializedObject tgs = gp.GetTGSerializedObject();
            
            GeoPoint gp2 = new GeoPoint();
            gp2.LoadFromTGSerializedObject(tgs);

            Assert.AreEqual(gp.X, gp2.X);
            Assert.AreEqual(gp.Y, gp2.Y);
        }

        [Test]
        public void ToStringTest()
        {
            GeoPoint gp = new GeoPoint(-104, 39);

            string text = gp.ToString();
            Assert.IsNotNull(text);
        }

        [Test]
        public void ToStringFormatTest()
        {
            GeoPoint gp = new GeoPoint(-104, 39);

            string text = gp.ToString("#.####");
            Assert.IsNotNull(text);
        }

        [Test]
        public void ToStringFormatTest2()
        {
            GeoPoint gp = new GeoPoint(-104, 39);

            string text = gp.ToString("{0} Lat, {1} lon", "#.####");
            Assert.IsNotNull(text);
        }

        [Test]
        public void ParseOpenGisText()
        {
            GeoPoint gp = new GeoPoint(-104, 39);

            string text = gp.GetOpenGISText();
            Assert.IsNotNull(text);

            GeoPoint gp2 = GeoPoint.Parse(text);
            Assert.IsNotNull(gp2);
            Assert.AreEqual(gp.X, gp2.X);
            Assert.AreEqual(gp.Y, gp2.Y);
        }

        [Test]
        public void GeoPointConstructor()
        {
            GeoPoint gp = new GeoPoint(-104, 39);

            string text = gp.GetOpenGISText();
            Assert.IsNotNull(text);

            GeoPoint gp2 = new GeoPoint(text);
            Assert.IsNotNull(gp2);
            Assert.AreEqual(gp.X, gp2.X);
            Assert.AreEqual(gp.Y, gp2.Y);
        }

        [Test]
        public void ParseGeoJsonText()
        {
            GeoPoint gp = new GeoPoint(-104, 39);

            string text = gp.ToGeoJson();
            Assert.IsNotNull(text);

            GeoPoint gp2 = GeoPoint.Parse(text);
            Assert.IsNotNull(gp2);
            Assert.AreEqual(gp.X, gp2.X);
            Assert.AreEqual(gp.Y, gp2.Y);
        }

    }
}

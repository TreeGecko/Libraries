using System;
using NUnit.Framework;
using TreeGecko.Library.Geospatial.Helpers;

namespace tgTests.Geospatial.Helpers
{
    [TestFixture]
    public class ConversionHelperTests
    {
        private void AssertApproxEqual(double _expected, double _actual)
        {
            double diff = Math.Abs(_expected - _actual);

            Assert.LessOrEqual(diff, 0.001);
        }

        [Test]
        public void ConvertDistanceTest()
        {
            double result = ConversionHelper.ConvertDistance(DistanceMeasurementType.Feet, DistanceMeasurementType.Mile, 5280);
            AssertApproxEqual(1, result);

            result = ConversionHelper.ConvertDistanceToMeters(DistanceMeasurementType.Kilometer, 1.2);
            AssertApproxEqual(1200, result);

            result = ConversionHelper.ConvertDistanceFromMeters(DistanceMeasurementType.Kilometer, 1000);
            AssertApproxEqual(1, result);

            
        }

        [Test]
        public void ConvertSpeedTest()
        {
            double result = ConversionHelper.ConvertSpeed(SpeedMeasurementType.MilesPerHour, SpeedMeasurementType.FeetPerSecond, 60);
        }
    }
}

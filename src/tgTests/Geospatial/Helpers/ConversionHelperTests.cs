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

            result = ConversionHelper.ConvertDistance(DistanceMeasurementType.Feet, DistanceMeasurementType.Feet, 5280);
            AssertApproxEqual(5280, result);

            result = ConversionHelper.ConvertDistanceToMeters(DistanceMeasurementType.Kilometer, 1.2);
            AssertApproxEqual(1200, result);

            result = ConversionHelper.ConvertDistanceToMeters(DistanceMeasurementType.Feet, 1000);

            result = ConversionHelper.ConvertDistanceToMeters(DistanceMeasurementType.Mile, 1);

            result = ConversionHelper.ConvertDistanceToMeters(DistanceMeasurementType.Meter, 1000);
            Assert.AreEqual(1000, result);
            
            
            result = ConversionHelper.ConvertDistanceFromMeters(DistanceMeasurementType.Kilometer, 1000);
            AssertApproxEqual(1, result);

            result = ConversionHelper.ConvertDistanceFromMeters(DistanceMeasurementType.Feet, 1000);

            result = ConversionHelper.ConvertDistanceFromMeters(DistanceMeasurementType.Mile, 1000);

            result = ConversionHelper.ConvertDistanceFromMeters(DistanceMeasurementType.Meter, 1000);
            Assert.AreEqual(1000, result);

        }

        [Test]
        public void ConvertSpeedTest()
        {
            double result = ConversionHelper.ConvertSpeed(SpeedMeasurementType.FeetPerSecond, SpeedMeasurementType.FeetPerSecond, 60);

            result = ConversionHelper.ConvertSpeed(SpeedMeasurementType.MilesPerHour, SpeedMeasurementType.FeetPerSecond, 60);

            result = ConversionHelper.ConvertSpeed(SpeedMeasurementType.MilesPerHour, SpeedMeasurementType.MetersPerSecond, 60);

            result = ConversionHelper.ConvertSpeed(SpeedMeasurementType.MilesPerHour, SpeedMeasurementType.KilometersPerHour, 60);

            result = ConversionHelper.ConvertSpeed(SpeedMeasurementType.MetersPerSecond, SpeedMeasurementType.FeetPerSecond, 60);

            result = ConversionHelper.ConvertSpeed(SpeedMeasurementType.KilometersPerHour, SpeedMeasurementType.FeetPerSecond, 60);

        }
    }
}

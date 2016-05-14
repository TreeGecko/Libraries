namespace TreeGecko.Library.Geospatial.Helpers
{
    public enum DistanceMeasurementType
    {
        Feet,
        Mile,
        Meter,
        Kilometer
    }

    public enum SpeedMeasurementType
    {
        FeetPerSecond,
        MilesPerHour,
        MetersPerSecond,
        KilometersPerHour
    }

    public class ConversionHelper
    {
        public static double ConvertDistance(DistanceMeasurementType _fromType,
            DistanceMeasurementType _toType, double _fromValue)
        {
            if (_fromType == _toType)
            {
                return _fromValue;
            }

            double meterValue = ConvertDistanceToMeters(_fromType, _fromValue);
            return ConvertDistanceFromMeters(_toType, meterValue);
        }


        public static double ConvertDistanceFromMeters(DistanceMeasurementType _toType,
            double _fromValue)
        {
            double multiplier = 1.0;

            switch (_toType)
            {
                case DistanceMeasurementType.Feet:
                {
                    multiplier = 3.2808399;
                    break;
                }
                case DistanceMeasurementType.Kilometer:
                {
                    multiplier = .001;
                    break;
                }
                case DistanceMeasurementType.Mile:
                {
                    multiplier = .000621371192;
                    break;
                }
                case DistanceMeasurementType.Meter:
                default:
                {
                    multiplier = 1.0;
                    break;
                }
            }

            return multiplier*_fromValue;
        }


        public static double ConvertDistanceToMeters(DistanceMeasurementType _fromType,
            double _fromValue)
        {
            double multiplier = 1.0;

            switch (_fromType)
            {
                case DistanceMeasurementType.Feet:
                {
                    multiplier = .3048;
                    break;
                }
                case DistanceMeasurementType.Kilometer:
                {
                    multiplier = 1000.0;
                    break;
                }
                case DistanceMeasurementType.Mile:
                {
                    multiplier = 1609.344;
                    break;
                }
                case DistanceMeasurementType.Meter:
                default:
                {
                    multiplier = 1.0;
                    break;
                }
            }

            return multiplier*_fromValue;
        }


        public static double ConvertSpeedFromMPS(SpeedMeasurementType _toType,
            double _fromValue)
        {
            double multiplier = 1.0;

            switch (_toType)
            {
                case SpeedMeasurementType.FeetPerSecond:
                {
                    multiplier = 3.2808399;
                    break;
                }
                case SpeedMeasurementType.KilometersPerHour:
                {
                    multiplier = 3.6;
                    break;
                }
                case SpeedMeasurementType.MilesPerHour:
                {
                    multiplier = 2.23693629;
                    break;
                }
                case SpeedMeasurementType.MetersPerSecond:
                default:
                {
                    multiplier = 1.0;
                    break;
                }
            }

            return multiplier*_fromValue;
        }

        public static double ConvertSpeedToMPS(SpeedMeasurementType _fromType,
            double _fromValue)
        {
            double multiplier = 1.0;

            switch (_fromType)
            {
                case SpeedMeasurementType.FeetPerSecond:
                {
                    multiplier = .3048;
                    break;
                }
                case SpeedMeasurementType.KilometersPerHour:
                {
                    multiplier = .277777778;
                    break;
                }
                case SpeedMeasurementType.MilesPerHour:
                {
                    multiplier = .44704;
                    break;
                }
                case SpeedMeasurementType.MetersPerSecond:
                default:
                {
                    multiplier = 1.0;
                    break;
                }
            }

            return multiplier*_fromValue;
        }

        public static double ConvertSpeed(SpeedMeasurementType _fromType,
            SpeedMeasurementType _toType, double _fromValue)
        {
            if (_fromType == _toType)
            {
                return _fromValue;
            }

            double mpsValue = ConvertSpeedToMPS(_fromType, _fromValue);
            return ConvertSpeedFromMPS(_toType, mpsValue);
        }
    }
}
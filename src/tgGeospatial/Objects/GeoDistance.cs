using DistanceUnits = TreeGecko.Library.Geospatial.Enums.DistanceUnits;

namespace TreeGecko.Library.Geospatial.Objects
{
    public class GeoDistance
    {
        public const double FEET_TO_METERS = 0.3048;
        public const double YARDS_TO_METERS = 0.9144;
        public const double STATUTE_MILES_TO_METERS = 1609.344;
        public const double NAUTICAL_MILES_TO_METERS = 1852;
        public const double METERS_TO_METERS = 1;
        public const double KILOMETERS_TO_METERS = 1000;

        public GeoDistance()
        {
            SetValue(DistanceUnits.Meters, 0);
        }

        public GeoDistance(DistanceUnits _distanceUnit, double _value)
        {
            SetValue(_distanceUnit, _value);
        }

        /// <summary>
        /// 
        /// </summary>
        public DistanceUnits DistanceUnit { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public double Meters { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public double GetValueInUnits()
        {
            double factor = GetFactor(DistanceUnit);

            if (!factor.Equals(0.0))
            {
                return Meters/factor;
            }

            return 0;
        }

        public double ToFeet()
        {
            double factor = GetFactor(DistanceUnits.Feet);

            if (!factor.Equals(0.0))
            {
                return Meters/factor;
            }

            return 0;
        }


        public void SetValue(DistanceUnits _distanceUnit, double _value)
        {
            DistanceUnit = _distanceUnit;
            Meters = GetMeters(_distanceUnit, _value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_distanceUnit"></param>
        /// <returns></returns>
        public static double GetFactor(DistanceUnits _distanceUnit)
        {
            double factor = 0;

            switch (_distanceUnit)
            {
                case DistanceUnits.Feet:
                    factor = FEET_TO_METERS;
                    break;
                case DistanceUnits.Kilometers:
                    factor = KILOMETERS_TO_METERS;
                    break;
                case DistanceUnits.Meters:
                    factor = METERS_TO_METERS;
                    break;
                case DistanceUnits.NauticalMiles:
                    factor = NAUTICAL_MILES_TO_METERS;
                    break;
                case DistanceUnits.StatuteMiles:
                    factor = STATUTE_MILES_TO_METERS;
                    break;
                case DistanceUnits.Yards:
                    factor = YARDS_TO_METERS;
                    break;
            }

            return factor;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_distanceUnit"></param>
        /// <param name="_value"></param>
        /// <returns></returns>
        public static double GetMeters(DistanceUnits _distanceUnit, double _value)
        {
            double factor = GetFactor(_distanceUnit);
            return _value*factor;
        }

        public override string ToString()
        {
            return string.Format("{0} meters", Meters)
                ;
        }
    }
}
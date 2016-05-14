using TreeGecko.Library.Common.Objects;
using TreeGecko.Library.Geospatial.Geoframeworks.Objects;
using TreeGecko.Library.Geospatial.Objects;
using DistanceUnits = TreeGecko.Library.Geospatial.Enums.DistanceUnits;

namespace TreeGecko.Library.Geospatial.Helpers
{
    /// <summary>
    /// 
    /// </summary>
    public static class PositionHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_pointFrom"></param>
        /// <param name="_pointTo"></param>
        /// <returns></returns>
        public static GeoDistance GetDistance(GeoPoint _pointFrom, GeoPoint _pointTo)
        {
            Position pFrom = new Position(new Longitude(_pointFrom.X), new Latitude(_pointFrom.Y));
            Position pTo = new Position(new Longitude(_pointTo.X), new Latitude(_pointTo.Y));

            Distance distance = pFrom.DistanceTo(pTo);

            GeoDistance returnValue = new GeoDistance(DistanceUnits.Meters, distance.ToMeters().Value);

            return returnValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_start"></param>
        /// <param name="_newPosition"></param>
        /// <returns></returns>
        public static GeoPoint Average(GeoPoint _start, GeoPoint _newPosition)
        {
            double x = (_start.X + _newPosition.X)/2;
            double y = (_start.Y + _newPosition.Y)/2;

            GeoPoint pos = new GeoPoint(x, y);

            return pos;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_pointFrom"></param>
        /// <param name="_geoDistance"></param>
        /// <returns></returns>
        public static GeoPoint GetPointToWest(GeoPoint _pointFrom, GeoDistance _geoDistance)
        {
            Position pFrom = new Position(new Longitude(_pointFrom.X), new Latitude(_pointFrom.Y));
            Distance distance = new Distance(_geoDistance.Meters, DistanceUnit.Meters);

            Position pTo = pFrom.TranslateTo(Azimuth.West, distance);

            GeoPoint returnValue = new GeoPoint(pTo.Longitude.DecimalDegrees, pTo.Latitude.DecimalDegrees);
            return returnValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_pointFrom"></param>
        /// <param name="_geoDistance"></param>
        /// <returns></returns>
        public static GeoPoint GetPointToEast(GeoPoint _pointFrom, GeoDistance _geoDistance)
        {
            Position pFrom = new Position(new Longitude(_pointFrom.X), new Latitude(_pointFrom.Y));
            Distance distance = new Distance(_geoDistance.Meters, DistanceUnit.Meters);

            Position pTo = pFrom.TranslateTo(Azimuth.East, distance);

            GeoPoint returnValue = new GeoPoint(pTo.Longitude.DecimalDegrees, pTo.Latitude.DecimalDegrees);
            return returnValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_pointFrom"></param>
        /// <param name="_geoDistance"></param>
        /// <returns></returns>
        public static GeoPoint GetPointToNorth(GeoPoint _pointFrom, GeoDistance _geoDistance)
        {
            Position pFrom = new Position(new Longitude(_pointFrom.X), new Latitude(_pointFrom.Y));
            Distance distance = new Distance(_geoDistance.Meters, DistanceUnit.Meters);

            Position pTo = pFrom.TranslateTo(Azimuth.North, distance);

            GeoPoint returnValue = new GeoPoint(pTo.Longitude.DecimalDegrees, pTo.Latitude.DecimalDegrees);
            return returnValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_pointFrom"></param>
        /// <param name="_geoDistance"></param>
        /// <returns></returns>
        public static GeoPoint GetPointToSouth(GeoPoint _pointFrom, GeoDistance _geoDistance)
        {
            Position pFrom = new Position(new Longitude(_pointFrom.X), new Latitude(_pointFrom.Y));
            Distance distance = new Distance(_geoDistance.Meters, DistanceUnit.Meters);

            Position pTo = pFrom.TranslateTo(Azimuth.South, distance);

            GeoPoint returnValue = new GeoPoint(pTo.Longitude.DecimalDegrees, pTo.Latitude.DecimalDegrees);
            return returnValue;
        }

        /// <summary>
        /// Gets the bearing.
        /// </summary>
        /// <returns>
        /// The bearing.
        /// </returns>
        /// <param name='_pointFrom'>
        /// Point from.
        /// </param>
        /// <param name='_pointTo'>
        /// Point to.
        /// </param>
        public static double GetBearing(GeoPoint _pointFrom, GeoPoint _pointTo)
        {
            Position pFrom = new Position(new Longitude(_pointFrom.X), new Latitude(_pointFrom.Y));
            Position pTo = new Position(new Longitude(_pointTo.X), new Latitude(_pointTo.Y));

            Azimuth bearing = pFrom.BearingTo(pTo);

            return bearing.DecimalDegrees;
        }
    }
}
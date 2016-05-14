using System;
using TreeGecko.Library.Common.Objects;
using TreeGecko.Library.Geospatial.Interfaces;
using TreeGecko.Library.Geospatial.Objects;

namespace TreeGecko.Library.Geospatial.Factories
{
    public static class OpenGISFactory
    {
        public static IGeoObject BuildGeoObject(string _wellKnownText)
        {
            string wkt = _wellKnownText.Trim();
            IGeoObject geoObject = null;

            if (wkt.StartsWith("Point", StringComparison.InvariantCultureIgnoreCase))
            {
                geoObject = new GeoPoint(wkt);
            }
            else if (wkt.StartsWith("LineString", StringComparison.InvariantCultureIgnoreCase))
            {
                geoObject = new GeoLine(wkt);
            }
            else if (wkt.StartsWith("Polygon", StringComparison.InvariantCultureIgnoreCase))
            {
                geoObject = new GeoPolygon(wkt);
            }

            return geoObject;
        }
    }
}
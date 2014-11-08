using TreeGecko.Library.Geospatial.Interfaces;
using TreeGecko.Library.Geospatial.Objects;

namespace TreeGecko.Library.Geospatial.Helpers
{
    public static class GeometryHelper
    {
        public static IGeoJson GetGeometryObject(string _geoJson)
        {
            //TODO - add other geometries and true parsing of them

            return GeoPoint.ParseGeoJson(_geoJson);
        }
    }
}
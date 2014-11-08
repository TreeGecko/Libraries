namespace TreeGecko.Library.Geospatial.Interfaces
{
    public interface IGeoJson
    {
        string ToGeoJson();
        void FromGeoJson(string _geoJson);
    }
}

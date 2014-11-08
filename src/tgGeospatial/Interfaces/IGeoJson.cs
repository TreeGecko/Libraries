namespace TreeGecko.Library.Geospatial.Interfaces
{
    public interface IGeoObject
    {
        string GetOpenGISText();
        void ParseOpenGISText(string _wellKnownText);
    }
}

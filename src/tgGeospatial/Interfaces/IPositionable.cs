using TreeGecko.Library.Geospatial.Objects;

namespace TreeGecko.Library.Geospatial.Interfaces
{
    public interface IPositionable
    {
        GeoPoint Position { get; set; }
    }
}
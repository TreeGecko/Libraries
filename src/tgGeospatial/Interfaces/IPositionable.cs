using TreeGecko.Library.Common.Objects;
using TreeGecko.Library.Geospatial.Objects;

namespace TreeGecko.Library.Common.Interfaces
{
	public interface IPositionable
	{
		GeoPoint Position { get; set; } 
	}
}


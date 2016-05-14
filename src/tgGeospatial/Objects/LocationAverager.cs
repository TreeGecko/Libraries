using TreeGecko.Library.Geospatial.Objects;

namespace TreeGecko.Library.Common.Location
{
    /// <summary>
    /// 
    /// </summary>
    public class LocationAverager
    {
        public GeoPoints Positions { get; private set; }

        public LocationAverager()
        {
            Positions = new GeoPoints();
        }

        public void AddPosition(GeoPoint _position)
        {
            Positions.Add(_position);
        }

        public GeoPoint GetAverage()
        {
            if (Positions.Count > 0)
            {
                double lat = 0;
                double lon = 0;
                int count = 0;

                foreach (GeoPoint position in Positions)
                {
                    lat += position.Y;
                    lon += position.X;
                    count++;
                }

                lat = lat/count;
                lon = lon/count;

                GeoPoint gp = new GeoPoint(lon, lat);
                return gp;
            }
            else
            {
                return null;
            }

            //TODO - add statistical analysis and outlier removal
            //Rejected Points
            //etc.
        }
    }
}
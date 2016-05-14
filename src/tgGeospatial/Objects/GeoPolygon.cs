using System;
using System.Collections.Generic;
using System.Text;
using TreeGecko.Library.Common.Helpers;
using TreeGecko.Library.Geospatial.Interfaces;

namespace TreeGecko.Library.Geospatial.Objects
{
    public class GeoPolygon : IGeoObject
    {
        public GeoPolygon()
        {
            Points = new List<GeoPoint>();
        }

        public GeoPolygon(string _wellKnownText)
        {
            Points = new List<GeoPoint>();
            ParseOpenGISText(_wellKnownText);
        }

        public List<GeoPoint> Points { get; set; }

        public string GetOpenGISText()
        {
            StringBuilder sb = new StringBuilder();

            if (Points.Count > 0)
            {
                foreach (GeoPoint gp in Points)
                {
                    sb.AppendFormat("{0} {1},", gp.X, gp.Y);
                }

                GeoPoint gp2 = Points[0];
                sb.AppendFormat("{0} {1}", gp2.X, gp2.Y);
            }

            return string.Format("Polygon (({0}))", sb);
        }

        public void ParseOpenGISText(string _wellKnownText)
        {
           string wkt = _wellKnownText.Trim();

            if (wkt.StartsWith("Polygon", StringComparison.InvariantCultureIgnoreCase))
            {
                int start = wkt.IndexOf("((", StringComparison.InvariantCulture);
                int end = wkt.IndexOf("))", StringComparison.InvariantCulture);

                string inner = wkt.Substring(start + 2, end - (start + 2));

                string[] points = inner.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                foreach (string point in points)
                {
                    string[] parts = point.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                    if (parts.Length == 2)
                    {
                        GeoPoint gp = new GeoPoint
                                          {
                                              X = Convert.ToDouble(parts[0]),
                                              Y = Convert.ToDouble(parts[1])
                                          };

                        Points.Add(gp);
                    }
                    else
                    {
                        TraceFileHelper.Warning(string.Format("Invalid Point Definition - {0}", wkt));
                        throw new Exception("Invalid Line definition");
                    }
                }

            }
            else
            {
                TraceFileHelper.Warning(string.Format("Not a line - {0}", wkt));
                throw new Exception("Not a line");
            }
        }
    }
}

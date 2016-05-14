using System;
using System.Collections.Generic;
using System.Text;
using TreeGecko.Library.Common.Helpers;
using TreeGecko.Library.Geospatial.Interfaces;

namespace TreeGecko.Library.Geospatial.Objects
{
    public class GeoLine : IGeoObject
    {
        public GeoLine()
        {
            Points = new List<GeoPoint>();
        }

        public GeoLine(string _wellKnownText)
        {
            Points = new List<GeoPoint>();
            ParseOpenGISText(_wellKnownText);
        }

        public List<GeoPoint> Points { get; set; }

        public string GetOpenGISText()
        {
            StringBuilder sb = new StringBuilder();

            foreach (GeoPoint gp in Points)
            {
                sb.AppendFormat("{0} {1},", gp.X, gp.Y);
            }

            if (sb.Length > 0)
            {
                sb.Remove(sb.Length - 1, 1);
            }

            return string.Format("LineString ({0})", sb.ToString());
        }

        public void ParseOpenGISText(string _wellKnownText)
        {
            string wkt = _wellKnownText.Trim();

            if (wkt.StartsWith("LineString", StringComparison.InvariantCultureIgnoreCase))
            {
                int start = wkt.IndexOf("(");
                int end = wkt.IndexOf(")");

                string inner = wkt.Substring(start + 1, end - (start + 1));

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
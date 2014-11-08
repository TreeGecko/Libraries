using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using TreeGecko.Library.Common.Helpers;
using TreeGecko.Library.Common.Interfaces;
using TreeGecko.Library.Geospatial.Objects;

namespace TreeGecko.Library.Common.Objects
{
    public class GeoPoints : List<GeoPoint>, ITGSerializable
    {
        public GeoPoints()
        {
        }

        public GeoPoints(string _wellKnownText)
        {
            ParseOpenGISText(_wellKnownText);
        }

        public string GetOpenGISText()
        {
            if (Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("MULTIPOINT (");

                int i = 0;
                foreach (GeoPoint point in this)
                {
                    sb.AppendFormat("({0} {1}),", point.X, point.Y);
                    i++;
                }

                if (i > 0)
                {
                    //Remove the last comma
                    sb.Remove(sb.Length - 1, 1);
                }

                sb.Append(")");

                return sb.ToString();
            }

            return null;
        }

        public static GeoPoints Parse(string _wellKnownText)
        {
            GeoPoints gps = new GeoPoints(_wellKnownText);
            return gps;
        }

        public void ParseOpenGISText(string _wellKnownText)
        {
            string wkt = _wellKnownText.Trim();

            const string MP_REGEX = @"MULTIPOINT \((?<Points>.+)\)";
            const string POINT_REGEX = @"(?<Point>\(.+?\))";
            const string XY_REGEX = @"\((?<X>.+?) (?<Y>.+)\)";

            Match pointsMatch = Regex.Match(_wellKnownText, MP_REGEX);

            if (pointsMatch != null)
            {
                string points = pointsMatch.Groups["Points"].Value;

                MatchCollection mc = Regex.Matches(points, POINT_REGEX);

                if (mc != null
                    && mc.Count > 0)
                {
                    foreach (Match pointMatch in mc)
                    {
                        string point = pointMatch.Groups[""].Value;

                        Match xyMatch = Regex.Match(point, XY_REGEX);

                        if (xyMatch != null)
                        {
                            double x = Convert.ToDouble(xyMatch.Groups["X"].Value);
                            double y = Convert.ToDouble(xyMatch.Groups["Y"].Value);

                            GeoPoint gp = new GeoPoint(x, y);
                            Add(gp);
                        }
                    }
                }

            }
            else  
            {
                TraceFileHelper.Warning(string.Format("Not a point - {0}", wkt));
                throw new Exception("Not a point");
            }
        }

        public override string ToString()
        {
            return GetOpenGISText();
        }
		
		

        public TGSerializedObject GetTGSerializedObject()
        {
            TGSerializedObject tg = new TGSerializedObject
                                          {
                                              TGObjectType = TGObjectType
                                          };

            tg.Add("GeoPoints", GetOpenGISText());
           
            return tg;
        }

        public void LoadFromTGSerializedObject(TGSerializedObject _tg)
        {
            if (_tg!=null 
                && _tg.TGObjectType == TGObjectType)
            {
                this.Clear();

                string points = _tg.GetString("GeoPoints");

                if (points != null)
                {
                    ParseOpenGISText(points);
                }
            }
        }

        public string TGObjectType
        {
            get { return ReflectionHelper.GetTypeName(GetType()); }
        }
    }
}

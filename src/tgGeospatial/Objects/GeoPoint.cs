using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TreeGecko.Library.Common.Helpers;
using TreeGecko.Library.Common.Interfaces;
using TreeGecko.Library.Common.Objects;
using TreeGecko.Library.Geospatial.Interfaces;

namespace TreeGecko.Library.Geospatial.Objects
{
    public class GeoPoint : IGeoObject, IGeoJson, ITGSerializable
    {
        public double X { get; set; }
        public double Y { get; set; }

        public GeoPoint()
        {
        }

        public GeoPoint(double _x, double _y)
        {
            X = _x;
            Y = _y;
        }

        public GeoPoint(string _wellKnownText)
        {
            ParseOpenGISText(_wellKnownText);
        }

        public string GetOpenGISText()
        {
            return string.Format("POINT ({0} {1})", X, Y);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string ToGeoJson()
        {
            return "{" + string.Format("\"type\": \"Point\", \"coordinates\": [{0}, {1}]", X, Y) + "}";
        }

        public static GeoPoint Parse(string _wellKnownText)
        {
            GeoPoint gp;
            if (_wellKnownText.Contains("{"))
            {
                gp = ParseGeoJson(_wellKnownText);
            }
            else
            {
                gp = new GeoPoint();
                gp.ParseOpenGISText(_wellKnownText);
            }

            return gp;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_json"></param>
        /// <returns></returns>
        public static GeoPoint ParseGeoJson(string _json)
        {
            if (_json != null)
            {
                try
                {
                    JObject point = (JObject) JsonConvert.DeserializeObject(_json);
                    JArray coordinates = (JArray) point["coordinates"];

                    double x = coordinates[0].Value<Double>();
                    double y = coordinates[1].Value<Double>();

                    return new GeoPoint(x, y);
                }
                catch (Exception)
                {
                }
            }

            return null;
        }

        public void ParseOpenGISText(string _wellKnownText)
        {
            string wkt = _wellKnownText.Trim();

            if (wkt.StartsWith("Point", StringComparison.InvariantCultureIgnoreCase))
            {
                int start = wkt.IndexOf("(");
                int end = wkt.IndexOf(")");

                string inner = wkt.Substring(start + 1, end - (start + 1));

                string[] parts = inner.Split(" ".ToCharArray());

                if (parts.Length == 2)
                {
                    X = Convert.ToDouble(parts[0]);
                    Y = Convert.ToDouble(parts[1]);
                }
                else
                {
                    TraceFileHelper.Warning(string.Format("Invalid Point Definition - {0}", wkt));
                    throw new Exception("Invalid Point definition");
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

        public string ToString(string _format)
        {
            string lat = Y.ToString(_format);
            string lon = X.ToString(_format);

            return string.Format("Latitude: {0}, Longitude: {1}", lat, lon);
        }

        public string ToString(string _stringFormat, string _numberFormat)
        {
            string lat = Y.ToString(_numberFormat);
            string lon = X.ToString(_numberFormat);

            return string.Format(_stringFormat, lat, lon);
        }

        public TGSerializedObject GetTGSerializedObject()
        {
            TGSerializedObject tg = new TGSerializedObject
            {
                TGObjectType = TGObjectType
            };

            tg.Add("X", X);
            tg.Add("Y", Y);

            return tg;
        }

        public void LoadFromTGSerializedObject(TGSerializedObject _tg)
        {
            if (_tg != null
                && _tg.TGObjectType == TGObjectType)
            {
                X = _tg.GetDouble("X");
                Y = _tg.GetDouble("Y");
            }
        }

        public string TGObjectType
        {
            get { return ReflectionHelper.GetTypeName(GetType()); }
        }

        public void FromGeoJson(string _geoJson)
        {
            GeoPoint point = ParseGeoJson(_geoJson);

            if (point != null)
            {
                X = point.X;
                Y = point.Y;
            }
        }
    }
}
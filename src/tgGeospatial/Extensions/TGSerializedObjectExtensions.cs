using TreeGecko.Library.Common.Objects;
using TreeGecko.Library.Geospatial.Helpers;
using TreeGecko.Library.Geospatial.Interfaces;
using TreeGecko.Library.Geospatial.Objects;

namespace TreeGecko.Library.Geospatial.Extensions
{
    public static class TGSerializedObjectExtensions
    {
        public static GeoPoint GetGeoPoint(this TGSerializedObject _tgs, string _name)
        {
            if (_tgs.Properties.ContainsKey(_name))
            {
                string temp = _tgs.Properties[_name].SerializedValue;
                if (temp != null)
                {
                    GeoPoint gp = GeoPoint.Parse(temp);
                    return gp;
                }
            }

            return null;
        }

        public static void Add(this TGSerializedObject _tgs, string _name, GeoPoint _geoPoint)
        {
            TGSerializedProperty tgsp = new TGSerializedProperty(_name, _geoPoint.ToGeoJson(), true);
            _tgs.Properties.Add(_name, tgsp);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_tgs"></param>
        /// <param name="_name"></param>
        /// <returns></returns>
        public static IGeoJson GetGeometry(this TGSerializedObject _tgs, string _name)
        {
            string temp = _tgs.GetString(_name);

            if (temp != null)
            {
                return GeometryHelper.GetGeometryObject(temp);
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_tgs"></param>
        /// <param name="_name"></param>
        /// <param name="_geometry"></param>
        public static void Add(this TGSerializedObject _tgs, string _name, IGeoJson _geometry)
        {
            if (_geometry != null)
            {
                _tgs.Properties.Add(_name, new TGSerializedProperty(_name,
                    _geometry.ToGeoJson(), true));
            }
            else
            {
                _tgs.Properties.Add(_name, new TGSerializedProperty(_name, null, true));
            }
        }
    }
}
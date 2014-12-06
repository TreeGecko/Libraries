using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using TreeGecko.Library.Common.Helpers;
using TreeGecko.Library.Common.Interfaces;

namespace TreeGecko.Library.Common.Objects
{
    /// <summary>
    /// 
    /// </summary>
    public class TGSerializedObject
    {
        /// <summary>
        /// 
        /// </summary>
        public TGSerializedObject()
        {
            Properties = new Dictionary<string, TGSerializedProperty>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_properties"></param>
        internal TGSerializedObject(Dictionary<string, TGSerializedProperty> _properties)
        {
            Properties = _properties;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_serializedValue"></param>
        public TGSerializedObject(string _serializedValue)
        {
			Deserialize(_serializedValue);
        }

		/// <summary>
		/// Deserialize the specified serializedValue.
		/// </summary>
		/// <param name='_serializedValue'>
		/// Serialized value.
		/// </param>
		private void Deserialize(string _serializedValue)
		{
			Properties = new Dictionary<string, TGSerializedProperty>();
			
			string[] type = _serializedValue.Split("-".ToCharArray());
			
			if (type.Length == 2)
			{
				TGObjectType = type[0];
				
				string[] parts = type[1].Split("|".ToCharArray());
				
				foreach (string part in parts)
				{
					TGSerializedProperty property = new TGSerializedProperty(part);
					
					Properties.Add(property.PropertyName, property);
				}
			}
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_key"></param>
        /// <returns></returns>
        public bool ContainsKey(string _key)
        {
            if (Properties.ContainsKey(_key))
            {
                return true;
            }

            return false;
        }

		/// <summary>
		/// Initializes a new instance of the <see cref="TGSerializedObject"/> class.
		/// </summary>
		/// <param name='_data'>
		/// Data.
		/// </param>
		public TGSerializedObject(byte[] _data)
		{
			string serialized = Encoding.UTF8.GetString(_data);

			Deserialize(serialized);
		}

        /// <summary>
        /// 
        /// </summary>
        public string TGObjectType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Dictionary<string, TGSerializedProperty> Properties { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_name"></param>
        /// <param name="_value"></param>
        public void Add(string _name, string _value)
        {
            Properties.Add(_name, new TGSerializedProperty(_name, _value));
        }

        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_name"></param>
        /// <param name="_value"></param>
        public void Add(string _name, double _value)
        {
            Properties.Add(_name, new TGSerializedProperty(_name, Convert.ToString(_value)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_name"></param>
        /// <param name="_value"></param>
        public void Add(string _name, Int16 _value)
        {
            Properties.Add(_name, new TGSerializedProperty(_name, Convert.ToString(_value)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_name"></param>
        /// <param name="_value"></param>
        public void Add(string _name, DateTime _value)
        {
            Properties.Add(_name, new TGSerializedProperty(_name, DateHelper.ToString(_value)));
        }

		/// <summary>
		/// Add the specified name and value.
		/// </summary>
		/// <param name='_name'>
		/// Name.
		/// </param>
		/// <param name='_value'>
		/// Value.
		/// </param>
		public void Add(string _name, TimeSpan _value)
        {
			long ticks = _value.Ticks;

            Properties.Add(_name, new TGSerializedProperty(_name, Convert.ToString(ticks)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_name"></param>
        /// <param name="_value"></param>
        public void Add(string _name, Int32 _value)
        {
            Properties.Add(_name, new TGSerializedProperty(_name, Convert.ToString(_value)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_name"></param>
        /// <param name="_value"></param>
        public void Add(string _name, UInt64 _value)
        {
            Properties.Add(_name, new TGSerializedProperty(_name, Convert.ToString(_value)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_name"></param>
        /// <param name="_value"></param>
        public void Add(string _name, Enum _value)
        {
            if (_value != null)
            {
                string temp = Enum.GetName(_value.GetType(), _value);
                Properties.Add(_name, new TGSerializedProperty(_name, temp));
            }
            else
            {
                Properties.Add(_name, new TGSerializedProperty(_name, null));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_name"></param>
        /// <param name="_properties"></param>
        public void Add(string _name, List<Property> _properties)
        {
            List<TGSerializedObject> props = new List<TGSerializedObject>();

            foreach (Property property in _properties)
            {
                props.Add(property.GetTGSerializedObject());
            }

            string json = JsonConvert.SerializeObject(props);

            Properties.Add(_name, new TGSerializedProperty(_name, json));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_name"></param>
        /// <param name="_guids"></param>
        public void Add(string _name,
		                List<Guid> _guids)
		{
			StringBuilder sb = new StringBuilder();

			foreach (Guid item in _guids) 
			{
				sb.Append(item);
				sb.Append("|");
			}

			if (sb.Length > 0)
			{
				sb.Remove(sb.Length - 1, 1);
			}

			if (sb.Length > 0)
			{
			 	Add(_name, sb.ToString());
			}
			else
			{
				string temp = null;

				Add(_name, temp);
			}
		}

		public void Add(string _name,
		                List<string> _values)
		{
			StringBuilder sb = new StringBuilder();
			
			foreach (string item in _values) 
			{
				sb.Append(item);
				sb.Append("|");
			}
			
			if (sb.Length > 0)
			{
				sb.Remove(sb.Length - 1, 1);
			}
			
			if (sb.Length > 0)
			{
				Add(_name, sb.ToString());
			}
			else
			{
				string temp = null;
				
				Add(_name, temp);
			}
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_name"></param>
        /// <param name="_value"></param>
        public void Add(string _name, UInt32 _value)
        {
            Properties.Add(_name, new TGSerializedProperty(_name, Convert.ToString(_value)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_name"></param>
        /// <param name="_value"></param>
        public void Add(string _name, Int64 _value)
        {
            Properties.Add(_name, new TGSerializedProperty(_name, Convert.ToString(_value)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_name"></param>
        /// <param name="_value"></param>
        public void Add(string _name, Guid _value)
        {
            Properties.Add(_name, new TGSerializedProperty(_name, Convert.ToString(_value).ToLower()));
        }
		
		/// <summary>
		/// Add the specified name and value.
		/// </summary>
		/// <param name='_name'>
		/// Name.
		/// </param>
		/// <param name='_value'>
		/// Value.
		/// </param>
		public void Add(string _name, Guid? _value)
        {
			if (_value != null)
			{
				Properties.Add(_name, new TGSerializedProperty(_name, _value.Value.ToString().ToLower()));
			}
			else
			{
				Properties.Add(_name, new TGSerializedProperty(_name, null));
			} 
        }

		/// <summary>
		/// Add the specified name and value.
		/// </summary>
		/// <param name='_name'>
		/// Name.
		/// </param>
		/// <param name='_value'>
		/// Value.
		/// </param>
		public void Add(string _name, DateTime? _value)
		{
			if (_value != null)
			{
				Properties.Add(_name, new TGSerializedProperty(_name, Convert.ToString(_value)));
			}
			else
			{
				Properties.Add(_name, new TGSerializedProperty(_name, null));
			} 
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_name"></param>
        /// <param name="_value"></param>
        public void Add(string _name, Int16? _value)
        {
            if (_value != null)
            {
                Properties.Add(_name, new TGSerializedProperty(_name, Convert.ToString(_value)));
            }
            else
            {
                Properties.Add(_name, new TGSerializedProperty(_name, null));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_name"></param>
        /// <param name="_value"></param>
        public void Add(string _name, bool? _value)
        {
            if (_value != null)
            {
                Properties.Add(_name, new TGSerializedProperty(_name, Convert.ToString(_value)));
            }
            else
            {
                Properties.Add(_name, new TGSerializedProperty(_name, null));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_name"></param>
        /// <param name="_value"></param>
        public void Add(string _name, Int32? _value)
        {
            if (_value != null)
            {
                Properties.Add(_name, new TGSerializedProperty(_name, Convert.ToString(_value)));
            }
            else
            {
                Properties.Add(_name, new TGSerializedProperty(_name, null));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_name"></param>
        /// <param name="_value"></param>
        public void Add(string _name, Double? _value)
        {
            if (_value != null)
            {
                Properties.Add(_name, new TGSerializedProperty(_name, Convert.ToString(_value)));
            }
            else
            {
                Properties.Add(_name, new TGSerializedProperty(_name, null));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_name"></param>
        /// <param name="_value"></param>
        public void Add(string _name, Int64? _value)
        {
            if (_value != null)
            {
                Properties.Add(_name, new TGSerializedProperty(_name, Convert.ToString(_value)));
            }
            else
            {
                Properties.Add(_name, new TGSerializedProperty(_name, null));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_name"></param>
        /// <param name="_values"></param>
        public void Add<T>(string _name, List<T> _values) where T:ITGSerializable, new()
        {
            TGListWrapper<T> wrapper = new TGListWrapper<T>(_values);

            TGSerializedObject obj = wrapper.GetTGSerializedObject();
            Properties.Add(_name, new TGSerializedProperty(_name, obj.ToString()));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_name"></param>
        /// <param name="_data"></param>
        public void Add(string _name, byte[] _data)
        {
            if (_data != null)
            {
                Properties.Add(_name, new TGSerializedProperty(_name, Convert.ToBase64String(_data)));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_name"></param>
        /// <param name="_value"></param>
        public void Add(string _name, bool _value)
        {
            Properties.Add(_name, new TGSerializedProperty(_name, Convert.ToString(_value)));
        }

        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_name"></param>
        /// <returns></returns>
        public string GetString(string _name)
        {
            if (Properties.ContainsKey(_name))
            {
                return Properties[_name].SerializedValue;
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_name"></param>
        /// <returns></returns>
        public Double GetDouble(string _name)
        {
            if (Properties.ContainsKey(_name))
            {
                string temp = Properties[_name].SerializedValue;

                return Convert.ToDouble(temp);
            }

            return 0.0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_name"></param>
        /// <returns></returns>
        public bool GetBoolean(string _name)
        {
            if (Properties.ContainsKey(_name))
            {
                string temp = Properties[_name].SerializedValue;

                return Convert.ToBoolean(temp);
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_name"></param>
        /// <returns></returns>
        public DateTime GetDateTime(string _name)
        {
            if (Properties.ContainsKey(_name))
            {
                string temp = Properties[_name].SerializedValue;

                return Convert.ToDateTime(temp);
            }

            return DateTime.MinValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_name"></param>
        /// <returns></returns>
        public DateTime GetUTCDateTime(string _name)
        {
            return DateTime.SpecifyKind(GetDateTime(_name), DateTimeKind.Utc);
        }
        
		/// <summary>
		/// Gets the time span.
		/// </summary>
		/// <returns>
		/// The time span.
		/// </returns>
		/// <param name='_name'>
		/// Name.
		/// </param>
		public TimeSpan GetTimeSpan(string _name)
        {
            if (Properties.ContainsKey(_name))
            {
                string temp = Properties[_name].SerializedValue;

                long ticks = Convert.ToInt64(temp);

				return new TimeSpan(ticks);
            }

            return new TimeSpan(0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_name"></param>
        /// <returns></returns>
        public Int16 GetInt16(string _name)
        {
            if (Properties.ContainsKey(_name))
            {
                string temp = Properties[_name].SerializedValue;

                return Convert.ToInt16(temp);
            }

            return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_name"></param>
        /// <returns></returns>
        public Int32 GetInt32(string _name)
        {
            if (Properties.ContainsKey(_name))
            {
                string temp = Properties[_name].SerializedValue;

                return Convert.ToInt32(temp);
            }

            return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_name"></param>
        /// <returns></returns>
        public Int64 GetInt64(string _name)
        {
            if (Properties.ContainsKey(_name))
            {
                string temp = Properties[_name].SerializedValue;

                return Convert.ToInt64(temp);
            }

            return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_name"></param>
        /// <returns></returns>
        public UInt32 GetUInt32(string _name)
        {
            if (Properties.ContainsKey(_name))
            {
                string temp = Properties[_name].SerializedValue;

                return Convert.ToUInt32(temp);
            }

            return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_name"></param>
        /// <returns></returns>
        public UInt64 GetUInt64(string _name)
        {
            if (Properties.ContainsKey(_name))
            {
                string temp = Properties[_name].SerializedValue;

                return Convert.ToUInt64(temp);
            }

            return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_name"></param>
        /// <returns></returns>
        public byte[] GetBytes(string _name)
        {
            if (Properties.ContainsKey(_name))
            {
                string temp = Properties[_name].SerializedValue;

                return Convert.FromBase64String(temp);
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_name"></param>
        /// <returns></returns>
        public Guid GetGuid(string _name)
        {
            if (Properties.ContainsKey(_name))
            {
                string temp = Properties[_name].SerializedValue;

				if (!string.IsNullOrEmpty(temp))
				{
                	return new Guid(temp);
				}
            }

            return Guid.Empty;
        }

        public Enum GetEnum(string _name, Type _type, Enum _default)
        {
            if (Properties.ContainsKey(_name))
            {
                string temp = Properties[_name].SerializedValue;

                if (!string.IsNullOrEmpty(temp))
                {
                    return (Enum)Enum.Parse(_type, temp);
                }
            }

            return _default;
        }

        public Enum GetNullableEnum(string _name, Type _type)
        {
            if (Properties.ContainsKey(_name))
            {
                string temp = Properties[_name].SerializedValue;

                if (!string.IsNullOrEmpty(temp))
                {
                    return (Enum) Enum.Parse(_type, temp);
                }
            }

            return null;
        }

        public Guid? GetNullableGuid(string _name)
        {
            if (Properties.ContainsKey(_name))
            {
                string temp = Properties[_name].SerializedValue;

				if (!string.IsNullOrEmpty(temp))
				{
                	return new Guid(temp);
				}
            }

            return null;
        }

		/// <summary>
		/// Gets the nullable date time.
		/// </summary>
		/// <returns>
		/// The nullable date time.
		/// </returns>
		/// <param name='_name'>
		/// Name.
		/// </param>
		public DateTime? GetNullableDateTime(string _name)
		{
			if (Properties.ContainsKey(_name))
			{
				string temp = Properties[_name].SerializedValue;

				if (!string.IsNullOrEmpty(temp))
				{
					return Convert.ToDateTime(temp);
				}
			}
			
			return null;
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_name"></param>
        /// <returns></returns>
        public Int16? GetNullableInt16(string _name)
        {
            if (Properties.ContainsKey(_name))
            {
                string temp = Properties[_name].SerializedValue;

                if (!string.IsNullOrEmpty(temp))
                {
                    return Convert.ToInt16(temp);
                }
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_name"></param>
        /// <returns></returns>
        public Int32? GetNullableInt32(string _name)
        {
            if (Properties.ContainsKey(_name))
            {
                string temp = Properties[_name].SerializedValue;

                if (!string.IsNullOrEmpty(temp))
                {
                    return Convert.ToInt32(temp);
                }
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_name"></param>
        /// <returns></returns>
        public Int64? GetNullableInt64(string _name)
        {
            if (Properties.ContainsKey(_name))
            {
                string temp = Properties[_name].SerializedValue;

                if (!string.IsNullOrEmpty(temp))
                {
                    return Convert.ToInt64(temp);
                }
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_name"></param>
        /// <returns></returns>
        public Double? GetNullableDouble(string _name)
        {
            if (Properties.ContainsKey(_name))
            {
                string temp = Properties[_name].SerializedValue;

                if (!string.IsNullOrEmpty(temp))
                {
                    return Convert.ToDouble(temp);
                }
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_name"></param>
        /// <returns></returns>
        public bool? GetNullableBoolean(string _name)
        {
            if (Properties.ContainsKey(_name))
            {
                string temp = Properties[_name].SerializedValue;

                if (!string.IsNullOrEmpty(temp))
                {
                    return Convert.ToBoolean(temp);
                }
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_name"></param>
        /// <returns></returns>
        public List<T> GetListITGSerializableObject<T>(string _name) where T : ITGSerializable, new()
        {
            if (Properties.ContainsKey(_name))
            {
                string temp = Properties[_name].SerializedValue;
                TGListWrapper<T> wrapper = new TGListWrapper<T>(temp);
                return wrapper.Values;
            }

            return new List<T>();
        }

        public List<Property> GetProperties(string _name)
        {
            if (Properties.ContainsKey(_name))
            {
                string temp = Properties[_name].SerializedValue;

                List<TGSerializedObject> props = JsonConvert.DeserializeObject<List<TGSerializedObject>>(temp);

                List<Property> output = new List<Property>();
                foreach (TGSerializedObject bcn in props)
                {
                    output.Add(GetTGSerializable<Property>(bcn));
                }

                return output;
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_name"></param>
        /// <returns></returns>
        public T GetITGSerializableObject<T>(string _name) where T : ITGSerializable, new()
        {
            if (Properties.ContainsKey(_name))
            {
                T obj = new T();
                TGSerializedObject temp = new TGSerializedObject(Properties[_name].SerializedValue);
                obj.LoadFromTGSerializedObject(temp);
                return obj;
            }
            return default(T);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string ToJSON()
        {
            string json = JsonConvert.SerializeObject(Properties);

            return json;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(TGObjectType + "-");

            foreach (KeyValuePair<string, TGSerializedProperty> kvp in Properties)
            {
                TGSerializedProperty property = kvp.Value;

                sb.Append(property);
                sb.Append("|");
            }

            if (Properties.Count > 0)
            {
                sb.Remove(sb.Length - 1, 1);
            }

            return sb.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public byte[] ToBytes()
        {
            string serialized = ToString();

            return Encoding.UTF8.GetBytes(serialized);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_data"></param>
        /// <returns></returns>
        public static TGSerializedObject FromBytes(byte[] _data)
        {
            string serialized = Encoding.UTF8.GetString(_data);

            return new TGSerializedObject(serialized);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_json"></param>
        /// <returns></returns>
        public static TGSerializedObject FromJSON(string _json)
        {
            Dictionary<string, TGSerializedProperty> temp = JsonConvert.DeserializeObject<Dictionary<string, TGSerializedProperty>>(_json);

            TGSerializedObject tg = new TGSerializedObject(temp);

            return tg;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_serialized"></param>
        /// <returns></returns>
        public static T GetTGSerializable<T>(string _serialized) where T: ITGSerializable, new()
        {
            T obj = new T();

            TGSerializedObject tg = new TGSerializedObject(_serialized);

            obj.LoadFromTGSerializedObject(tg);

            return obj;
        }

        public static T GetTGSerializable<T>(TGSerializedObject _tg) where T : ITGSerializable, new()
        {
            T obj = new T();

            obj.LoadFromTGSerializedObject(_tg);

            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_data"></param>
        /// <returns></returns>
        public static T GetTGSerializable<T>(byte[] _data) where T : ITGSerializable, new()
        {
            string serialized = Encoding.UTF8.GetString(_data);

            return GetTGSerializable<T>(serialized);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_name"></param>
        /// <returns></returns>
        public List<Guid> GetListOfGuids(string _name)
        {
            List<Guid> guids = new List<Guid>();

            string temp = GetString(_name);

            if (temp != null)
            {
                string[] stringGuids = temp.Split("|".ToCharArray(),
                                                  StringSplitOptions.RemoveEmptyEntries);

                foreach (string stringGuid in stringGuids)
                {
                    guids.Add(new Guid(stringGuid));
                }
            }

            return guids;
        }

		public List<string> GetListOfStrings(string _name)
		{
			List<string> values = new List<string>();

			string temp = GetString(_name);
			
			if (temp != null)
			{
				string[] valueArray = temp.Split("|".ToCharArray(),
				                                  StringSplitOptions.RemoveEmptyEntries);
				
				foreach (string valueItem in valueArray)
				{
					values.Add(valueItem);
				}
			}
			
			return values;
		}
    }
}
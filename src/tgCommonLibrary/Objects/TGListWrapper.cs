using System;
using System.Collections.Generic;
using TreeGecko.Library.Common.Helpers;
using TreeGecko.Library.Common.Interfaces;

namespace TreeGecko.Library.Common.Objects
{
    public class TGListWrapper<T> : ITGSerializable
        where T : ITGSerializable, new()
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_values"></param>
        public TGListWrapper(List<T> _values)
        {
            Values = _values;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_serializedValue"></param>
        public TGListWrapper(string _serializedValue)
        {
            Values = new List<T>();
            TGSerializedObject obj = new TGSerializedObject(_serializedValue);
            LoadFromTGSerializedObject(obj);
        }

        /// <summary>
        /// 
        /// </summary>
        public List<T> Values { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public TGSerializedObject GetTGSerializedObject()
        {
            TGSerializedObject obj = new TGSerializedObject();

            //TODO - Resolve issues or remove.
            //int i = 0;

            //foreach (T value in Values)
            //{
            //    obj.Add(Convert.ToString(i), value);
            //}

            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_tg"></param>
        public void LoadFromTGSerializedObject(TGSerializedObject _tg)
        {
            int itemCount = _tg.Properties.Count;

            for (int i = 0; i < itemCount; i++)
            {
                T value = _tg.GetITGSerializableObject<T>(Convert.ToString(i));
                Values.Add(value);
            }
        }

        public string TGObjectType
        {
            get { return ReflectionHelper.GetTypeName(GetType()); }
        }
    }
}
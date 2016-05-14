using System.Collections.Generic;
using TreeGecko.Library.Common.Helpers;
using TreeGecko.Library.Common.Interfaces;

namespace TreeGecko.Library.Common.Objects
{
    /// <summary>
    /// 
    /// </summary>
    public class AbstractTGList<T> : List<T>, ITGSerializable where T : ITGSerializable, new()
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_tg"></param>
        public void LoadFromTGSerializedObject(TGSerializedObject _tg)
        {
            int recordCount = _tg.GetInt32("RecordCount");

            for (int i = 0; i < recordCount; i++)
            {
                string serialized = _tg.GetString(i.ToString());
                T item = TGSerializedObject.GetTGSerializable<T>(serialized);
                Add(item);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public TGSerializedObject GetTGSerializedObject()
        {
            TGSerializedObject tg = new TGSerializedObject {TGObjectType = TGObjectType};

            int i = 0;

            foreach (T item in this)
            {
                tg.Add(i.ToString(), item.GetTGSerializedObject().ToString());
                i++;
            }

            tg.Add("RecordCount", i);

            return tg;
        }

        public string TGObjectType
        {
            get { return ReflectionHelper.GetTypeName(GetType()); }
        }

        public override string ToString()
        {
            TGSerializedObject obj = GetTGSerializedObject();

            return obj.ToString();
        }
    }
}
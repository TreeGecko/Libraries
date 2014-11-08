using TreeGecko.Library.Common.Enums;
using TreeGecko.Library.Common.Helpers;
using TreeGecko.Library.Common.Interfaces;

namespace TreeGecko.Library.Common.Objects
{
    public class Property : ITGSerializable
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public AttributeTypes? AttributeType { get; set; } 

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public TGSerializedObject GetTGSerializedObject()
        {
            TGSerializedObject tg = new TGSerializedObject {TGObjectType = TGObjectType};

            tg.Add("Name", Name);
            tg.Add("Value", Value);
            tg.Add("AttributeType",  AttributeType);

            return tg;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_tg"></param>
        public void LoadFromTGSerializedObject(TGSerializedObject _tg)
        {
            Name = _tg.GetString("Name");
            Value = _tg.GetString("Value");
            AttributeType = (AttributeTypes) _tg.GetNullableEnum("AttributeType", 
                typeof(AttributeTypes));
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

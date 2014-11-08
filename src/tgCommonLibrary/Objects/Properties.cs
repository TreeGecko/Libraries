using System;
using System.Collections.Generic;
using TreeGecko.Library.Common.Enums;
using TreeGecko.Library.Common.Helpers;
using TreeGecko.Library.Common.Interfaces;

namespace TreeGecko.Library.Common.Objects
{
    public class Properties : List<Property>,  ITGSerializable
    {
        public Property FindProperty(string _name)
        {
            foreach (Property property in this)
            {
                if (property.Name.Equals(_name, StringComparison.InvariantCulture))
                {
                    return property;
                }
            }

            return null;
        }

        public void SetValue(string _name, string _value, AttributeTypes? _attributeType)
        {
            Property prop = FindProperty(_name);

            if (prop != null)
            {
                //We don't store null values
                if (_value != null)
                {
                    prop.Value = _value;
                    prop.AttributeType = _attributeType;
                }
                else
                {
                    //Since the new value is null remove this item
                    Remove(prop);
                }
            }
            else
            {
                //We don't store null values
                if (_value != null)
                {
                    prop = new Property
                    {
                        Name = _name, 
                        Value = _value, 
                        AttributeType = _attributeType
                    };
                    
                    Add(prop);
                }
            }
        }

        public TGSerializedObject GetTGSerializedObject()
        {
            TGSerializedObject tg = new TGSerializedObject {TGObjectType = TGObjectType};
            tg.Add("PropertyCount", Count);

            int i = 0;

            foreach (Property property in this)
            {
                tg.Add(i.ToString(), property.ToString());

                i++;
            }

            return tg;
        }

        public void LoadFromTGSerializedObject(TGSerializedObject _tg)
        {
            int? propertyCount = _tg.GetNullableInt32("PropertyCount");

            if (propertyCount != null)
            {
                for (int i = 0; i < propertyCount.Value; i++)
                {
                    string temp = _tg.GetString(i.ToString());

                    Property p = TGSerializedObject.GetTGSerializable<Property>(temp);
                    Add(p);
                }
            }
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

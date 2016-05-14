using System;
using TreeGecko.Library.Common.Enums;

namespace TreeGecko.Library.Common.Objects
{
    public class Attribute : AbstractTGObject
    {
        public string Name { get; set; }

        public string Value { get; set; }

        public AttributeTypes AttributeType { get; set; }

        public override TGSerializedObject GetTGSerializedObject()
        {
            TGSerializedObject tg = base.GetTGSerializedObject();

            tg.Add("Name", Name);
            tg.Add("Value", Value);
            tg.Add("AttributeType", Enum.GetName(typeof(AttributeTypes), AttributeType));

            return tg;
        }

        public override void LoadFromTGSerializedObject(TGSerializedObject _tg)
        {
            base.LoadFromTGSerializedObject(_tg);

            Name = _tg.GetString("Name");
            Value = _tg.GetString("Value");

            string temp = _tg.GetString("AttributeType");
            AttributeType = (AttributeTypes) Enum.Parse(typeof(AttributeTypes), temp);
        }
    }
}
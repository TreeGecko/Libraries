using System;
using System.Collections.Generic;
using System.Text;
using TreeGecko.Library.Common.Interfaces;

namespace TreeGecko.Library.Common.Objects
{
    /// <summary>
    /// Attributes.
    /// </summary>
    public class Attributes : List<Attribute>, ITGSerializable
    {
        /// <summary>
        /// Gets or sets the attribute definition GUID.
        /// </summary>
        /// <value>
        /// The attribute definition GUID.
        /// </value>
        public Guid AttributeDefinitionGuid { get; set; }


        public TGSerializedObject GetTGSerializedObject()
        {
            StringBuilder sb = new StringBuilder();

            string value = null;

            if (Count > 0)
            {
                foreach (Attribute attribute in this)
                {
                    TGSerializedObject tg = attribute.GetTGSerializedObject();
                    sb.Append(tg);
                    sb.Append("||");
                }

                if (sb.Length > 2)
                {
                    sb.Remove(sb.Length - 2, 2);
                }

                value = sb.ToString();
            }

            TGSerializedObject returnValue = new TGSerializedObject();
            returnValue.Add("AttributeDefinitionGuid", AttributeDefinitionGuid);
            returnValue.Add("Attributes", value);
            return returnValue;
        }

        /// <summary>
        /// Loads from TG serilizable object.
        /// </summary>
        /// <param name='_tg'>
        /// Bcn.
        /// </param>
        public void LoadFromTGSerializedObject(TGSerializedObject _tg)
        {
            AttributeDefinitionGuid = _tg.GetGuid("AttributeDefinitionGuid");
            string value = _tg.GetString("Attributes");

            if (value != null)
            {
                string[] temp = value.Split(new[] {"||"}, StringSplitOptions.RemoveEmptyEntries);

                foreach (string item in temp)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        Attribute attribute = new Attribute();
                        attribute.LoadFromTGSerializedString(item);

                        Add(attribute);
                    }
                }
            }
        }

        public string TGObjectType
        {
            get { return null; }
        }

        public override string ToString()
        {
            TGSerializedObject tg = GetTGSerializedObject();
            return tg.ToString();
        }
    }
}
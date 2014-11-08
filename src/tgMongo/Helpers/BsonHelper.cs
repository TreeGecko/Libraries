using System;
using System.Collections.Generic;
using MongoDB.Bson;
using TreeGecko.Library.Common.Objects;

namespace TreeGecko.Library.Mongo.Helpers
{
    public static class BsonHelper
    {
        public static BsonDocument Get<T>(T _obj) where T : AbstractTGObject
        {
            BsonDocument doc = new BsonDocument();

            TGSerializedObject bcn = _obj.GetTGSerializedObject();

            foreach (KeyValuePair<string, TGSerializedProperty> kvp in bcn.Properties)
            {
                TGSerializedProperty prop = kvp.Value;

                if (prop.PropertyName.Equals("guid", StringComparison.InvariantCultureIgnoreCase))
                {
                    Guid temp = new Guid(prop.SerializedValue);

                    doc.Add("_id", new BsonBinaryData(temp.ToByteArray()));
                    doc.Add("Guid", new BsonString(prop.SerializedValue));
                }
                else if (prop.IsChildObject)
                {
                    string temp = prop.SerializedValue;
                    BsonDocument subDoc = BsonDocument.Parse(temp);
                    doc.Add(prop.PropertyName, subDoc);
                }
                else
                {
                    doc.Add(prop.PropertyName, prop.SerializedValue);
                }
            }

            return doc;
        }

        public static T Get<T>(BsonDocument _document) where T : AbstractTGObject, new()
        {
            if (_document != null)
            {
                TGSerializedObject tso = new TGSerializedObject();

                foreach (string name in _document.Names)
                {
                    if (!name.Equals("_id"))
                    {
                        BsonValue val = _document[name];

                        TGSerializedProperty serializedProperty = new TGSerializedProperty(name, val.ToString());
                        tso.Properties.Add(name, serializedProperty);
                    }
                }

                return TGSerializedObject.GetTGSerializable<T>(tso);
            }

            return default(T);
        }

    }
}

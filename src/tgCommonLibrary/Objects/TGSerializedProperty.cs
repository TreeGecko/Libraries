using TreeGecko.Library.Common.Helpers;

namespace TreeGecko.Library.Common.Objects
{
    public class TGSerializedProperty
    {
        public TGSerializedProperty(string _serialized)
        {
            string[] parts = _serialized.Split(":".ToCharArray());

            if (parts.Length ==2)
            {
                PropertyName = StringHelper.GetStringFromBase64(parts[0]);
                SerializedValue = StringHelper.GetStringFromBase64(parts[1]);
            }
        }

        public TGSerializedProperty(string _propertyName, string _serializedValue, bool _isChildObject = false)
        {
            PropertyName = _propertyName;
            SerializedValue = _serializedValue;
            IsChildObject = _isChildObject;
        }

        public string PropertyName { get; set; }
        public string SerializedValue { get; set; }
        public bool IsChildObject { get; set; }

        public override string ToString()
        {
            return string.Format("{0}:{1}",  StringHelper.GetBase64(PropertyName), StringHelper.GetBase64(SerializedValue)); 
        }

    }
}

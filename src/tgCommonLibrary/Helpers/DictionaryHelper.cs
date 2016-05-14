using System;
using System.Collections.Generic;
using TreeGecko.Library.Common.Objects;

namespace TreeGecko.Library.Common.Helpers
{
    public static class DictionaryHelper
    {
        public static Dictionary<Guid, T> GetDictionary<T>(List<T> _values)
            where T : AbstractTGObject
        {
            Dictionary<Guid, T> dictionary = new Dictionary<Guid, T>();

            foreach (T item in _values)
            {
                dictionary.Add(item.Guid, item);
            }

            return dictionary;
        }

        public static Dictionary<string, T> GetNamedObjectDictionary<T>(List<T> _values)
            where T : NamedObject
        {
            Dictionary<string, T> dictionary = new Dictionary<string, T>();

            foreach (T item in _values)
            {
                dictionary.Add(item.Name, item);
            }

            return dictionary;
        }
    }
}
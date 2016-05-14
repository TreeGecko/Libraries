using System;
using System.Collections.Generic;
using System.Text;
using TreeGecko.Library.Common.Objects;

namespace TreeGecko.Library.Common.Helpers
{
    public static class ListHelper
    {
        public static T FindValue<T>(Guid _guid, List<T> _values)
            where T : AbstractTGObject
        {
            foreach (T item in _values)
            {
                if (item.Guid.Equals(_guid))
                {
                    return item;
                }
            }

            return null;
        }

        public static int FindIndex<T>(Guid _guid, List<T> _values)
            where T : AbstractTGObject
        {
            if (_values != null)
            {
                foreach (T item in _values)
                {
                    if (item.Guid.Equals(_guid))
                    {
                        return _values.IndexOf(item);
                    }
                }
            }

            return -1;
        }

        public static string Combine(List<Guid> _guids)
        {
            StringBuilder sb = new StringBuilder();

            foreach (Guid guid in _guids)
            {
                sb.Append(guid.ToString());
                sb.Append("|");
            }

            if (sb.Length > 0)
            {
                sb.Remove(sb.Length - 1, 1);
            }

            return sb.ToString();
        }

        public static List<Guid> SplitGuidsToList(string _guids)
        {
            string[] temp = _guids.Split("|".ToCharArray());

            List<Guid> guidList = new List<Guid>();

            foreach (string item in temp)
            {
                if (GuidHelper.IsValidGuidString(item))
                {
                    Guid guid = new Guid(item);

                    guidList.Add(guid);
                }
            }

            return guidList;
        }
    }
}
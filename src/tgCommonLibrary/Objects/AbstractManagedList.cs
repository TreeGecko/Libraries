using System;
using System.Collections;
using System.Collections.Generic;
using TreeGecko.Library.Common.Helpers;

namespace TreeGecko.Library.Common.Objects
{
    [Serializable]
    public abstract class AbstractManagedList : ArrayList
    {
        public abstract Type ManagedType();
        public abstract List<T> GetGenericList<T>();

        public override int Add(object _item)
        {
            Type itemType = _item.GetType();

            if (ReflectionHelper.IsRelated(itemType, ManagedType()))
            {
                return base.Add(_item);
            }
            
            string error = string.Format("You cannot add type {0} to this array list.  Expecting type {1}", typeof(object).FullName, ManagedType().FullName);
            throw new Exception(error);
        }

    }
}

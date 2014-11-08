using System.Collections.Generic;
using TreeGecko.Library.Common.Objects;

namespace TreeGecko.Library.Common.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IExporter
    {
        void Export(string _fileName, List<TGSerializedObject> _items);
        void Export(string _fileName, List<ITGSerializable> _items);
    }
}

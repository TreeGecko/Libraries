using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TreeGecko.Library.HDF5.Interfaces
{
    public interface IGroup
    {
        List<string> GetChildDatasetNames();
        List<string> GetChildGroupNames();
    }
}

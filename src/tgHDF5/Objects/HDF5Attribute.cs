using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HDF5DotNet;
using TreeGecko.Library.HDF5.Helpers;

namespace TreeGecko.Library.HDF5.Objects
{
    public class HDF5Attribute : IDisposable
    {
        private H5ObjectWithAttributes m_ParentObjectId;
        private readonly H5AttributeId m_AttributeId;
        private H5AttributeInfo m_AttributeInfo;

        internal HDF5Attribute(H5ObjectWithAttributes _parentObjectId, string _name)
        {
            m_ParentObjectId = _parentObjectId;
            m_AttributeId = H5A.open(_parentObjectId, _name);
            m_AttributeInfo = H5A.getInfo(m_AttributeId);
        }

        public Int32 AsInt32()
        {
            return HDF5Helper.AttributeAsInt32(m_AttributeId);
        }

        public Double AsDouble()
        {
            return HDF5Helper.AttributeAsDouble(m_AttributeId);
        }

        public string AsString()
        {
            return HDF5Helper.AttributeAsString(m_AttributeId);
        }

        public void Dispose()
        {
            H5A.close(m_AttributeId);
        }
    }
}

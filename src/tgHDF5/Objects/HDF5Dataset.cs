using System;
using HDF5DotNet;
using TreeGecko.Library.HDF5.Interfaces;

namespace TreeGecko.Library.HDF5.Objects
{
    public class HDF5Dataset : IDisposable, IHasAttributes
    {
        private H5FileOrGroupId m_ParentObjectID;
        private readonly H5DataSetId m_DatasetId;
        

        internal HDF5Dataset(H5FileOrGroupId _parentObjectId, string _name)
        {
            m_ParentObjectID = _parentObjectId;
            m_DatasetId = H5D.open(_parentObjectId, _name);
        }

        public void Dispose()
        {
            H5D.close(m_DatasetId);
        }

        public HDF5Attribute GetAttribute(string _attributeName)
        {
            return new HDF5Attribute(m_DatasetId, _attributeName);
        }

        public void DeleteAttribute(string _attributeName)
        {
            H5A.Delete(m_DatasetId, _attributeName);
        }
    }
}

using System;
using System.Collections.Generic;
using HDF5DotNet;
using TreeGecko.Library.HDF5.Helpers;
using TreeGecko.Library.HDF5.Interfaces;

namespace TreeGecko.Library.HDF5.Objects
{
    public class HDF5Group : IDisposable, IHasAttributes, IGroup
    {
        private H5FileId m_FileId;
        private readonly H5GroupId m_GroupId;

        internal HDF5Group(H5FileId _fileId, string _groupName)
        {
            m_FileId = _fileId;
            m_GroupId = H5G.open(_fileId, _groupName);
        }

        public HDF5Attribute GetAttribute(string _attributeName)
        {
            return new HDF5Attribute(m_GroupId, _attributeName);
        }

        public void DeleteAttribute(string _attributeName)
        {
            H5A.Delete(m_GroupId, _attributeName);
        }

        public HDF5Dataset GetDataset(string _datasetName)
        {
            return new HDF5Dataset(m_GroupId, _datasetName);
        }

        public List<string> GetChildGroupNames()
        {
            return HDF5Helper.GetChildGroupNames(m_GroupId);
        }

        public List<string> GetChildDatasetNames()
        {
            return HDF5Helper.GetChildDatasetNames(m_GroupId);
        }

        public long ChildObjectCount
        {
            get
            {
                return H5G.getNumObjects(m_GroupId);
            }
        }

        public void Dispose()
        {
            H5G.close(m_GroupId);
        }

        
    }
}

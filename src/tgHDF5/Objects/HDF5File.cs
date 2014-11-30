using System;
using System.Collections.Generic;
using HDF5DotNet;
using TreeGecko.Library.HDF5.Helpers;
using TreeGecko.Library.HDF5.Interfaces;

namespace TreeGecko.Library.HDF5.Objects
{
    public class HDF5File : IDisposable
    {
        private string m_FileName;
        private readonly H5FileId m_FileId;

        public HDF5File(string _file, bool _readonly)
        {
            m_FileName = _file;

            if (_readonly)
            {
                m_FileId = H5F.open(_file, H5F.OpenMode.ACC_RDONLY);
            }
            else
            {
                m_FileId = H5F.open(_file, H5F.OpenMode.ACC_RDWR);
            }
        }

        

        public HDF5Group GetGroup(string _groupName)
        {
            HDF5Group group = new HDF5Group(m_FileId, _groupName);
            return group;
        }

        public void Dispose()
        {
            H5F.close(m_FileId);
        }
    }
}

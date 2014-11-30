using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using HDF5DotNet;
using TreeGecko.Library.HDF5.Structs;

namespace TreeGecko.Library.HDF5.Helpers
{
    public static class HDF5Helper
    {
        public static List<string> GetChildDatasetNames(H5GroupId _groupId)
        {
            List<string> names = new List<string>();

            ulong count = (ulong)H5G.getNumObjects(_groupId); ;

            for (ulong i = 0; i < count; i++)
            {
                string name = H5G.getObjectNameByIndex(_groupId, i);

                ObjectInfo info = H5G.getObjectInfo(_groupId, name, false);
                if (info.objectType == H5GType.DATASET)
                {
                    names.Add(name);
                }
            }

            return names;
        }

        public static List<string> GetChildGroupNames(H5GroupId _groupId)
        {
            List<string> names = new List<string>();

            ulong count = (ulong)H5G.getNumObjects(_groupId); ;

            for (ulong i = 0; i < count; i++)
            {
                string name = H5G.getObjectNameByIndex(_groupId, i);

                ObjectInfo info = H5G.getObjectInfo(_groupId, name, false);
                if (info.objectType == H5GType.GROUP)
                {
                    names.Add(name);
                }
            }

            return names;
        }

        public static int AttributeAsInt32(H5AttributeId _attributeId)
        {
            H5DataTypeId attributeType = H5T.copy(H5T.H5Type.NATIVE_INT);
            int[] value = new int[1];
            H5A.read<int>(_attributeId, attributeType, new H5Array<int>(value));

            return value[0];
        }

        public static Double AttributeAsDouble(H5AttributeId _attributeId)
        {
            H5DataTypeId attributeType = H5T.copy(H5T.H5Type.NATIVE_DOUBLE);
            double[] value = new double[1];
            H5A.read<double>(_attributeId, attributeType, new H5Array<double>(value));

            return value[0];
        }

        public static string AttributeAsString(H5AttributeId _attributeId)
        {
            H5DataTypeId dataTypeId = H5A.getType(_attributeId);
            bool isVariableLength = H5T.isVariableString(dataTypeId);

            if (isVariableLength)
            {
                // Variable length string attribute
                // NOTE: This section only works if the array length is 1
                VariableLengthString[] value = new VariableLengthString[1];
                H5A.read<VariableLengthString>(_attributeId, dataTypeId, 
                    new H5Array<VariableLengthString>(value));

                return value[0].ToString();
            }
            else
            {
                // Make length smaller so null termination character is not read
                int length = (int)H5T.getSize(dataTypeId) - 1;

                // Fixed length string attribute
                byte[] valueBytes = new byte[length];

                H5A.read<byte>(_attributeId, dataTypeId, new H5Array<byte>(valueBytes));
                string value = System.Text.ASCIIEncoding.ASCII.GetString(valueBytes);
                return value;
            }
        }
    }

    

}

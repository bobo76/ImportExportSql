using System.Collections.Generic;
using System.Linq;

namespace ImportExportSql
{
    public static class DataTypeFactory
    {
        private static List<IDataType> DataTypeList = new List<IDataType>();
        static DataTypeFactory()
        {
            DataTypeList = new List<IDataType>();
        }

        public static void AddDataType(IDataType[] newdataType)
        {
            DataTypeList.AddRange(newdataType);
        }

        public static IDataType GetDataType(string typeName)
        {
            return DataTypeList.FirstOrDefault(dt => dt.DataTypeName == typeName);
        }
    }
}

using System;
using System.Data.SqlClient;

namespace ImportExportSql.ColumnType
{
    public class ColumnTypeUniqueidentifier : ColumnTypeBase, IDataType
    {
        public string DataTypeName { get; } = "uniqueidentifier";
        public IDataType CreateInstance()
        {
            return new ColumnTypeUniqueidentifier();
        }

        public SqlParameter CreateParemeter(string parameterName)
        {
            return new SqlParameter(parameterName, System.Data.SqlDbType.UniqueIdentifier);
        }
        internal override object ReadValueObject(SqlDataReader dr, int ix)
        {
            return dr.GetGuid(ix);
        }
        internal override object ReadValueObject(string value)
        {
            return Guid.Parse(value);
        }
    }
}

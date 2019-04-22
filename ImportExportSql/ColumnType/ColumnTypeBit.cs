using System;
using System.Data.SqlClient;

namespace ImportExportSql.ColumnType
{
    public class ColumnTypeBit : ColumnTypeBase, IDataType
    {
        public string DataTypeName { get; } = "bit";
        public IDataType CreateInstance()
        {
            return new ColumnTypeBit();
        }
        public SqlParameter CreateParemeter(string parameterName)
        {
            return new SqlParameter(parameterName, System.Data.SqlDbType.Bit);
        }

        internal override object ReadValueObject(SqlDataReader dr, int ix)
        {
            return dr.GetSqlBoolean(ix);
        }
        internal override object ReadValueObject(string value)
        {
            return bool.Parse(value);
        }
    }
}

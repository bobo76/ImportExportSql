using System.Data.SqlClient;

namespace ImportExportSql.ColumnType
{
    public class ColumnTypeSmallint : ColumnTypeBase, IDataType
    {
        public string DataTypeName { get; } = "smallint";
        public IDataType CreateInstance()
        {
            return new ColumnTypeSmallint();
        }

        public SqlParameter CreateParemeter(string parameterName)
        {
            return new SqlParameter(parameterName, System.Data.SqlDbType.SmallInt);
        }
        internal override object ReadValueObject(SqlDataReader dr, int ix)
        {
            return dr.GetInt16(ix);
        }
        internal override object ReadValueObject(string value)
        {
            return short.Parse(value);
        }
    }
}

using System.Data.SqlClient;

namespace ImportExportSql.ColumnType
{
    public class ColumnTypeTinyInt : ColumnTypeBase, IDataType
    {
        public string DataTypeName { get; } = "tinyint";
        public IDataType CreateInstance()
        {
            return new ColumnTypeTinyInt();
        }

        public SqlParameter CreateParemeter(string parameterName)
        {
            return new SqlParameter(parameterName, System.Data.SqlDbType.TinyInt);
        }
        internal override object ReadValueObject(SqlDataReader dr, int ix)
        {
            return dr.GetByte(ix);
        }
        internal override object ReadValueObject(string value)
        {
            return byte.Parse(value);
        }
    }
}

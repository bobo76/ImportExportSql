using System.Data.SqlClient;

namespace ImportExportSql.ColumnType
{
    public class ColumnTypeInt : ColumnTypeBase, IDataType
    {
        public string DataTypeName { get; } = "int";
        public IDataType CreateInstance()
        {
            return new ColumnTypeInt();
        }
        public SqlParameter CreateParemeter(string parameterName)
        {
            return new SqlParameter(parameterName, System.Data.SqlDbType.Int);
        }
        internal override object ReadValueObject(SqlDataReader dr, int ix)
        {
            return dr.GetSqlInt32(ix);
        }
        internal override object ReadValueObject(string value)
        {
            return int.Parse(value);
        }
    }
}

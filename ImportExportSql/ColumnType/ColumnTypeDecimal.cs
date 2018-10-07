using System.Data.SqlClient;

namespace ImportExportSql.ColumnType
{
    public class ColumnTypeDecimal : ColumnTypeBase, IDataType
    {
        public string DataTypeName { get; } = "decimal";
        public IDataType CreateInstance()
        {
            return new ColumnTypeDecimal();
        }

        public SqlParameter CreateParemeter(string parameterName)
        {
            return new SqlParameter(parameterName, System.Data.SqlDbType.Decimal);
        }
        internal override object ReadValueObject(SqlDataReader dr, int ix)
        {
            return dr.GetDecimal(ix);
        }
        internal override object ReadValueObject(string value)
        {
            return decimal.Parse(value);
        }
    }
}

using System.Data.SqlClient;

namespace ImportExportSql.ColumnType
{
    public class ColumnTypeMoney : ColumnTypeBase, IDataType
    {
        public string DataTypeName { get; } = "money";
        public IDataType CreateInstance()
        {
            return new ColumnTypeMoney();
        }

        public SqlParameter CreateParemeter(string parameterName)
        {
            return new SqlParameter(parameterName, System.Data.SqlDbType.Money);
        }
        internal override object ReadValueObject(SqlDataReader dr, int ix)
        {
            return dr.GetSqlMoney(ix);
        }
        internal override object ReadValueObject(string value)
        {
            return decimal.Parse(value);
        }
    }
}

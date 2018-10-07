using System.Data.SqlClient;

namespace ImportExportSql.ColumnType
{
    public class ColumnTypeReal : ColumnTypeBase, IDataType
    {
        public string DataTypeName { get; } = "real";
        public IDataType CreateInstance()
        {
            return new ColumnTypeReal();
        }

        public SqlParameter CreateParemeter(string parameterName)
        {
            return new SqlParameter(parameterName, System.Data.SqlDbType.Real);
        }
        internal override object ReadValueObject(SqlDataReader dr, int ix)
        {
            return dr.GetFloat(ix);
        }
        internal override object ReadValueObject(string value)
        {
            return float.Parse(value, System.Globalization.NumberStyles.AllowDecimalPoint);
        }
    }
}

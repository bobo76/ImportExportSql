using System.Data.SqlClient;

namespace ImportExportSql.ColumnType
{
    public class ColumnTypeNVarchar : ColumnTypeBase, IDataType
    {
        public virtual string DataTypeName { get; } = "nvarchar";

        public virtual IDataType CreateInstance()
        {
            return new ColumnTypeNVarchar();
        }
        public SqlParameter CreateParemeter(string parameterName)
        {
            return new SqlParameter(parameterName, System.Data.SqlDbType.NVarChar);
        }

        internal override object ReadValueObject(SqlDataReader dr, int ix)
        {
            return dr.GetSqlString(ix);
        }
        internal override object ReadValueObject(string value)
        {
            return value;
        }
    }
}

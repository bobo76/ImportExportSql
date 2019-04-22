using System;
using System.Data.SqlClient;

namespace ImportExportSql.ColumnType
{
    public class ColumnTypeNChar : ColumnTypeBase, IDataType
    {
        public virtual string DataTypeName { get; } = "nchar";
        public virtual IDataType CreateInstance()
        {
            return new ColumnTypeNChar();
        }
        public SqlParameter CreateParemeter(string parameterName)
        {
            return new SqlParameter(parameterName, System.Data.SqlDbType.NChar);
        }

        internal override object ReadValueObject(SqlDataReader dr, int ix)
        {
            return dr.GetSqlString(ix);
        }
        internal override object ReadValueObject(string value)
        {
            return value;
        }
        public override string ConvertToString(object value)
        {
            if (value == DBNull.Value)
                return string.Empty;
            return value.ToString().TrimEnd();
        }
    }
}

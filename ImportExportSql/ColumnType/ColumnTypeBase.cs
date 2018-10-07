using System;
using System.Data.SqlClient;

namespace ImportExportSql.ColumnType
{
    public class ColumnTypeBase
    {
        public object ReadValue(SqlDataReader dr, int ix)
        {
            if (dr.IsDBNull(ix))
                return DBNull.Value;
            return ReadValueObject(dr, ix);
        }

        public virtual string ConvertToString(object value)
        {
            if (value == DBNull.Value || value == null)
                return "";
            return value.ToString();
        }
        internal virtual object ReadValueObject(SqlDataReader dr, int ix)
        {
            throw new Exception("not implemented");
        }
        public object ReadValue(string value, bool isNullable)
        {
            if (string.IsNullOrEmpty(value) && isNullable)
                return DBNull.Value;
            return ReadValueObject(value);
        }
        internal virtual object ReadValueObject(string value)
        {
            throw new Exception("not implemented");
        }
    }
}

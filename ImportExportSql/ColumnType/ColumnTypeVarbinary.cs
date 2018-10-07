using System;
using System.Data.SqlClient;

namespace ImportExportSql.ColumnType
{
    public class ColumnTypeVarbinary : ColumnTypeBase, IDataType
    {
        public virtual string DataTypeName { get; } = "varbinary";
        public virtual IDataType CreateInstance()
        {
            return new ColumnTypeVarbinary();
        }

        public virtual SqlParameter CreateParemeter(string parameterName)
        {
            return new SqlParameter(parameterName, System.Data.SqlDbType.VarBinary);
        }
        internal override object ReadValueObject(SqlDataReader dr, int ix)
        {
            var len = dr.GetBytes(ix, 0, null, 0, 0);
            byte[] buffer = new byte[len];
            dr.GetBytes(ix, 0, buffer, 0, (int)len);
            return buffer;
        }
        public override string ConvertToString(object value)
        {
            if (value == DBNull.Value)
                return string.Empty;
            return Convert.ToBase64String((byte[])value);
        }
        internal override object ReadValueObject(string value)
        {
            return Convert.FromBase64String(value);
        }
    }
}

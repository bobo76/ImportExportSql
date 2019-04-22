using System;
using System.Data.SqlClient;

namespace ImportExportSql.ColumnType
{
    public class ColumnTypeDateTime : ColumnTypeBase, IDataType
    {
        public string DataTypeName { get; } = "datetime";
        public IDataType CreateInstance()
        {
            return new ColumnTypeDateTime();
        }

        internal override object ReadValueObject(SqlDataReader dr, int ix)
        {
            var dateTmp = dr.GetSqlDateTime(ix);
            return (DateTime)dateTmp;
        }
        public SqlParameter CreateParemeter(string parameterName)
        {
            return new SqlParameter(parameterName, System.Data.SqlDbType.DateTime);
        }
        public override string ConvertToString(object value)
        {
            if (value == DBNull.Value)
                return string.Empty;
            return ((DateTime)value).ToString("yyyy-MM-dd hh:mm:ss.FFFtt");
        }
        internal override object ReadValueObject(string value)
        {
            if (string.IsNullOrEmpty(value))
                return DBNull.Value;

            return DateTime.Parse(value);
        }
    }
}

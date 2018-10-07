using System;
using System.Data.SqlClient;

namespace ImportExportSql.ColumnType
{
    public class ColumnTypeDate : ColumnTypeBase, IDataType
    {
        public string DataTypeName { get; } = "date";
        public IDataType CreateInstance()
        {
            return new ColumnTypeDate();
        }

        internal override object ReadValueObject(SqlDataReader dr, int ix)
        {
            var dateTmp = dr.GetDateTime(ix);
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
            return ((DateTime)value).ToString("yyyy-MM-dd");
        }
        internal override object ReadValueObject(string value)
        {
            return DateTime.Parse(value);
        }
    }
}

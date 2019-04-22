using System;
using System.Data.SqlClient;

namespace ImportExportSql.ColumnType
{
    public class ColumnTypeSmallDateTime : ColumnTypeBase, IDataType
    {
        public string DataTypeName { get; } = "smalldatetime";
        public IDataType CreateInstance()
        {
            return new ColumnTypeSmallDateTime();
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
            return ((DateTime)value).ToString("yyyy-MM-dd hh:mm:ss");
        }
        internal override object ReadValueObject(string value)
        {
            return DateTime.Parse(value);
        }
    }
}

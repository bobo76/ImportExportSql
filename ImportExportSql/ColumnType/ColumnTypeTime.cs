using System;
using System.Data.SqlClient;

namespace ImportExportSql.ColumnType
{
    public class ColumnTypeTime : ColumnTypeBase, IDataType
    {
        public string DataTypeName { get; } = "time";
        public IDataType CreateInstance()
        {
            return new ColumnTypeTime();
        }

        internal override object ReadValueObject(SqlDataReader dr, int ix)
        {
            var obj = dr.GetValue(ix);
            var dateTmp = dr.GetTimeSpan(ix);
            return dateTmp;
        }
        public SqlParameter CreateParemeter(string parameterName)
        {
            return new SqlParameter(parameterName, System.Data.SqlDbType.Time);
        }
        public override string ConvertToString(object value)
        {
            if (value == DBNull.Value)
                return string.Empty;
            var sqlDateTmp = (TimeSpan)value;
            return sqlDateTmp.ToString();
        }
        internal override object ReadValueObject(string value)
        {
            return TimeSpan.Parse(value);
        }
    }
}

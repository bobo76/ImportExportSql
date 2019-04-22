using System.Data.SqlClient;

namespace ImportExportSql.ColumnType
{
    public class ColumnTypeXml : ColumnTypeBase, IDataType
    {
        public string DataTypeName { get; } = "xml";
        public IDataType CreateInstance()
        {
            return new ColumnTypeXml();
        }

        public SqlParameter CreateParemeter(string parameterName)
        {
            return new SqlParameter(parameterName, System.Data.SqlDbType.Xml);
        }
        internal override object ReadValueObject(SqlDataReader dr, int ix)
        {
            return dr.GetSqlXml(ix);
        }
        internal override object ReadValueObject(string value)
        {
            //TODO
            return value;
        }
    }
}

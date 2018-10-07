using System.Data.SqlClient;

namespace ImportExportSql.ColumnType
{
    public class ColumnTypeHierarchyId : ColumnTypeBase, IDataType
    {
        public string DataTypeName { get; } = "hierarchyid";
        public IDataType CreateInstance()
        {
            return new ColumnTypeHierarchyId();
        }

        public SqlParameter CreateParemeter(string parameterName)
        {
            return new SqlParameter(parameterName, System.Data.SqlDbType.Variant);
        }
        internal override object ReadValueObject(SqlDataReader dr, int ix)
        {
            return dr.GetValue(ix);
        }
        internal override object ReadValueObject(string value)
        {
            //todo
            return value;
        }
    }
}

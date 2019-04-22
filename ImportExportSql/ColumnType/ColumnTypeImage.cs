using System.Data.SqlClient;

namespace ImportExportSql.ColumnType
{
    public class ColumnTypeImage : ColumnTypeVarbinary, IDataType
    {
        public override string DataTypeName { get; } = "image";
        public override IDataType CreateInstance()
        {
            return new ColumnTypeImage();
        }

        public override SqlParameter CreateParemeter(string parameterName)
        {
            return new SqlParameter(parameterName, System.Data.SqlDbType.VarBinary);
        }
    }
}

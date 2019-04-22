using ImportExportSql.ColumnType;

namespace ImportExportSql
{

    public class ColumnTypeVarchar : ColumnTypeNVarchar
    {
        public override string DataTypeName { get; } = "varchar";
        public override IDataType CreateInstance()
        {
            return new ColumnTypeVarchar();
        }
    }

    public class ColumnTypeChar : ColumnTypeNChar
    {
        public override string DataTypeName { get; } = "char";
        public override IDataType CreateInstance()
        {
            return new ColumnTypeChar();
        }
    }

}

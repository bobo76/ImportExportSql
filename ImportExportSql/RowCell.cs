namespace ImportExportSql
{
    public class RowCell
    {
        public Column CellColumn { get; set; }
        public object Value { get; set; }
        public override string ToString()
        {
            return CellColumn.Type.ConvertToString(Value);
        }
    }
}

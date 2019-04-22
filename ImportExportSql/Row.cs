using System.Linq;

namespace ImportExportSql
{
    public class Row
    {
        public RowCell[] Cells { get; set; }

        public string SaveAsString(string cellSeparator = ";")
        {
            return string.Join(cellSeparator, Cells.Select(t => t.ToString()).ToArray());
        }
        public string TitleCells(string cellSeparator = ";")
        {
            //:{t.CellColumn.Type.DataTypeName}
            return string.Join(cellSeparator, Cells.Select(t => $"{t.CellColumn.Name}").ToArray());
        }
        public string CellsType(string cellSeparator = ";")
        {
            return string.Join(cellSeparator, Cells.Select(t => $"{t.CellColumn.Name}:{MapTypeFromSql(t.CellColumn.Type.DataTypeName)}").ToArray());
        }
        public static string MapTypeFromSql(string value)
        {
            switch (value)
            {
                case "date":
                case "smalldatetime":
                case "datetime":
                    return "System.DateTime";
                case "datetimeoffset":
                    return "System.DateTimeOffset";
                case "smallint":
                    return "System.Int16";
                case "int":
                    return "System.Int32";
                case "bigint":
                    return "System.Int64";
                case "tinyint":
                    return "System.Byte";
                case "uniqueidentifier":
                    return "System.Guid";
                case "bit":
                    return "System.Boolean";
                case "float":
                    return "System.Double";
                case "real":
                    return "System.Single";
                case "money":
                case "decimal":
                    return "System.Decimal";
                case "char":
                case "nchar":
                case "text":
                case "ntext":
                case "varchar":
                case "nvarchar":
                    return "System.String";
                case "binary":
                case "image":
                case "varbinary":
                    return "System.Byte[]";
            }
            return null;
            }
    }
}

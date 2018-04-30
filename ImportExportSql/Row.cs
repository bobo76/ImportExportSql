using System.Text;

namespace ImportExportSql
{
    public class Row
    {
        public RowCell[] Cells { get; set; }

        public string SaveAsString(char cellSeparator = ';')
        {
            var sb = new StringBuilder();
            for (var i = 0; i < Cells.Length; i++)
            {
                if (i > 0)
                    sb.Append(cellSeparator);
                sb.Append(Cells[i].ToString());
            }
            return sb.ToString();
        }
        public string TitleCells(char cellSeparator = ';')
        {
            var sb = new StringBuilder();
            for (var i = 0; i < Cells.Length; i++)
            {
                if (i > 0)
                    sb.Append(cellSeparator);
                sb.Append(Cells[i].CellColumn.Name);
            }
            return sb.ToString();
        }
    }
}

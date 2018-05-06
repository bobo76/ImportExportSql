using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace ImportExportSql
{
    public static class SqlImport
    {

        public static List<Row> ReadDataFromFile(Table productInfo, string fileName)
        {
            var rowList = new List<Row>();
            var rowCellList = new List<Column>();
            var notFoundCells = new List<string>();

            using (StreamReader SR = new StreamReader(fileName))
            {
                var fieldsList = SR.ReadLine().Split(';');
                for (var i = 0; i < fieldsList.Length; i++)
                {
                    var fieldName = fieldsList[i];
                    var curCell = productInfo.Columns.FirstOrDefault(r => r.Name == fieldName);
                    if (curCell == null)
                        notFoundCells.Add(fieldName);
                    else
                    {
                        curCell.DataIndex = i;
                        rowCellList.Add(curCell);
                    }
                }

                if (notFoundCells.Count > 0)
                {
                    var resp = MessageBox.Show("Can't find this cells : " + notFoundCells.Aggregate((x, y) => x + ", " + y) +
                        Environment.NewLine + "Continue ? ", "Some fields are missing", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                    if (resp != DialogResult.Yes)
                        return null;
                }

                string line;
                object cellValue;
                var cellList = new List<RowCell>();
                while ((line = SR.ReadLine()) != null)
                {
                    fieldsList = line.Split(';');

                    for (var i = 0; i < rowCellList.Count; i++)
                    {
                        try
                        {
                            cellValue = rowCellList[i].Type.ReadValue(fieldsList[rowCellList[i].DataIndex], rowCellList[i].IsNullable);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return null;
                        }
                        var newCell = new RowCell { CellColumn = rowCellList[i], Value = cellValue };
                        cellList.Add(newCell);
                    }
                    var newRow = new Row { Cells = cellList.ToArray() };
                    rowList.Add(newRow);
                    cellList.Clear();
                };
            };
            return rowList;
        }
    }
}

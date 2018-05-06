using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace ImportExportSql
{
    public static class SqlExport
    {

        public static List<Row> ReadFromQuery(Table productInfo, string query, string connectionString)
        {
            var cmd = new SqlCommand(query);
            var rowList = new List<Row>();
            var rowCellList = new List<Column>();

            using (var con = new SqlConnection(connectionString))
            {
                cmd.Connection = con;
                con.Open();
                using (var dr = cmd.ExecuteReader())
                {
                    if (!dr.Read())
                        return null;
                    for (var i = 0; i < dr.FieldCount; i++)
                    {
                        var fieldName = dr.GetName(i);
                        var curCell = productInfo.Columns.FirstOrDefault(r => r.Name == fieldName);
                        rowCellList.Add(curCell);
                    }
                    do
                    {
                        var cellList = new List<RowCell>();
                        for (var i = 0; i < rowCellList.Count; i++)
                        {
                            cellList.Add(new RowCell
                            {
                                CellColumn = rowCellList[i],
                                Value = rowCellList[i].Type.ReadValue(dr, i)
                            });
                        }
                        var newRow = new Row { Cells = cellList.ToArray() };
                        rowList.Add(newRow);
                    } while (dr.Read());
                }
            }
            return rowList;
        }
    }
}

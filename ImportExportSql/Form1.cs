using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace ImportExportSql
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            var dataList = new List<IDataType>
            {
                new ColumnTypeBit(),
                new ColumnTypeDate(),
                new ColumnTypeDateTime(),
                new ColumnTypeDecimal(),
                new ColumnTypeHierarchyId(),
                new ColumnTypeInt(),
                new ColumnTypeMoney(),
                new ColumnTypeNChar(),
                new ColumnTypeNVarchar(),
                new ColumnTypeSmallint(),
                new ColumnTypeTime(),
                new ColumnTypeTinyint(),
                new ColumnTypeUniqueidentifier(),
                new ColumnTypeVarbinary(),
                new ColumnTypeVarchar(),
                new ColumnTypeXml()
            };
            DataTypeFactory.AddDataType(dataList.ToArray());
        }

        private void CmdExport_Click(object sender, EventArgs e)
        {
            var tableInfo = DataHelper.GetTableInfo(TxtTable.Text, TxtConnection.Text);

            var rowList = ReadData(tableInfo, TxtQuery.Text, TxtConnection.Text);
            WriteDataToFile(rowList, TxtPath.Text + TxtTable.Text + DateTime.Now.ToString("_yyyy-MM-dd_hh-mm") + ".txt");
        }

        private List<Row> ReadData(Table productInfo, string query, string connectionString)
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

        private void WriteDataToFile(List<Row> rowList, string fileName)
        {
            if (rowList?.Count == 0)
                return;
            using (StreamWriter FS = new StreamWriter(fileName, true))
            {
                FS.WriteLine(rowList[0].TitleCells());
                rowList.ForEach(i => FS.WriteLine(i.SaveAsString()));
            };
        }

        private void CmdImport_Click(object sender, EventArgs e)
        {
            var tableInfo = DataHelper.GetTableInfo(TxtTable.Text, TxtConnection.Text);
            var rowList = ReadDataFromFile(tableInfo, TxtPath.Text + "Document_2018-05-01_05-10.txt");
            tableInfo.TableName = "Document_test";
            var cmd = DataHelper.BuildCommand(tableInfo, rowList);
            DataHelper.UpdateTable(tableInfo, rowList, cmd, TxtConnection.Text);
        }

        private List<Row> ReadDataFromFile(Table productInfo, string fileName)
        {
            var rowList = new List<Row>();
            var rowCellList = new List<Column>();

            using (StreamReader SR = new StreamReader(fileName))
            {
                var fieldsList = SR.ReadLine().Split(';');
                for (var i = 0; i < fieldsList.Length; i++)
                {
                    var fieldName = fieldsList[i];
                    var curCell = productInfo.Columns.FirstOrDefault(r => r.Name == fieldName);
                    rowCellList.Add(curCell);
                }
                string line;
                var cellList = new List<RowCell>();
                while ((line = SR.ReadLine()) != null)
                {
                    fieldsList = line.Split(';');

                    for (var i = 0; i < rowCellList.Count; i++)
                    {
                        cellList.Add(new RowCell
                        {
                            CellColumn = rowCellList[i],
                            Value = rowCellList[i].Type.ReadValue(fieldsList[i])
                        });
                    }
                    var newRow = new Row{ Cells = cellList.ToArray() };
                    rowList.Add(newRow);
                    cellList.Clear();
                };

            };
            return rowList;
        }

    }
}

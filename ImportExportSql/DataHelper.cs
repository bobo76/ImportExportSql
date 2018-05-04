using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace ImportExportSql
{
    public static class DataHelper
    {
        public static bool VerifyIfTableExist(string tableName, string connectionString)
        {
            //var rst = ExecuteNoQuery($"IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE' AND TABLE_NAME='{tableName}')" +
            //    "SELECT 1 AS res ELSE SELECT 0 AS res", connectionString);
            var rst = ExecuteScalar($"SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE' AND TABLE_NAME='{tableName}'", connectionString);
            if (rst == null)
                return false;
            return (int)rst == 1;
        }
        public static Table GetTableInfo(string tableName, string connectionString)
        {
            var cmd = new SqlCommand("SELECT COLUMN_NAME, DATA_TYPE, IS_NULLABLE, COLUMN_DEFAULT, CHARACTER_MAXIMUM_LENGTH " +
                "FROM INFORMATION_SCHEMA.COLUMNS " +
                $"WHERE TABLE_NAME = '{tableName}'");

            var newTable = new Table { TableName = tableName };
            var columnList = new List<Column>();

            using (var con = new SqlConnection(connectionString))
            {
                cmd.Connection = con;
                con.Open();
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var newColumn = new Column
                        {
                            Name = dr.GetString(0),
                            IsNullable = dr.GetString(2) == "YES"
                        };
                        var dataTypeName = dr.GetString(1);
                        newColumn.Type = DataTypeFactory.GetDataType(dataTypeName);
                        if (newColumn.Type == null)
                        {
                            MessageBox.Show($"Can't find {dataTypeName} type", "Gather data information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return null;
                        }
                        columnList.Add(newColumn);
                    }
                }
            }

            if (columnList.Count == 0)
            {
                MessageBox.Show($"There is no fields found ***", "Gather data information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }

            newTable.Columns = columnList.ToArray();

            return newTable;
        }
        public static SqlCommand BuildCommand(Table tableInfo, List<Row> rowList)
        {
            if (rowList == null || rowList.Count == 0)
                return null;

            string sqlInsert = "", sqlParams = "";
            var fieldsList = rowList[0].Cells.Select(t => t.CellColumn.Name).Aggregate((x, y) => x + "," + y);
            var paramList = rowList[0].Cells.Select(t => "@" + t.CellColumn.Name).Aggregate((x, y) => x + "," + y);
            sqlInsert = $"INSERT INTO {tableInfo.TableName} ({fieldsList}) ";
            sqlParams = $"VALUES ({paramList})";

            var cmd = new SqlCommand(sqlInsert + sqlParams);
            rowList[0].Cells.ToList().ForEach(c => cmd.Parameters.Add(c.CellColumn.Type.CreateParemeter("@" + c.CellColumn.Name)));

            return cmd;
        }
        public static void UpdateTable(Table tableInfo, List<Row> rowList, SqlCommand cmd, string connectionString)
        {
            using (var con = new SqlConnection(connectionString))
            {
                cmd.Connection = con;
                con.Open();
                for (var i = 0; i < rowList.Count; i++)
                {
                    rowList[i].Cells.ToList().ForEach(c => cmd.Parameters["@" + c.CellColumn.Name].Value = c.Value);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private static object ExecuteScalar(string query, string connectionString)
        {
            using (var cmd = new SqlCommand(query))
            using (var con = new SqlConnection(connectionString))
            {
                cmd.Connection = con;
                con.Open();
                return cmd.ExecuteScalar();
            }
        }
    }
}


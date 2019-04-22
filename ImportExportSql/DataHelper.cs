using System;
using System.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using System.Data;

namespace ImportExportSql
{
    public static class DataHelper
    {
        public static bool VerifyIfTableExist(string tableName, string connectionString)
        {
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
                            continue;
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
        public static DataTable GetShema(string query, string connectionString)
        {
            var cmd = new SqlCommand(query);
            using (var con = new SqlConnection(connectionString))
            {
                cmd.Connection = con;
                con.Open();
                using (var dr = cmd.ExecuteReader())
                {
                    return dr.GetSchemaTable();
                }
            }
        }

        public static Table GetTableInfoFromQuery(string query, string connectionString)
        {
            var schema = GetShema(query, connectionString);
            var newTable = new Table { TableName = schema.TableName };
            var columnList = new List<Column>();

            //DataRow row = schema.Rows[0];
            //foreach (DataColumn column in schema.Columns)
            //{
            //    Console.WriteLine(String.Format("{0} = {1}",
            //       column.ColumnName, row[column]));
            //}
            //foreach (DataRow row1 in schema.Rows)
            //{
            //    Console.WriteLine(String.Format("{0} = {1}, {2}", row1["ColumnName"].ToString(), row1["DataTypeName"].ToString(), row1["AllowDBNull"].ToString()));
            //}

            foreach (DataRow row1 in schema.Rows)
            {
                var newColumn = new Column
                {
                    Name = row1["ColumnName"].ToString(),
                    IsNullable = row1["AllowDBNull"].ToString() == "True"
                };
                var dataTypeName = row1["DataTypeName"].ToString();
                newColumn.Type = DataTypeFactory.GetDataType(dataTypeName);
                if (newColumn.Type == null)
                {
                    MessageBox.Show($"Can't find {dataTypeName} type", "Gather data information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    continue;
                }
                columnList.Add(newColumn);
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

        public static object ExecuteNonQuery(string query, string connectionString)
        {
            using (var cmd = new SqlCommand(query))
            using (var con = new SqlConnection(connectionString))
            {
                cmd.Connection = con;
                con.Open();
                return cmd.ExecuteNonQuery();
            }
        }

        public static bool TestConnection(string connectionString)
        {
            var timeout = ConfigurationManager.AppSettings["ConnectionTimeout"];
            timeout = string.IsNullOrEmpty(timeout) ? "5" : timeout;

            if (!connectionString.Contains("Timeout"))
                connectionString += (connectionString.EndsWith(";") ? "" : ";") + $"Connection Timeout = {timeout};";
            using (var con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
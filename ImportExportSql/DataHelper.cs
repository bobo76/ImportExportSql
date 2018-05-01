﻿using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace ImportExportSql
{
    public static class DataHelper
    {
        public static Table GetTableInfo(string tableName, string connectionString)
        {
            var cmd = new SqlCommand("SELECT COLUMN_NAME, DATA_TYPE, IS_NULLABLE, COLUMN_DEFAULT, CHARACTER_MAXIMUM_LENGTH " + 
                "FROM INFORMATION_SCHEMA.COLUMNS " + 
                $"WHERE TABLE_NAME = '{tableName}'");

            var newTable = new Table
            {
                TableName = tableName
            };
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
                            IsNullable = dr.GetString(2) == "YES" ? true : false
                        };
                        var dataTypeName = dr.GetString(1);
                        newColumn.Type = DataTypeFactory.GetDataType(dataTypeName);
                        columnList.Add(newColumn);
                    }
                }
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
    }
}

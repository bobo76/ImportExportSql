using ImportExportSql;
using ImportExportSql.ColumnType;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace TestNUnit
{
    [TestFixture]
    public class DataHelperTest
    {
        private string TestConnectrionString { get; }

        #region "Query"
        private const string TestTypesTableScript = "IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE' AND TABLE_NAME='TestTypes') " +
            "	DROP TABLE [TestTypes]" +
            "CREATE TABLE [dbo].[TestTypes](" +
            "[ID] [int] IDENTITY(1,1) NOT NULL," +
            "[fIntNull] [int] NULL,[fIntNNull] [int] NOT NULL," +
            "[fVarcharNull] [varchar](50) NULL,[fVarcharNNull] [varchar](50) NOT NULL," +
            "[fNVarcharNull] [nvarchar](50) NULL,[fNVarcharNNull] [nvarchar](50) NOT NULL," +
            "[fNCharNull] [nchar](50) NULL,[fNCharNNull] [nchar](50) NOT NULL," +
            "[fBitNull] [bit] NULL,[fBitNNull] [bit] NOT NULL," +
            "[fSmallIntNull] [smallint] NULL,[fSmallIntNNull] [smallint] NOT NULL," +
            "[fTinyIntNull] [tinyint] NULL,[fTinyIntNNull] [tinyint] NOT NULL," +
            "[fRealNull] [real] NULL,[fRealNNull] [real] NOT NULL," +
            "[fMoneyNull] [money] NULL,[fMoneyNNull] [money] NOT NULL," +
            "[fDateTimeNull] [datetime] NULL,[fDateTimeNNull] [datetime] NOT NULL," +
            "[fSmallDateTimeNull] [smalldatetime] NULL,[fSmallDateTimeNNull] [smalldatetime] NOT NULL," +
            "[fDateNull] [date] NULL,[fDateNNull] [date] NOT NULL," +
            "[fTimeNull] [time] NULL,[fTimeNNull] [time] NOT NULL," +
            "[fUniqueidentifierNull] [uniqueidentifier] NULL,[fUniqueidentifierNNull] [uniqueidentifier] NOT NULL," +
            "[fDecimalNull] [decimal](10, 3) NULL,[fDecimalNNull] [decimal](10, 3) NOT NULL," +
            "[fVarbinaryNull] [varbinary](Max) NULL,[fVarbinaryNNull] [varbinary](Max) NOT NULL," +
            "[fImageNull] [image] NULL,[fImageNNull] [image] NOT NULL)"
            ;
        private const string TestSelectScript = "SELECT fIntNull, fIntNNull, fVarcharNull, fVarcharNNull, fNVarcharNull, fNVarcharNNull, fNCharNull, fNCharNNull, fBitNull, fBitNNull, fSmallIntNull, fSmallIntNNull,  " +
            "  fTinyIntNull, fTinyIntNNull, fRealNull,fRealNNull, fMoneyNull, fMoneyNNull, fDateTimeNull, fDateTimeNNull, fSmallDateTimeNull, fSmallDateTimeNNull, fDateNull, fDateNNull, fTimeNull, fTimeNNull,  " +
            "  fUniqueidentifierNull, fUniqueidentifierNNull, fDecimalNull,fDecimalNNull, fVarbinaryNull, fVarbinaryNNull, fImageNull, fImageNNull " +
            "FROM TestTypes";
        #endregion

        public DataHelperTest()
        {
            TestConnectrionString = ConfigurationManager.ConnectionStrings["Test"].ConnectionString;
            var dataList = new List<IDataType>
            {
                new ColumnTypeBit(),
                new ColumnTypeDate(),
                new ColumnTypeSmallDateTime(),
                new ColumnTypeDateTime(),
                new ColumnTypeDecimal(),
                new ColumnTypeInt(),
                new ColumnTypeMoney(),
                new ColumnTypeNChar(),
                new ColumnTypeChar(),
                new ColumnTypeNVarchar(),
                new ColumnTypeReal(),
                new ColumnTypeSmallint(),
                new ColumnTypeTime(),
                new ColumnTypeTinyInt(),
                new ColumnTypeUniqueidentifier(),
                new ColumnTypeVarbinary(),
                new ColumnTypeImage(),
                new ColumnTypeVarchar(),
            };
            DataTypeFactory.AddDataType(dataList.ToArray());
        }

        [Test]
        public void TestTableExist()
        {
			Assert.That(DataHelper.VerifyIfTableExist("EmailAddress", TestConnectrionString), Is.True);
            Assert.That(DataHelper.VerifyIfTableExist("blablalba", TestConnectrionString), Is.False);
        }
		
        [Test]
        public void TestTypeTable()
        {
            CreateTestTypesTable(TestConnectrionString);
            var tableInfo = DataHelper.GetTableInfo("TestTypes", TestConnectrionString);
            Assert.IsNotNull(tableInfo);
            Assert.IsNull(tableInfo.Columns.FirstOrDefault(x => x.Name == "blablalba"));
            var fieldList = new List<Tuple<string, string, Type>>
            {
                new Tuple<string, string, Type>("fIntNull", "fIntNNull", typeof(ColumnTypeInt)),
                new Tuple<string, string, Type>("fVarcharNull", "fVarcharNNull", typeof(ColumnTypeVarchar)),
                new Tuple<string, string, Type>("fNVarcharNull", "fNVarcharNNull", typeof(ColumnTypeNVarchar)),
                new Tuple<string, string, Type>("fNCharNull", "fNCharNNull", typeof(ColumnTypeNChar)),
                new Tuple<string, string, Type>("fBitNull", "fBitNNull", typeof(ColumnTypeBit)),
                new Tuple<string, string, Type>("fSmallIntNull", "fSmallIntNNull", typeof(ColumnTypeSmallint)),
                new Tuple<string, string, Type>("fTinyIntNull", "fTinyIntNNull", typeof(ColumnTypeTinyInt)),
                new Tuple<string, string, Type>("fRealNull", "fRealNNull", typeof(ColumnTypeReal)),
                new Tuple<string, string, Type>("fMoneyNull", "fMoneyNNull", typeof(ColumnTypeMoney)),
                new Tuple<string, string, Type>("fSmallDateTimeNull", "fSmallDateTimeNNull", typeof(ColumnTypeSmallDateTime)),
                new Tuple<string, string, Type>("fDateNull", "fDateNNull", typeof(ColumnTypeDate)),
                new Tuple<string, string, Type>("fDateTimeNull", "fDateTimeNNull", typeof(ColumnTypeDateTime)),
                new Tuple<string, string, Type>("fTimeNull", "fTimeNNull", typeof(ColumnTypeTime)),
                new Tuple<string, string, Type>("fUniqueidentifierNull", "fUniqueidentifierNNull", typeof(ColumnTypeUniqueidentifier)),
                new Tuple<string, string, Type>("fDecimalNull", "fDecimalNNull", typeof(ColumnTypeDecimal)),
                new Tuple<string, string, Type>("fVarbinaryNull", "fVarbinaryNNull", typeof(ColumnTypeVarbinary)),
                new Tuple<string, string, Type>("fImageNull", "fImageNNull", typeof(ColumnTypeImage))
            };

            Column cur;
            foreach (var curField in fieldList)
            {
                cur = tableInfo.Columns.FirstOrDefault(x => x.Name == curField.Item1 && x.IsNullable);
                Assert.IsNotNull(cur);
                Assert.That(cur.Type, Is.TypeOf(curField.Item3));

                cur = tableInfo.Columns.FirstOrDefault(x => x.Name == curField.Item2 && !x.IsNullable);
                Assert.IsNotNull(cur);
                Assert.That(cur.Type, Is.TypeOf(curField.Item3));
            }
            Assert.IsTrue(fieldList.Count == 17);
        }

        [Test]
        public void TestImportExport()
        {
            CreateTestTypesTable(TestConnectrionString);
            var tableInfo = DataHelper.GetTableInfo("TestTypes", TestConnectrionString);
            Assert.IsNotNull(tableInfo);
            var sourceFilePath = AppDomain.CurrentDomain.BaseDirectory + @"..\..\Data\TestTypes_2018-05-06.txt";
            var rowList = SqlImport.ReadDataFromFile(tableInfo, sourceFilePath);
            Assert.IsNotNull(rowList);

            var cmd = DataHelper.BuildCommand(tableInfo, rowList);
            DataHelper.UpdateTable(tableInfo, rowList, cmd, TestConnectrionString);

            var rowListDB = SqlExport.ReadFromQuery(tableInfo, TestSelectScript, TestConnectrionString);
            var fileName = AppDomain.CurrentDomain.BaseDirectory + @"..\..\Data\" + tableInfo.TableName + DateTime.Now.ToString("_yyyy-MM-dd_hh-mm") + ".txt";
            if (File.Exists(fileName))
                File.Delete(fileName);
            WriteDataToFile(rowList, fileName);
            Assert.AreEqual(CheckMD5(sourceFilePath), CheckMD5(fileName));
            File.Delete(fileName);
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

        public string CheckMD5(string filename)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(filename))
                {
                    return Encoding.Default.GetString(md5.ComputeHash(stream));
                }
            }
        }

        private void CreateTestTypesTable(string conStr)
        {
            DataHelper.ExecuteNonQuery(TestTypesTableScript, conStr);
        }
    }
}

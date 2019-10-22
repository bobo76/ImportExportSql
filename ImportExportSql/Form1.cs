using System;
using System.Collections.Generic;
using System.Configuration;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ImportExportSql.ColumnType;

namespace ImportExportSql
{
    public partial class Form1 : Form
    {
        private BackgroundWorker WorkerThread { get; set; }

        public Form1()
        {
            InitializeComponent();

            var dataList = new List<IDataType>
            {
                new ColumnTypeBit(),
                new ColumnTypeDate(),
                new ColumnTypeSmallDateTime(),
                new ColumnTypeDateTime(),
                new ColumnTypeDecimal(),
                new ColumnTypeHierarchyId(),
                new ColumnTypeInt(),
                new ColumnTypeBigInt(),
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
                new ColumnTypeBinary(),
                new ColumnTypeImage(),
                new ColumnTypeVarchar(),
                new ColumnTypeText(),
                new ColumnTypeXml(),
                new ColumnTypeTimestamp()
            };
            DataTypeFactory.AddDataType(dataList.ToArray());

            WorkerThread = new BackgroundWorker();
            WorkerThread.DoWork += WorkerThread_DoWork;
            WorkerThread.ProgressChanged += WorkerThread_ProgressChanged;
            WorkerThread.RunWorkerCompleted += WorkerThread_RunWorkerCompleted;
            WorkerThread.WorkerReportsProgress = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var activeCon = ConfigurationManager.AppSettings["ActiveConnectionString"];
            TxtConnection.Text = ConfigurationManager.ConnectionStrings[activeCon].ConnectionString;
        }

        private void WorkerThread_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            CmdExport.Enabled = true;
            CmdImport.Enabled = true;
        }

        private void WorkerThread_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            pbTransfert.Value = e.ProgressPercentage;
        }

        private void WorkerThread_DoWork(object sender, DoWorkEventArgs e)
        {
            if (e.Argument == null)
                return;
            if (e.Argument.ToString() == "export")
            {
            }
            else if (e.Argument.ToString() == "import")
            {
            }
        }

        private void CmdExport_Click(object sender, EventArgs e)
        {
            var conString = TxtConnection.Text;
            if (!DataHelper.TestConnection(conString))
            {
                MessageBox.Show($"Cannot connect to {conString}", "Export Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Table tableInfo ;
            if (TxtExportTableName.Text.Length > 0)
            {
                var tableName = TxtExportTableName.Text;
                if (!DataHelper.VerifyIfTableExist(tableName, conString))
                {
                    MessageBox.Show($"The table {tableName} doesn't exist in this database", "Validate table", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                tableInfo = DataHelper.GetTableInfo(tableName, conString);
                if (tableInfo == null)
                {
                    MessageBox.Show($"Something went wrong with table : {tableName}", "Export Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            else {
                tableInfo = DataHelper.GetTableInfoFromQuery(TxtQuery.Text, conString);
            }

            pbTransfert.Value = 0;

            WorkerThread.RunWorkerAsync("export");
            var rowList = SqlExport.ReadFromQuery(tableInfo, TxtQuery.Text, conString);
            WriteDataToFile(rowList, TxtExportPath.Text + tableInfo.TableName + DateTime.Now.ToString("_yyyy-MM-dd_hh-mm"));
        }

        private void WriteDataToFile(List<Row> rowList, string fileName)
        {
            if (rowList == null || rowList.Count == 0)
                return;

            using (StreamWriter FS = new StreamWriter(fileName + ".csv", true))
            {
                FS.WriteLine(rowList[0].TitleCells());
                rowList.ForEach(i => FS.WriteLine(i.SaveAsString()));
            };
            using (StreamWriter FS = new StreamWriter(fileName + "_datatype.csv", true))
            {
                FS.WriteLine(rowList[0].CellsType());
            };
        }

        private void CmdImport_Click(object sender, EventArgs e)
        {
            var conString = TxtConnection.Text;
            if (!DataHelper.TestConnection(conString))
            {
                MessageBox.Show($"Cannot connect to {conString}", "Export Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var tableName = txtImportTableName.Text;
            if (!DataHelper.VerifyIfTableExist(tableName, conString))
            {
                MessageBox.Show($"The table {tableName} does'nt exist in this database", "Validate table", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var tableInfo = DataHelper.GetTableInfo(tableName, conString);
            if (tableInfo == null)
            {
                MessageBox.Show($"Something went wrong with table : {tableName}", "Export Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var rowList = SqlImport.ReadDataFromFile(tableInfo, TxtImportPath.Text);
            if (rowList == null)
                return;

            pbTransfert.Value = 0;

            WorkerThread.RunWorkerAsync("import");

            var cmd = DataHelper.BuildCommand(tableInfo, rowList);
            DataHelper.UpdateTable(tableInfo, rowList, cmd, conString);
        }

        private void CmdGetImportPath_Click(object sender, EventArgs e)
        {
            var openFile = new OpenFileDialog
            {
                CheckFileExists = true,
                Multiselect = false
            };
            var rst = openFile.ShowDialog();
            if (rst == DialogResult.Cancel)
                return;
            TxtImportPath.Text = openFile.FileName;
        }

        private void TxtImportPath_TextChanged(object sender, EventArgs e)
        {
            var fileName = Path.GetFileName(TxtImportPath.Text);
            if (string.IsNullOrEmpty(fileName))
                return;

            if (fileName.Contains("_"))
                txtImportTableName.Text = fileName.Substring(0, fileName.IndexOf("_"));
        }
    }
}

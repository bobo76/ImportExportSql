namespace ImportExportSql
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label1 = new System.Windows.Forms.Label();
            this.TxtQuery = new System.Windows.Forms.TextBox();
            this.CmdExport = new System.Windows.Forms.Button();
            this.TxtExportTableName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.CmdImport = new System.Windows.Forms.Button();
            this.TxtImportPath = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.TxtExportPath = new System.Windows.Forms.TextBox();
            this.TxtConnection = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cmdGetImportPath = new System.Windows.Forms.Button();
            this.txtImportTableName = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Query";
            // 
            // TxtQuery
            // 
            this.TxtQuery.Location = new System.Drawing.Point(108, 62);
            this.TxtQuery.Multiline = true;
            this.TxtQuery.Name = "TxtQuery";
            this.TxtQuery.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TxtQuery.Size = new System.Drawing.Size(728, 292);
            this.TxtQuery.TabIndex = 1;
            this.TxtQuery.Text = resources.GetString("TxtQuery.Text");
            // 
            // CmdExport
            // 
            this.CmdExport.Location = new System.Drawing.Point(108, 375);
            this.CmdExport.Name = "CmdExport";
            this.CmdExport.Size = new System.Drawing.Size(148, 35);
            this.CmdExport.TabIndex = 2;
            this.CmdExport.Text = "Export";
            this.CmdExport.UseVisualStyleBackColor = true;
            this.CmdExport.Click += new System.EventHandler(this.CmdExport_Click);
            // 
            // TxtExportTableName
            // 
            this.TxtExportTableName.Location = new System.Drawing.Point(108, 18);
            this.TxtExportTableName.Name = "TxtExportTableName";
            this.TxtExportTableName.Size = new System.Drawing.Size(246, 26);
            this.TxtExportTableName.TabIndex = 6;
            this.TxtExportTableName.Text = "NoteData";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 20);
            this.label3.TabIndex = 5;
            this.label3.Text = "Table";
            // 
            // CmdImport
            // 
            this.CmdImport.Location = new System.Drawing.Point(108, 106);
            this.CmdImport.Name = "CmdImport";
            this.CmdImport.Size = new System.Drawing.Size(148, 35);
            this.CmdImport.TabIndex = 7;
            this.CmdImport.Text = "Import";
            this.CmdImport.UseVisualStyleBackColor = true;
            this.CmdImport.Click += new System.EventHandler(this.CmdImport_Click);
            // 
            // TxtImportPath
            // 
            this.TxtImportPath.Location = new System.Drawing.Point(108, 12);
            this.TxtImportPath.Name = "TxtImportPath";
            this.TxtImportPath.Size = new System.Drawing.Size(680, 26);
            this.TxtImportPath.TabIndex = 9;
            this.TxtImportPath.Text = "c:\\test\\EmailAddress_2018-05-03_07-53.txt";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 20);
            this.label4.TabIndex = 8;
            this.label4.Text = "File";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.TxtExportPath);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.TxtQuery);
            this.panel1.Controls.Add(this.CmdExport);
            this.panel1.Controls.Add(this.TxtExportTableName);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Location = new System.Drawing.Point(18, 58);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(860, 460);
            this.panel1.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 420);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(42, 20);
            this.label5.TabIndex = 10;
            this.label5.Text = "Path";
            // 
            // TxtExportPath
            // 
            this.TxtExportPath.Location = new System.Drawing.Point(108, 417);
            this.TxtExportPath.Name = "TxtExportPath";
            this.TxtExportPath.Size = new System.Drawing.Size(728, 26);
            this.TxtExportPath.TabIndex = 11;
            this.TxtExportPath.Text = "c:\\test\\";
            // 
            // TxtConnection
            // 
            this.TxtConnection.Location = new System.Drawing.Point(126, 17);
            this.TxtConnection.Name = "TxtConnection";
            this.TxtConnection.Size = new System.Drawing.Size(728, 26);
            this.TxtConnection.TabIndex = 12;
            this.TxtConnection.Text = "Server=DESKTOP-2D86E14\\SQLEXPRESS;Database=AdventureWorks2017;Trusted_Connection=" +
    "True;";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 20);
            this.label2.TabIndex = 11;
            this.label2.Text = "Connection";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.cmdGetImportPath);
            this.panel2.Controls.Add(this.txtImportTableName);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.CmdImport);
            this.panel2.Controls.Add(this.TxtImportPath);
            this.panel2.Location = new System.Drawing.Point(18, 552);
            this.panel2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(860, 182);
            this.panel2.TabIndex = 13;
            // 
            // cmdGetImportPath
            // 
            this.cmdGetImportPath.Location = new System.Drawing.Point(796, 8);
            this.cmdGetImportPath.Name = "cmdGetImportPath";
            this.cmdGetImportPath.Size = new System.Drawing.Size(42, 35);
            this.cmdGetImportPath.TabIndex = 12;
            this.cmdGetImportPath.Text = "...";
            this.cmdGetImportPath.UseVisualStyleBackColor = true;
            this.cmdGetImportPath.Click += new System.EventHandler(this.CmdGetImportPath_Click);
            // 
            // txtImportTableName
            // 
            this.txtImportTableName.Location = new System.Drawing.Point(108, 49);
            this.txtImportTableName.Name = "txtImportTableName";
            this.txtImportTableName.Size = new System.Drawing.Size(246, 26);
            this.txtImportTableName.TabIndex = 11;
            this.txtImportTableName.Text = "EmailAddress";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 52);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(48, 20);
            this.label6.TabIndex = 10;
            this.label6.Text = "Table";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(912, 775);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.TxtConnection);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TxtQuery;
        private System.Windows.Forms.Button CmdExport;
        private System.Windows.Forms.TextBox TxtExportTableName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button CmdImport;
        private System.Windows.Forms.TextBox TxtImportPath;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox TxtExportPath;
        private System.Windows.Forms.TextBox TxtConnection;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txtImportTableName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button cmdGetImportPath;
    }
}


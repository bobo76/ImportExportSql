﻿namespace ImportExportSql
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
            this.cmdTest = new System.Windows.Forms.Button();
            this.cmdGetImportPath = new System.Windows.Forms.Button();
            this.txtImportTableName = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.pbTransfert = new System.Windows.Forms.ProgressBar();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 42);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Query";
            // 
            // TxtQuery
            // 
            this.TxtQuery.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtQuery.Location = new System.Drawing.Point(72, 40);
            this.TxtQuery.Margin = new System.Windows.Forms.Padding(2);
            this.TxtQuery.Multiline = true;
            this.TxtQuery.Name = "TxtQuery";
            this.TxtQuery.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TxtQuery.Size = new System.Drawing.Size(581, 252);
            this.TxtQuery.TabIndex = 1;
            this.TxtQuery.Text = "SELECT     [Document].*\r\nFROM         [Document]\r\nWHERE     (DomNo IN (130, 134))" +
    "\r\n";
            // 
            // CmdExport
            // 
            this.CmdExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.CmdExport.Location = new System.Drawing.Point(72, 305);
            this.CmdExport.Margin = new System.Windows.Forms.Padding(2);
            this.CmdExport.Name = "CmdExport";
            this.CmdExport.Size = new System.Drawing.Size(71, 25);
            this.CmdExport.TabIndex = 2;
            this.CmdExport.Text = "Export";
            this.CmdExport.UseVisualStyleBackColor = true;
            this.CmdExport.Click += new System.EventHandler(this.CmdExport_Click);
            // 
            // TxtExportTableName
            // 
            this.TxtExportTableName.Location = new System.Drawing.Point(72, 12);
            this.TxtExportTableName.Margin = new System.Windows.Forms.Padding(2);
            this.TxtExportTableName.Name = "TxtExportTableName";
            this.TxtExportTableName.Size = new System.Drawing.Size(165, 20);
            this.TxtExportTableName.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 14);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Table";
            // 
            // CmdImport
            // 
            this.CmdImport.Location = new System.Drawing.Point(72, 69);
            this.CmdImport.Margin = new System.Windows.Forms.Padding(2);
            this.CmdImport.Name = "CmdImport";
            this.CmdImport.Size = new System.Drawing.Size(71, 25);
            this.CmdImport.TabIndex = 7;
            this.CmdImport.Text = "Import";
            this.CmdImport.UseVisualStyleBackColor = true;
            this.CmdImport.Click += new System.EventHandler(this.CmdImport_Click);
            // 
            // TxtImportPath
            // 
            this.TxtImportPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtImportPath.Location = new System.Drawing.Point(72, 8);
            this.TxtImportPath.Margin = new System.Windows.Forms.Padding(2);
            this.TxtImportPath.Name = "TxtImportPath";
            this.TxtImportPath.Size = new System.Drawing.Size(549, 20);
            this.TxtImportPath.TabIndex = 9;
            this.TxtImportPath.Text = "c:\\test\\EmailAddress_2018-05-03_07-53.txt";
            this.TxtImportPath.TextChanged += new System.EventHandler(this.TxtImportPath_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 10);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(23, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "File";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.TxtExportPath);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.TxtQuery);
            this.panel1.Controls.Add(this.CmdExport);
            this.panel1.Controls.Add(this.TxtExportTableName);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Location = new System.Drawing.Point(12, 81);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(666, 361);
            this.panel1.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 334);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Path";
            // 
            // TxtExportPath
            // 
            this.TxtExportPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtExportPath.Location = new System.Drawing.Point(72, 332);
            this.TxtExportPath.Margin = new System.Windows.Forms.Padding(2);
            this.TxtExportPath.Name = "TxtExportPath";
            this.TxtExportPath.Size = new System.Drawing.Size(581, 20);
            this.TxtExportPath.TabIndex = 11;
            this.TxtExportPath.Text = "c:\\test\\";
            // 
            // TxtConnection
            // 
            this.TxtConnection.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtConnection.Location = new System.Drawing.Point(84, 11);
            this.TxtConnection.Margin = new System.Windows.Forms.Padding(2);
            this.TxtConnection.Name = "TxtConnection";
            this.TxtConnection.Size = new System.Drawing.Size(594, 20);
            this.TxtConnection.TabIndex = 12;
            this.TxtConnection.Text = "Server=DESKTOP-2D86E14\\SQLEXPRESS;Database=AdventureWorks2017;Trusted_Connection=" +
    "True;";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 13);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Connection";
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.cmdTest);
            this.panel2.Controls.Add(this.cmdGetImportPath);
            this.panel2.Controls.Add(this.txtImportTableName);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.CmdImport);
            this.panel2.Controls.Add(this.TxtImportPath);
            this.panel2.Location = new System.Drawing.Point(12, 448);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(666, 119);
            this.panel2.TabIndex = 13;
            // 
            // cmdTest
            // 
            this.cmdTest.Location = new System.Drawing.Point(191, 69);
            this.cmdTest.Margin = new System.Windows.Forms.Padding(2);
            this.cmdTest.Name = "cmdTest";
            this.cmdTest.Size = new System.Drawing.Size(71, 25);
            this.cmdTest.TabIndex = 13;
            this.cmdTest.Text = "clean df";
            this.cmdTest.UseVisualStyleBackColor = true;
            //this.cmdTest.Click += new System.EventHandler(this.cmdTest_Click);
            // 
            // cmdGetImportPath
            // 
            this.cmdGetImportPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdGetImportPath.Location = new System.Drawing.Point(625, 5);
            this.cmdGetImportPath.Margin = new System.Windows.Forms.Padding(2);
            this.cmdGetImportPath.Name = "cmdGetImportPath";
            this.cmdGetImportPath.Size = new System.Drawing.Size(28, 23);
            this.cmdGetImportPath.TabIndex = 12;
            this.cmdGetImportPath.Text = "...";
            this.cmdGetImportPath.UseVisualStyleBackColor = true;
            this.cmdGetImportPath.Click += new System.EventHandler(this.CmdGetImportPath_Click);
            // 
            // txtImportTableName
            // 
            this.txtImportTableName.Location = new System.Drawing.Point(72, 32);
            this.txtImportTableName.Margin = new System.Windows.Forms.Padding(2);
            this.txtImportTableName.Name = "txtImportTableName";
            this.txtImportTableName.Size = new System.Drawing.Size(165, 20);
            this.txtImportTableName.TabIndex = 11;
            this.txtImportTableName.Text = "EmailAddress";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 34);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(34, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Table";
            // 
            // pbTransfert
            // 
            this.pbTransfert.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbTransfert.Location = new System.Drawing.Point(12, 36);
            this.pbTransfert.Name = "pbTransfert";
            this.pbTransfert.Size = new System.Drawing.Size(666, 23);
            this.pbTransfert.TabIndex = 14;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(690, 579);
            this.Controls.Add(this.pbTransfert);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.TxtConnection);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "Import export to sql";
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
        private System.Windows.Forms.ProgressBar pbTransfert;
        private System.Windows.Forms.Button cmdTest;
    }
}


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
            this.label2 = new System.Windows.Forms.Label();
            this.TxtConnection = new System.Windows.Forms.TextBox();
            this.TxtTable = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.CmdImport = new System.Windows.Forms.Button();
            this.TxtPath = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 120);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Query";
            // 
            // TxtQuery
            // 
            this.TxtQuery.Location = new System.Drawing.Point(126, 117);
            this.TxtQuery.Multiline = true;
            this.TxtQuery.Name = "TxtQuery";
            this.TxtQuery.Size = new System.Drawing.Size(729, 406);
            this.TxtQuery.TabIndex = 1;
            this.TxtQuery.Text = resources.GetString("TxtQuery.Text");
            // 
            // CmdExport
            // 
            this.CmdExport.Location = new System.Drawing.Point(126, 544);
            this.CmdExport.Name = "CmdExport";
            this.CmdExport.Size = new System.Drawing.Size(148, 35);
            this.CmdExport.TabIndex = 2;
            this.CmdExport.Text = "Export";
            this.CmdExport.UseVisualStyleBackColor = true;
            this.CmdExport.Click += new System.EventHandler(this.CmdExport_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Connection";
            // 
            // TxtConnection
            // 
            this.TxtConnection.Location = new System.Drawing.Point(126, 37);
            this.TxtConnection.Name = "TxtConnection";
            this.TxtConnection.Size = new System.Drawing.Size(729, 26);
            this.TxtConnection.TabIndex = 4;
            this.TxtConnection.Text = "Server=DESKTOP-2D86E14\\SQLEXPRESS;Database=AdventureWorks2017;Trusted_Connection=" +
    "True;";
            // 
            // TxtTable
            // 
            this.TxtTable.Location = new System.Drawing.Point(126, 74);
            this.TxtTable.Name = "TxtTable";
            this.TxtTable.Size = new System.Drawing.Size(245, 26);
            this.TxtTable.TabIndex = 6;
            this.TxtTable.Text = "Customer";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(27, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 20);
            this.label3.TabIndex = 5;
            this.label3.Text = "Table";
            // 
            // CmdImport
            // 
            this.CmdImport.Location = new System.Drawing.Point(126, 665);
            this.CmdImport.Name = "CmdImport";
            this.CmdImport.Size = new System.Drawing.Size(148, 35);
            this.CmdImport.TabIndex = 7;
            this.CmdImport.Text = "Import";
            this.CmdImport.UseVisualStyleBackColor = true;
            this.CmdImport.Click += new System.EventHandler(this.CmdImport_Click);
            // 
            // TxtPath
            // 
            this.TxtPath.Location = new System.Drawing.Point(126, 606);
            this.TxtPath.Name = "TxtPath";
            this.TxtPath.Size = new System.Drawing.Size(729, 26);
            this.TxtPath.TabIndex = 9;
            this.TxtPath.Text = "D:\\Tests\\";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(27, 609);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 20);
            this.label4.TabIndex = 8;
            this.label4.Text = "Path";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(912, 776);
            this.Controls.Add(this.TxtPath);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.CmdImport);
            this.Controls.Add(this.TxtTable);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.TxtConnection);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.CmdExport);
            this.Controls.Add(this.TxtQuery);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TxtQuery;
        private System.Windows.Forms.Button CmdExport;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TxtConnection;
        private System.Windows.Forms.TextBox TxtTable;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button CmdImport;
        private System.Windows.Forms.TextBox TxtPath;
        private System.Windows.Forms.Label label4;
    }
}


namespace ACM.BL
{
    partial class RepositoryViewer
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
            this.SqlBtn = new System.Windows.Forms.Button();
            this.RepositoryTypeTextBlock = new System.Windows.Forms.ListBox();
            this.ClearDataBtn = new System.Windows.Forms.Button();
            this.CsvBtn = new System.Windows.Forms.Button();
            this.ServiceBtn = new System.Windows.Forms.Button();
            this.ClickMeBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // SqlBtn
            // 
            this.SqlBtn.Location = new System.Drawing.Point(14, 63);
            this.SqlBtn.Name = "SqlBtn";
            this.SqlBtn.Size = new System.Drawing.Size(109, 23);
            this.SqlBtn.TabIndex = 13;
            this.SqlBtn.Text = "Sql Repository";
            this.SqlBtn.UseVisualStyleBackColor = true;
            // 
            // RepositoryTypeTextBlock
            // 
            this.RepositoryTypeTextBlock.FormattingEnabled = true;
            this.RepositoryTypeTextBlock.Location = new System.Drawing.Point(178, 5);
            this.RepositoryTypeTextBlock.Name = "RepositoryTypeTextBlock";
            this.RepositoryTypeTextBlock.Size = new System.Drawing.Size(280, 433);
            this.RepositoryTypeTextBlock.TabIndex = 12;
            // 
            // ClearDataBtn
            // 
            this.ClearDataBtn.Location = new System.Drawing.Point(14, 408);
            this.ClearDataBtn.Name = "ClearDataBtn";
            this.ClearDataBtn.Size = new System.Drawing.Size(109, 23);
            this.ClearDataBtn.TabIndex = 11;
            this.ClearDataBtn.Text = "Clear Data";
            this.ClearDataBtn.UseVisualStyleBackColor = true;
            this.ClearDataBtn.Click += new System.EventHandler(this.ClearDataBtn_Click);
            // 
            // CsvBtn
            // 
            this.CsvBtn.Location = new System.Drawing.Point(14, 34);
            this.CsvBtn.Name = "CsvBtn";
            this.CsvBtn.Size = new System.Drawing.Size(109, 23);
            this.CsvBtn.TabIndex = 10;
            this.CsvBtn.Text = "CSV Repository";
            this.CsvBtn.UseVisualStyleBackColor = true;
            // 
            // ServiceBtn
            // 
            this.ServiceBtn.Location = new System.Drawing.Point(14, 5);
            this.ServiceBtn.Name = "ServiceBtn";
            this.ServiceBtn.Size = new System.Drawing.Size(109, 23);
            this.ServiceBtn.TabIndex = 9;
            this.ServiceBtn.Text = "Service Repository";
            this.ServiceBtn.UseVisualStyleBackColor = true;
            this.ServiceBtn.Click += new System.EventHandler(this.ServiceBtn_Click);
            // 
            // ClickMeBtn
            // 
            this.ClickMeBtn.Location = new System.Drawing.Point(14, 92);
            this.ClickMeBtn.Name = "ClickMeBtn";
            this.ClickMeBtn.Size = new System.Drawing.Size(109, 23);
            this.ClickMeBtn.TabIndex = 14;
            this.ClickMeBtn.Text = "Click Me";
            this.ClickMeBtn.UseVisualStyleBackColor = true;
            this.ClickMeBtn.Click += new System.EventHandler(this.ClickMeBtn_Click);
            // 
            // RepositoryViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(465, 450);
            this.Controls.Add(this.ClickMeBtn);
            this.Controls.Add(this.SqlBtn);
            this.Controls.Add(this.RepositoryTypeTextBlock);
            this.Controls.Add(this.ClearDataBtn);
            this.Controls.Add(this.CsvBtn);
            this.Controls.Add(this.ServiceBtn);
            this.Name = "RepositoryViewer";
            this.Text = "RepositoryViewer";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button SqlBtn;
        private System.Windows.Forms.ListBox RepositoryTypeTextBlock;
        private System.Windows.Forms.Button ClearDataBtn;
        private System.Windows.Forms.Button CsvBtn;
        private System.Windows.Forms.Button ServiceBtn;
        private System.Windows.Forms.Button ClickMeBtn;
    }
}
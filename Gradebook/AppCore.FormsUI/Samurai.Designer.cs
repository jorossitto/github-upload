namespace AppCore.FormsUI
{
    partial class Samurai
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.samuraiListBox = new System.Windows.Forms.ListBox();
            this.Name = new System.Windows.Forms.Label();
            this.RealName = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.QuotesListBox = new System.Windows.Forms.ListBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SamuraiName = new System.Windows.Forms.TextBox();
            this.SamuraiRealName = new System.Windows.Forms.TextBox();
            this.NewSamurai = new System.Windows.Forms.Button();
            this.Save = new System.Windows.Forms.Button();
            // 
            // samuraiListBox
            // 
            this.samuraiListBox.FormattingEnabled = true;
            this.samuraiListBox.ItemHeight = 15;
            this.samuraiListBox.Location = new System.Drawing.Point(11, 10);
            this.samuraiListBox.Name = "samuraiListBox";
            this.samuraiListBox.Size = new System.Drawing.Size(120, 94);
            this.samuraiListBox.TabIndex = 0;
            this.samuraiListBox.SelectedIndexChanged += new System.EventHandler(this.samuraiListBox_SelectedIndexChanged);
            // 
            // Name
            // 
            this.Name.AutoSize = true;
            this.Name.Location = new System.Drawing.Point(15, 111);
            this.Name.Name = "Name";
            this.Name.Size = new System.Drawing.Size(38, 15);
            this.Name.TabIndex = 1;
            this.Name.Text = "label1";
            // 
            // RealName
            // 
            this.RealName.AutoSize = true;
            this.RealName.Location = new System.Drawing.Point(15, 140);
            this.RealName.Name = "RealName";
            this.RealName.Size = new System.Drawing.Size(38, 15);
            this.RealName.TabIndex = 1;
            this.RealName.Text = "label2";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 15;
            this.listBox1.Location = new System.Drawing.Point(21, 159);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(120, 94);
            this.listBox1.TabIndex = 2;
            // 
            // QuotesListBox
            // 
            this.QuotesListBox.FormattingEnabled = true;
            this.QuotesListBox.ItemHeight = 15;
            this.QuotesListBox.Location = new System.Drawing.Point(15, 166);
            this.QuotesListBox.Name = "QuotesListBox";
            this.QuotesListBox.Size = new System.Drawing.Size(120, 94);
            this.QuotesListBox.TabIndex = 3;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(61, 111);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 23);
            this.textBox1.TabIndex = 4;
            // 
            // SamuraiName
            // 
            this.SamuraiName.Location = new System.Drawing.Point(59, 111);
            this.SamuraiName.Name = "SamuraiName";
            this.SamuraiName.Size = new System.Drawing.Size(100, 23);
            this.SamuraiName.TabIndex = 5;
            // 
            // SamuraiRealName
            // 
            this.SamuraiRealName.Location = new System.Drawing.Point(59, 137);
            this.SamuraiRealName.Name = "SamuraiRealName";
            this.SamuraiRealName.Size = new System.Drawing.Size(100, 23);
            this.SamuraiRealName.TabIndex = 6;
            this.SamuraiRealName.TextChanged += new System.EventHandler(this.SamuraiRealName_TextChanged);
            // 
            // NewSamurai
            // 
            this.NewSamurai.Location = new System.Drawing.Point(149, 10);
            this.NewSamurai.Name = "NewSamurai";
            this.NewSamurai.Size = new System.Drawing.Size(75, 23);
            this.NewSamurai.TabIndex = 7;
            this.NewSamurai.Text = "button1";
            this.NewSamurai.UseVisualStyleBackColor = true;
            this.NewSamurai.Click += new System.EventHandler(this.NewSamurai_Click);
            // 
            // Save
            // 
            this.Save.Location = new System.Drawing.Point(170, 226);
            this.Save.Name = "Save";
            this.Save.Size = new System.Drawing.Size(75, 23);
            this.Save.TabIndex = 8;
            this.Save.Text = "button1";
            this.Save.UseVisualStyleBackColor = true;
            // 
            // Samurai
            // 
            this.Controls.Add(this.Save);
            this.Controls.Add(this.NewSamurai);
            this.Controls.Add(this.SamuraiRealName);
            this.Controls.Add(this.SamuraiName);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.QuotesListBox);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.Name);
            this.Controls.Add(this.samuraiListBox);
            this.Controls.Add(this.RealName);
            this.Name.Text = "Samurai";

        }

        #endregion

        private System.Windows.Forms.ListBox samuraiListBox;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label Name;
        private System.Windows.Forms.Label RealName;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.ListBox QuotesListBox;
        private System.Windows.Forms.TextBox SamuraiName;
        private System.Windows.Forms.TextBox SamuraiRealName;
        private System.Windows.Forms.Button NewSamurai;
        private System.Windows.Forms.Button Save;
    }
}


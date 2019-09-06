namespace ACM.BL
{
    partial class MainWindow
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
            this.PeopleMenuBtn = new System.Windows.Forms.Button();
            this.OrderBtn = new System.Windows.Forms.Button();
            this.PedometerBtn = new System.Windows.Forms.Button();
            this.CountryBox = new System.Windows.Forms.ComboBox();
            this.SubmitBtn = new System.Windows.Forms.Button();
            this.CountryLbl = new System.Windows.Forms.Label();
            this.jobMenuBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // PeopleMenuBtn
            // 
            this.PeopleMenuBtn.Location = new System.Drawing.Point(16, 12);
            this.PeopleMenuBtn.Name = "PeopleMenuBtn";
            this.PeopleMenuBtn.Size = new System.Drawing.Size(107, 23);
            this.PeopleMenuBtn.TabIndex = 0;
            this.PeopleMenuBtn.Text = "People Menu";
            this.PeopleMenuBtn.UseVisualStyleBackColor = true;
            this.PeopleMenuBtn.Click += new System.EventHandler(this.PeopleMenuBtn_Click);
            // 
            // OrderBtn
            // 
            this.OrderBtn.Location = new System.Drawing.Point(129, 12);
            this.OrderBtn.Name = "OrderBtn";
            this.OrderBtn.Size = new System.Drawing.Size(107, 23);
            this.OrderBtn.TabIndex = 3;
            this.OrderBtn.Text = "Order Menu";
            this.OrderBtn.UseVisualStyleBackColor = true;
            // 
            // PedometerBtn
            // 
            this.PedometerBtn.Location = new System.Drawing.Point(242, 12);
            this.PedometerBtn.Name = "PedometerBtn";
            this.PedometerBtn.Size = new System.Drawing.Size(107, 23);
            this.PedometerBtn.TabIndex = 4;
            this.PedometerBtn.Text = "Pedometer Menu";
            this.PedometerBtn.UseVisualStyleBackColor = true;
            // 
            // CountryBox
            // 
            this.CountryBox.FormattingEnabled = true;
            this.CountryBox.Location = new System.Drawing.Point(115, 86);
            this.CountryBox.Name = "CountryBox";
            this.CountryBox.Size = new System.Drawing.Size(121, 21);
            this.CountryBox.TabIndex = 5;
            this.CountryBox.SelectedIndexChanged += new System.EventHandler(this.CountryBox_SelectedIndexChanged);
            // 
            // SubmitBtn
            // 
            this.SubmitBtn.Location = new System.Drawing.Point(129, 113);
            this.SubmitBtn.Name = "SubmitBtn";
            this.SubmitBtn.Size = new System.Drawing.Size(75, 23);
            this.SubmitBtn.TabIndex = 6;
            this.SubmitBtn.Text = "Submit";
            this.SubmitBtn.UseVisualStyleBackColor = true;
            this.SubmitBtn.Click += new System.EventHandler(this.SubmitBtn_Click);
            // 
            // CountryLbl
            // 
            this.CountryLbl.AutoSize = true;
            this.CountryLbl.Location = new System.Drawing.Point(41, 94);
            this.CountryLbl.Name = "CountryLbl";
            this.CountryLbl.Size = new System.Drawing.Size(54, 13);
            this.CountryLbl.TabIndex = 7;
            this.CountryLbl.Text = "Courntries";
            // 
            // jobMenuBtn
            // 
            this.jobMenuBtn.Location = new System.Drawing.Point(16, 41);
            this.jobMenuBtn.Name = "jobMenuBtn";
            this.jobMenuBtn.Size = new System.Drawing.Size(107, 23);
            this.jobMenuBtn.TabIndex = 8;
            this.jobMenuBtn.Text = "Job Menu";
            this.jobMenuBtn.UseVisualStyleBackColor = true;
            this.jobMenuBtn.Click += new System.EventHandler(this.jobMenuBtn_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(373, 144);
            this.Controls.Add(this.jobMenuBtn);
            this.Controls.Add(this.CountryLbl);
            this.Controls.Add(this.SubmitBtn);
            this.Controls.Add(this.CountryBox);
            this.Controls.Add(this.PedometerBtn);
            this.Controls.Add(this.OrderBtn);
            this.Controls.Add(this.PeopleMenuBtn);
            this.Name = "MainWindow";
            this.Text = "MainWindow";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button PeopleMenuBtn;
        private System.Windows.Forms.Button OrderBtn;
        private System.Windows.Forms.Button PedometerBtn;
        private System.Windows.Forms.ComboBox CountryBox;
        private System.Windows.Forms.Button SubmitBtn;
        private System.Windows.Forms.Label CountryLbl;
        private System.Windows.Forms.Button jobMenuBtn;
    }
}
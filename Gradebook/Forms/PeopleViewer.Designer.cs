namespace ACM.BL
{
    partial class PeopleViewer
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
            this.PersonListBox = new System.Windows.Forms.ListBox();
            this.ClearDataBtn = new System.Windows.Forms.Button();
            this.AbstractionBtn = new System.Windows.Forms.Button();
            this.ConcreteTypeBtn = new System.Windows.Forms.Button();
            this.RepositoryMenuBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // PersonListBox
            // 
            this.PersonListBox.FormattingEnabled = true;
            this.PersonListBox.Location = new System.Drawing.Point(179, 12);
            this.PersonListBox.Name = "PersonListBox";
            this.PersonListBox.Size = new System.Drawing.Size(280, 433);
            this.PersonListBox.TabIndex = 7;
            // 
            // ClearDataBtn
            // 
            this.ClearDataBtn.Location = new System.Drawing.Point(15, 415);
            this.ClearDataBtn.Name = "ClearDataBtn";
            this.ClearDataBtn.Size = new System.Drawing.Size(109, 23);
            this.ClearDataBtn.TabIndex = 6;
            this.ClearDataBtn.Text = "Clear Data";
            this.ClearDataBtn.UseVisualStyleBackColor = true;
            this.ClearDataBtn.Click += new System.EventHandler(this.ClearDataBtn_Click);
            // 
            // AbstractionBtn
            // 
            this.AbstractionBtn.Location = new System.Drawing.Point(15, 41);
            this.AbstractionBtn.Name = "AbstractionBtn";
            this.AbstractionBtn.Size = new System.Drawing.Size(109, 23);
            this.AbstractionBtn.TabIndex = 5;
            this.AbstractionBtn.Text = "Abstraction";
            this.AbstractionBtn.UseVisualStyleBackColor = true;
            this.AbstractionBtn.Click += new System.EventHandler(this.AbstractionBtn_Click);
            // 
            // ConcreteTypeBtn
            // 
            this.ConcreteTypeBtn.Location = new System.Drawing.Point(15, 12);
            this.ConcreteTypeBtn.Name = "ConcreteTypeBtn";
            this.ConcreteTypeBtn.Size = new System.Drawing.Size(109, 23);
            this.ConcreteTypeBtn.TabIndex = 4;
            this.ConcreteTypeBtn.Text = "Concrete Type";
            this.ConcreteTypeBtn.UseVisualStyleBackColor = true;
            this.ConcreteTypeBtn.Click += new System.EventHandler(this.ConcreteTypeBtn_Click);
            // 
            // RepositoryMenuBtn
            // 
            this.RepositoryMenuBtn.Location = new System.Drawing.Point(15, 70);
            this.RepositoryMenuBtn.Name = "RepositoryMenuBtn";
            this.RepositoryMenuBtn.Size = new System.Drawing.Size(109, 23);
            this.RepositoryMenuBtn.TabIndex = 8;
            this.RepositoryMenuBtn.Text = "Repository Menu";
            this.RepositoryMenuBtn.UseVisualStyleBackColor = true;
            this.RepositoryMenuBtn.Click += new System.EventHandler(this.RepositoryMenuBtn_Click);
            // 
            // PeopleViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(473, 450);
            this.Controls.Add(this.RepositoryMenuBtn);
            this.Controls.Add(this.PersonListBox);
            this.Controls.Add(this.ClearDataBtn);
            this.Controls.Add(this.AbstractionBtn);
            this.Controls.Add(this.ConcreteTypeBtn);
            this.Name = "PeopleViewer";
            this.Text = "PeopleViewer";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox PersonListBox;
        private System.Windows.Forms.Button ClearDataBtn;
        private System.Windows.Forms.Button AbstractionBtn;
        private System.Windows.Forms.Button ConcreteTypeBtn;
        private System.Windows.Forms.Button RepositoryMenuBtn;
    }
}
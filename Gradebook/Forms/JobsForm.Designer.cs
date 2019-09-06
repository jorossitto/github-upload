namespace Forms
{
    partial class JobsForm
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
            this.jobCombo = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // jobCombo
            // 
            this.jobCombo.FormattingEnabled = true;
            this.jobCombo.Location = new System.Drawing.Point(158, 164);
            this.jobCombo.Name = "jobCombo";
            this.jobCombo.Size = new System.Drawing.Size(121, 21);
            this.jobCombo.TabIndex = 0;
            this.jobCombo.SelectedIndexChanged += new System.EventHandler(this.jobCombo_SelectedIndexChanged);
            // 
            // JobsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.jobCombo);
            this.Name = "JobsForm";
            this.Text = "JobsForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox jobCombo;
    }
}
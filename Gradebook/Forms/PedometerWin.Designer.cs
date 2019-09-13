namespace ACM.BL
{
    partial class PedometerWin
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
            this.StepGoalTxt = new System.Windows.Forms.TextBox();
            this.GoalLBL = new System.Windows.Forms.Label();
            this.TotalStepLBL = new System.Windows.Forms.Label();
            this.TotalStepsTxt = new System.Windows.Forms.TextBox();
            this.CalculateBTN = new System.Windows.Forms.Button();
            this.ResultLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // StepGoalTxt
            // 
            this.StepGoalTxt.Location = new System.Drawing.Point(136, 16);
            this.StepGoalTxt.Name = "StepGoalTxt";
            this.StepGoalTxt.Size = new System.Drawing.Size(100, 20);
            this.StepGoalTxt.TabIndex = 0;
            // 
            // GoalLBL
            // 
            this.GoalLBL.AutoSize = true;
            this.GoalLBL.Location = new System.Drawing.Point(7, 16);
            this.GoalLBL.Name = "GoalLBL";
            this.GoalLBL.Size = new System.Drawing.Size(105, 13);
            this.GoalLBL.TabIndex = 1;
            this.GoalLBL.Text = "Step Goal for Today:";
            // 
            // TotalStepLBL
            // 
            this.TotalStepLBL.AutoSize = true;
            this.TotalStepLBL.Location = new System.Drawing.Point(7, 42);
            this.TotalStepLBL.Name = "TotalStepLBL";
            this.TotalStepLBL.Size = new System.Drawing.Size(122, 13);
            this.TotalStepLBL.TabIndex = 3;
            this.TotalStepLBL.Text = "Number of Steps Today:";
            // 
            // TotalStepsTxt
            // 
            this.TotalStepsTxt.Location = new System.Drawing.Point(136, 42);
            this.TotalStepsTxt.Name = "TotalStepsTxt";
            this.TotalStepsTxt.Size = new System.Drawing.Size(100, 20);
            this.TotalStepsTxt.TabIndex = 2;
            // 
            // CalculateBTN
            // 
            this.CalculateBTN.Location = new System.Drawing.Point(252, 12);
            this.CalculateBTN.Name = "CalculateBTN";
            this.CalculateBTN.Size = new System.Drawing.Size(97, 50);
            this.CalculateBTN.TabIndex = 4;
            this.CalculateBTN.Text = "Calculate";
            this.CalculateBTN.UseVisualStyleBackColor = true;
            this.CalculateBTN.Click += new System.EventHandler(this.CalculateBTN_Click);
            // 
            // ResultLabel
            // 
            this.ResultLabel.AutoSize = true;
            this.ResultLabel.Location = new System.Drawing.Point(10, 86);
            this.ResultLabel.Name = "ResultLabel";
            this.ResultLabel.Size = new System.Drawing.Size(10, 13);
            this.ResultLabel.TabIndex = 5;
            this.ResultLabel.Text = " ";
            
            // 
            // PedometerWin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(359, 138);
            this.Controls.Add(this.ResultLabel);
            this.Controls.Add(this.CalculateBTN);
            this.Controls.Add(this.TotalStepLBL);
            this.Controls.Add(this.TotalStepsTxt);
            this.Controls.Add(this.GoalLBL);
            this.Controls.Add(this.StepGoalTxt);
            this.Name = "PedometerWin";
            this.Text = "PedometerWin";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox StepGoalTxt;
        private System.Windows.Forms.Label GoalLBL;
        private System.Windows.Forms.Label TotalStepLBL;
        private System.Windows.Forms.TextBox TotalStepsTxt;
        private System.Windows.Forms.Button CalculateBTN;
        private System.Windows.Forms.Label ResultLabel;
    }
}
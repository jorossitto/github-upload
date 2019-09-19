namespace ACM.BL
{
    partial class OrderWin
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
            this.button1 = new System.Windows.Forms.Button();
            this.PlaceOrders = new System.Windows.Forms.Button();
            this.StockBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(285, 11);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(82, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Place Order";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // PlaceOrders
            // 
            this.PlaceOrders.Location = new System.Drawing.Point(285, 40);
            this.PlaceOrders.Name = "PlaceOrders";
            this.PlaceOrders.Size = new System.Drawing.Size(82, 23);
            this.PlaceOrders.TabIndex = 1;
            this.PlaceOrders.Text = "Place Orders";
            this.PlaceOrders.UseVisualStyleBackColor = true;
            this.PlaceOrders.Click += new System.EventHandler(this.PlaceOrders_Click);
            // 
            // StockBtn
            // 
            this.StockBtn.Location = new System.Drawing.Point(285, 69);
            this.StockBtn.Name = "StockBtn";
            this.StockBtn.Size = new System.Drawing.Size(82, 23);
            this.StockBtn.TabIndex = 2;
            this.StockBtn.Text = "Stock";
            this.StockBtn.UseVisualStyleBackColor = true;
            this.StockBtn.Click += new System.EventHandler(this.StockBtn_Click);
            // 
            // OrderWin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(379, 200);
            this.Controls.Add(this.StockBtn);
            this.Controls.Add(this.PlaceOrders);
            this.Controls.Add(this.button1);
            this.Name = "OrderWin";
            this.Text = "OrderWin";
            this.Load += new System.EventHandler(this.OrderWin_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button PlaceOrders;
        private System.Windows.Forms.Button StockBtn;
    }
}
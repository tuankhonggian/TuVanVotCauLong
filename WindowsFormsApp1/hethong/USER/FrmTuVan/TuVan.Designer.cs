namespace WindowsFormsApp1.hethong.FrmTuVan
{
    partial class TuVan
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
            this.guna2Button_BatDau = new Guna.UI2.WinForms.Guna2Button();
            this.SuspendLayout();
            // 
            // guna2Button_BatDau
            // 
            this.guna2Button_BatDau.BorderColor = System.Drawing.Color.White;
            this.guna2Button_BatDau.BorderRadius = 18;
            this.guna2Button_BatDau.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button_BatDau.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button_BatDau.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.guna2Button_BatDau.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.guna2Button_BatDau.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.guna2Button_BatDau.ForeColor = System.Drawing.Color.White;
            this.guna2Button_BatDau.Location = new System.Drawing.Point(507, 290);
            this.guna2Button_BatDau.Name = "guna2Button_BatDau";
            this.guna2Button_BatDau.PressedColor = System.Drawing.Color.Lime;
            this.guna2Button_BatDau.Size = new System.Drawing.Size(180, 45);
            this.guna2Button_BatDau.TabIndex = 0;
            this.guna2Button_BatDau.Text = "Bắt Đầu Chọn Vợt";
            this.guna2Button_BatDau.Click += new System.EventHandler(this.guna2Button_BatDau_Click);
            // 
            // TuVan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1200, 660);
            this.Controls.Add(this.guna2Button_BatDau);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "TuVan";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FrmTuVan";
            this.Load += new System.EventHandler(this.FrmTuVan_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private Guna.UI2.WinForms.Guna2Button guna2Button_BatDau;
    }
}
namespace WindowsFormsApp1.hethong.ITEAM
{
    partial class iteam
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
            this.panel_Border = new System.Windows.Forms.Panel();
            this.label_Type = new System.Windows.Forms.Label();
            this.label_Messsage = new System.Windows.Forms.Label();
            this.pictureBox_Icon = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Icon)).BeginInit();
            this.SuspendLayout();
            // 
            // panel_Border
            // 
            this.panel_Border.BackColor = System.Drawing.Color.White;
            this.panel_Border.Location = new System.Drawing.Point(0, -2);
            this.panel_Border.Name = "panel_Border";
            this.panel_Border.Size = new System.Drawing.Size(11, 63);
            this.panel_Border.TabIndex = 0;
            // 
            // label_Type
            // 
            this.label_Type.AutoSize = true;
            this.label_Type.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Type.Location = new System.Drawing.Point(63, 12);
            this.label_Type.Name = "label_Type";
            this.label_Type.Size = new System.Drawing.Size(46, 21);
            this.label_Type.TabIndex = 1;
            this.label_Type.Text = "Type";
            // 
            // label_Messsage
            // 
            this.label_Messsage.AutoSize = true;
            this.label_Messsage.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Messsage.Location = new System.Drawing.Point(64, 38);
            this.label_Messsage.Name = "label_Messsage";
            this.label_Messsage.Size = new System.Drawing.Size(82, 13);
            this.label_Messsage.TabIndex = 2;
            this.label_Messsage.Text = "Toast Message";
            // 
            // pictureBox_Icon
            // 
            this.pictureBox_Icon.Location = new System.Drawing.Point(17, 18);
            this.pictureBox_Icon.Name = "pictureBox_Icon";
            this.pictureBox_Icon.Size = new System.Drawing.Size(30, 30);
            this.pictureBox_Icon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox_Icon.TabIndex = 3;
            this.pictureBox_Icon.TabStop = false;
            // 
            // iteam
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 60);
            this.Controls.Add(this.pictureBox_Icon);
            this.Controls.Add(this.label_Messsage);
            this.Controls.Add(this.label_Type);
            this.Controls.Add(this.panel_Border);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "iteam";
            this.Text = "iteam";
            this.Load += new System.EventHandler(this.iteam_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Icon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel_Border;
        private System.Windows.Forms.Label label_Type;
        private System.Windows.Forms.Label label_Messsage;
        private System.Windows.Forms.PictureBox pictureBox_Icon;
    }
}
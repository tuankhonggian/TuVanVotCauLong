namespace WindowsFormsApp1.hethong.sanpham
{
    partial class SanPhamControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SanPhamControl));
            this.guna2RatingStar_Ratting = new Guna.UI2.WinForms.Guna2RatingStar();
            this.label_Ten = new System.Windows.Forms.Label();
            this.label_Gia = new System.Windows.Forms.Label();
            this.pictureBox_HinhAnh = new System.Windows.Forms.PictureBox();
            this.pictureBox1_ID = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_HinhAnh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1_ID)).BeginInit();
            this.SuspendLayout();
            // 
            // guna2RatingStar_Ratting
            // 
            resources.ApplyResources(this.guna2RatingStar_Ratting, "guna2RatingStar_Ratting");
            this.guna2RatingStar_Ratting.Name = "guna2RatingStar_Ratting";
            this.guna2RatingStar_Ratting.RatingColor = System.Drawing.Color.Gold;
            this.guna2RatingStar_Ratting.Value = 5F;
            this.guna2RatingStar_Ratting.ValueChanged += new System.EventHandler(this.guna2RatingStar_Ratting_ValueChanged);
            this.guna2RatingStar_Ratting.Click += new System.EventHandler(this.guna2RatingStar_Ratting_Click);
            // 
            // label_Ten
            // 
            resources.ApplyResources(this.label_Ten, "label_Ten");
            this.label_Ten.Name = "label_Ten";
            this.label_Ten.Click += new System.EventHandler(this.label_Ten_Click);
            // 
            // label_Gia
            // 
            resources.ApplyResources(this.label_Gia, "label_Gia");
            this.label_Gia.ForeColor = System.Drawing.Color.Red;
            this.label_Gia.Name = "label_Gia";
            this.label_Gia.Click += new System.EventHandler(this.label_Gia_Click);
            // 
            // pictureBox_HinhAnh
            // 
            resources.ApplyResources(this.pictureBox_HinhAnh, "pictureBox_HinhAnh");
            this.pictureBox_HinhAnh.Name = "pictureBox_HinhAnh";
            this.pictureBox_HinhAnh.TabStop = false;
            this.pictureBox_HinhAnh.Click += new System.EventHandler(this.pictureBox_HinhAnh_Click);
            // 
            // pictureBox1_ID
            // 
            resources.ApplyResources(this.pictureBox1_ID, "pictureBox1_ID");
            this.pictureBox1_ID.Name = "pictureBox1_ID";
            this.pictureBox1_ID.TabStop = false;
            this.pictureBox1_ID.Click += new System.EventHandler(this.pictureBox1_ID_Click);
            // 
            // SanPhamControl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.label_Ten);
            this.Controls.Add(this.label_Gia);
            this.Controls.Add(this.pictureBox_HinhAnh);
            this.Controls.Add(this.pictureBox1_ID);
            this.Controls.Add(this.guna2RatingStar_Ratting);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Name = "SanPhamControl";
            this.Load += new System.EventHandler(this.SanPhamControl_Load);
            this.Click += new System.EventHandler(this.SanPhamControl_Click_1);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_HinhAnh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1_ID)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Guna.UI2.WinForms.Guna2RatingStar guna2RatingStar_Ratting;
        private System.Windows.Forms.Label label_Ten;
        private System.Windows.Forms.PictureBox pictureBox1_ID;
        private System.Windows.Forms.PictureBox pictureBox_HinhAnh;
        private System.Windows.Forms.Label label_Gia;
    }
}

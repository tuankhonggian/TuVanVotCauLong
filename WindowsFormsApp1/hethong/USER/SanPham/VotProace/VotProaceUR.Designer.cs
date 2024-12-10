namespace WindowsFormsApp1.hethong.USER.SanPham.VotProace
{
    partial class VotProaceAD
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VotProaceAD));
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.sanPhamControl1 = new WindowsFormsApp1.hethong.sanpham.SanPhamControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.guna2Button_LamMoi = new Guna.UI2.WinForms.Guna2Button();
            this.guna2ComboBox_TKgia = new Guna.UI2.WinForms.Guna2ComboBox();
            this.gunuHighligh_TimKiem = new Guna.UI2.WinForms.Guna2TextBox();
            this.flowLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.AutoScrollMargin = new System.Drawing.Size(0, 1);
            this.flowLayoutPanel1.AutoScrollMinSize = new System.Drawing.Size(0, 1);
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.White;
            this.flowLayoutPanel1.Controls.Add(this.sanPhamControl1);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(6, 72);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1228, 955);
            this.flowLayoutPanel1.TabIndex = 9;
            // 
            // sanPhamControl1
            // 
            this.sanPhamControl1.BackColor = System.Drawing.Color.White;
            this.sanPhamControl1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.sanPhamControl1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.sanPhamControl1.GiaSanPham = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.sanPhamControl1.HinhAnh = null;
            this.sanPhamControl1.Location = new System.Drawing.Point(5, 5);
            this.sanPhamControl1.Margin = new System.Windows.Forms.Padding(5);
            this.sanPhamControl1.Name = "sanPhamControl1";
            this.sanPhamControl1.ProductID = "0";
            this.sanPhamControl1.Size = new System.Drawing.Size(285, 246);
            this.sanPhamControl1.TabIndex = 0;
            this.sanPhamControl1.TenSanPham = "Vợt Yonex";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(99)))), ((int)(((byte)(160)))));
            this.panel1.Controls.Add(this.guna2Button_LamMoi);
            this.panel1.Controls.Add(this.guna2ComboBox_TKgia);
            this.panel1.Controls.Add(this.gunuHighligh_TimKiem);
            this.panel1.Location = new System.Drawing.Point(-3, -8);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1237, 76);
            this.panel1.TabIndex = 10;
            // 
            // guna2Button_LamMoi
            // 
            this.guna2Button_LamMoi.BorderRadius = 15;
            this.guna2Button_LamMoi.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button_LamMoi.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button_LamMoi.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.guna2Button_LamMoi.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.guna2Button_LamMoi.FillColor = System.Drawing.Color.White;
            this.guna2Button_LamMoi.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.guna2Button_LamMoi.ForeColor = System.Drawing.Color.White;
            this.guna2Button_LamMoi.Image = ((System.Drawing.Image)(resources.GetObject("guna2Button_LamMoi.Image")));
            this.guna2Button_LamMoi.Location = new System.Drawing.Point(491, 30);
            this.guna2Button_LamMoi.Margin = new System.Windows.Forms.Padding(4);
            this.guna2Button_LamMoi.Name = "guna2Button_LamMoi";
            this.guna2Button_LamMoi.PressedColor = System.Drawing.Color.White;
            this.guna2Button_LamMoi.Size = new System.Drawing.Size(40, 42);
            this.guna2Button_LamMoi.TabIndex = 6;
            this.guna2Button_LamMoi.Click += new System.EventHandler(this.guna2Button_LamMoi_Click);
            // 
            // guna2ComboBox_TKgia
            // 
            this.guna2ComboBox_TKgia.BackColor = System.Drawing.Color.Transparent;
            this.guna2ComboBox_TKgia.BorderRadius = 15;
            this.guna2ComboBox_TKgia.BorderThickness = 0;
            this.guna2ComboBox_TKgia.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.guna2ComboBox_TKgia.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.guna2ComboBox_TKgia.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.guna2ComboBox_TKgia.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.guna2ComboBox_TKgia.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.guna2ComboBox_TKgia.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.guna2ComboBox_TKgia.ItemHeight = 30;
            this.guna2ComboBox_TKgia.Location = new System.Drawing.Point(297, 28);
            this.guna2ComboBox_TKgia.Margin = new System.Windows.Forms.Padding(4);
            this.guna2ComboBox_TKgia.Name = "guna2ComboBox_TKgia";
            this.guna2ComboBox_TKgia.Size = new System.Drawing.Size(184, 36);
            this.guna2ComboBox_TKgia.TabIndex = 2;
            this.guna2ComboBox_TKgia.SelectedIndexChanged += new System.EventHandler(this.guna2ComboBox_TKgia_SelectedIndexChanged);
            // 
            // gunuHighligh_TimKiem
            // 
            this.gunuHighligh_TimKiem.BorderRadius = 15;
            this.gunuHighligh_TimKiem.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.gunuHighligh_TimKiem.DefaultText = "";
            this.gunuHighligh_TimKiem.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.gunuHighligh_TimKiem.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.gunuHighligh_TimKiem.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.gunuHighligh_TimKiem.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.gunuHighligh_TimKiem.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.gunuHighligh_TimKiem.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.gunuHighligh_TimKiem.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.gunuHighligh_TimKiem.IconLeft = ((System.Drawing.Image)(resources.GetObject("gunuHighligh_TimKiem.IconLeft")));
            this.gunuHighligh_TimKiem.Location = new System.Drawing.Point(9, 28);
            this.gunuHighligh_TimKiem.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gunuHighligh_TimKiem.Name = "gunuHighligh_TimKiem";
            this.gunuHighligh_TimKiem.PasswordChar = '\0';
            this.gunuHighligh_TimKiem.PlaceholderText = "";
            this.gunuHighligh_TimKiem.SelectedText = "";
            this.gunuHighligh_TimKiem.Size = new System.Drawing.Size(281, 43);
            this.gunuHighligh_TimKiem.TabIndex = 0;
            this.gunuHighligh_TimKiem.TextChanged += new System.EventHandler(this.gunuHighligh_TimKiem_TextChanged);
            this.gunuHighligh_TimKiem.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gunuHighligh_TimKiem_KeyDown);
            // 
            // VotProaceUR
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1231, 1019);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "VotProaceUR";
            this.Text = "VotProaceUR";
            this.Load += new System.EventHandler(this.VotProaceUR_Load);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private sanpham.SanPhamControl sanPhamControl1;
        private System.Windows.Forms.Panel panel1;
        private Guna.UI2.WinForms.Guna2Button guna2Button_LamMoi;
        private Guna.UI2.WinForms.Guna2ComboBox guna2ComboBox_TKgia;
        private Guna.UI2.WinForms.Guna2TextBox gunuHighligh_TimKiem;
    }
}
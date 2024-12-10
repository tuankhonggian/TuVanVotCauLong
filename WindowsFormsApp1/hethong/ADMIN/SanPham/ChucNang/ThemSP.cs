using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.hethong.ITEAM;
using WindowsFormsApp1.ketnoi.dangnhap;

namespace WindowsFormsApp1.hethong.sanpham
{
    public partial class ThemSP : Form
    {
        ketnoiDN kn = new ketnoiDN();
        private int progressBarValue = 0;
        private bool isDragging = false;
        private Point startPoint = new Point(0, 0);
        public ThemSP()
        {
            InitializeComponent();
            // Khởi tạo timer
            timer1 = new Timer();
            timer1.Interval = 50; // Cập nhật mỗi 50ms
            timer1.Tick += timer1_Tick;

            guna2ProgressBar_Them.Visible = false; // Ẩn ProgressBar khi khởi tạo
            // Di chuyển header
            panel_Header.MouseDown += new MouseEventHandler(panel_Header_MouseDown);
            panel_Header.MouseMove += new MouseEventHandler(panel_Header_MouseMove);
            panel_Header.MouseUp += new MouseEventHandler(panel_Header_MouseUp);
        }
        private void panel_Header_MouseDown(object sender, MouseEventArgs e)
        {
            isDragging = true;
            startPoint = new Point(e.X, e.Y);
        }

        private void panel_Header_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point p = PointToScreen(e.Location);
                this.Location = new Point(p.X - startPoint.X, p.Y - startPoint.Y);
            }
        }

        private void panel_Header_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }

        private void LoadComboBoxData()
        {
            // Cập nhật dữ liệu cho ComboBox
            LoadComboBoxData("SELECT DISTINCT ThuongHieu FROM [TT.VotYonex]", guna2ComboBox_ThuongHieu);
            LoadComboBoxData("SELECT DISTINCT LoaiVot FROM [TT.VotYonex]", guna_LoaiVot);
            LoadComboBoxData("SELECT DISTINCT TrongLuong FROM [TT.VotYonex]", guna2ComboBox_TrongLuong);
            LoadComboBoxData("SELECT DISTINCT ChuViCan FROM [TT.VotYonex]", guna2ComboBox_ChuViCanVot);
            LoadComboBoxData("SELECT DISTINCT SucCang FROM [TT.VotYonex]", guna2ComboBox_SucCangVot);
            LoadComboBoxData("SELECT DISTINCT DiemCanBang FROM [TT.VotYonex]", guna2ComboBox_DiemCanBang);
            LoadComboBoxData("SELECT DISTINCT DoCung FROM [TT.VotYonex]", guna2ComboBox_DoCung);
            LoadComboBoxData("SELECT DISTINCT DangMatVot FROM [TT.VotYonex]", guna2ComboBox_DangMatVot);
            LoadComboBoxData("SELECT DISTINCT TrongLuongVung FROM [TT.VotYonex]", guna2ComboBox_TrongLuongVung);
            LoadComboBoxData("SELECT DISTINCT TroLuc FROM [TT.VotYonex]", guna2ComboBox_TroLuc);
        }
        private void LoadComboBoxData(string query, ComboBox comboBox)
        {
            DataSet ds = kn.Laydulieu(query);

            if (ds != null && ds.Tables.Count > 0)
            {
                comboBox.Items.Clear();
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    comboBox.Items.Add(row[0].ToString());
                }
            }
        }

        private void guna2Button_Back_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void ShowNotification(string message, string type)
        {
            iteam notification = new iteam(message, type);
            notification.Show();
        }
        private void guna2ProgressBar_Them_ValueChanged(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (guna2ProgressBar_Them.Value < 100)
            {
                guna2ProgressBar_Them.Value += 1; // Tăng giá trị của ProgressBar
            }
            else
            {
                timer1.Stop(); // Dừng timer khi hoàn tất
                guna2ProgressBar_Them.Visible = false; // Ẩn ProgressBar
                ShowNotification("Sản phẩm đã được thêm thành công.", "Success"); // Hiển thị thông báo
                this.Close();

            }
        }

        private void pictureBox_Anh_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "D:\\CODE\\votyonex\\";
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Lấy đường dẫn ảnh được chọn
                    string selectedImagePath = openFileDialog.FileName;


                    // Hiển thị ảnh mới trong PictureBox
                    pictureBox_AnhSanPham.ImageLocation = selectedImagePath;
                    pictureBox_AnhSanPham.Load();
                }
            }
        }

        private void richTextBox_MoTa_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void guna2ComboBox_TroLuc_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void guna2ComboBox_DangMatVot_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void guna2ComboBox_TrongLuongVung_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void guna2ComboBox_DoCung_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void guna2ComboBox_DiemCanBang_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void guna2ComboBox_SucCangVot_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void guna2ComboBox_ChuViCanVot_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void guna2ComboBox_TrongLuong_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void guna_LoaiVot_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void guna2ComboBox_ThuongHieu_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void panel_Header_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Button_Back_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void guna2TextBox_TenSanPham_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox_AnhSanPham_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void guna2ProgressBar_Update_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label_MoTa_Click(object sender, EventArgs e)
        {

        }

        private void label_Gia_Click(object sender, EventArgs e)
        {

        }

        private void label_CongNghe_Click(object sender, EventArgs e)
        {

        }

        private void label_MauSac_Click(object sender, EventArgs e)
        {

        }

        private void label_DiemCanBang_Click(object sender, EventArgs e)
        {

        }

        private void label_ChieuDai_Click(object sender, EventArgs e)
        {

        }

        private void label_DoCung_Click(object sender, EventArgs e)
        {

        }

        private void label_TrongLuong_Click(object sender, EventArgs e)
        {

        }

        private void label_ChatLieu_Click(object sender, EventArgs e)
        {

        }

        private void label_LoaiVot_Click(object sender, EventArgs e)
        {

        }

        private void label_HangSanXuat_Click(object sender, EventArgs e)
        {

        }

        private void guna2TextBox_ChatLieu_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2TextBox_Gia_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2TextBox_MauSac_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2TextBox_ChieuDai_TextChanged(object sender, EventArgs e)
        {

        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {

        }

        private void timer2_Tick(object sender, EventArgs e)
        {

        }

        private void guna2Button_Them_Click(object sender, EventArgs e)
        {
            // Lấy dữ liệu từ các control trên form
            string tenSanPham = guna2TextBox_TenSanPham.Text.Trim();
            string thuongHieu = guna2ComboBox_ThuongHieu.Text.Trim();
            string loaiVot = guna_LoaiVot.Text.Trim();
            string chatLieu = guna2TextBox_ChatLieu.Text.Trim();
            string trongLuong = guna2ComboBox_TrongLuong.Text.Trim();
            string chuViCan = guna2ComboBox_ChuViCanVot.Text.Trim();
            string sucCang = guna2ComboBox_SucCangVot.Text.Trim();
            string diemCanBang = guna2ComboBox_DiemCanBang.Text.Trim();
            string doCung = guna2ComboBox_DoCung.Text.Trim();
            string dangMatVot = guna2ComboBox_DangMatVot.Text.Trim();
            string trongLuongVung = guna2ComboBox_TrongLuongVung.Text.Trim();
            string troLuc = guna2ComboBox_TroLuc.Text.Trim();
            string mauSac = guna2TextBox_MauSac.Text.Trim();
            string moTa = richTextBox_MoTa.Text.Trim();
            string anhDaiDien = pictureBox_AnhSanPham.ImageLocation;
            string chieuDai = guna2TextBox_ChieuDai.Text.Trim();
            decimal gia;

            // Kiểm tra các trường thông tin
            if (string.IsNullOrEmpty(tenSanPham) ||
                string.IsNullOrEmpty(thuongHieu) ||
                string.IsNullOrEmpty(loaiVot) ||
                string.IsNullOrEmpty(chatLieu) ||
                string.IsNullOrEmpty(trongLuong) ||
                string.IsNullOrEmpty(chuViCan) ||
                string.IsNullOrEmpty(sucCang) ||
                !decimal.TryParse(chieuDai, out decimal chieuDaiDecimal) ||
                string.IsNullOrEmpty(diemCanBang) ||
                string.IsNullOrEmpty(doCung) ||
                string.IsNullOrEmpty(dangMatVot) ||
                string.IsNullOrEmpty(trongLuongVung) ||
                string.IsNullOrEmpty(troLuc) ||
                !decimal.TryParse(guna2TextBox_Gia.Text.Trim(), out gia) ||
                string.IsNullOrEmpty(moTa) ||
                string.IsNullOrEmpty(anhDaiDien))
            {
                ShowNotification("Vui lòng nhập đầy đủ thông tin vào tất cả các trường.", "Error");
                return; // Ngừng thực hiện nếu có trường thông tin còn thiếu
            }

            // Tạo đối tượng ketnoiDN
            ketnoiDN kn = new ketnoiDN();

            // Gọi phương thức ThemSanPham để thêm sản phẩm
            //bool success = kn.ThemSanPham(
            //    tenSanPham,
            //    thuongHieu,
            //    loaiVot,
            //    chatLieu,
            //    trongLuong,
            //    chuViCan,
            //    sucCang,
            //    chieuDai,
            //    diemCanBang,
            //    doCung,
            //    dangMatVot,
            //    trongLuongVung,
            //    mauSac,
            //    troLuc,
            //    gia,
            //    moTa,
            //    anhDaiDien
            //);

            //if (success)
            //{
            //    guna2ProgressBar_Them.Visible = true; // Hiển thị ProgressBar khi bắt đầu tiến trình
            //    guna2ProgressBar_Them.Value = 0; // Đặt giá trị ProgressBar về 0
            //    timer1.Start(); // Bắt đầu timer để cập nhật ProgressBar
            //}
            //else
            //{
            //    ShowNotification("Có lỗi xảy ra khi thêm sản phẩm.", "Error");
            //}
        }

        private void ThemSP_Load(object sender, EventArgs e)
        {
            LoadComboBoxData(); // Load dữ liệu cho ComboBox
        }
    }
}

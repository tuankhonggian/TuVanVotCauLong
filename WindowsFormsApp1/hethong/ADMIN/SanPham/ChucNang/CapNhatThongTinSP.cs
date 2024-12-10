using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using WindowsFormsApp1.hethong.ITEAM;
using WindowsFormsApp1.ketnoi.dangnhap;

namespace WindowsFormsApp1.hethong.sanpham
{
    public partial class CapNhatThongTinSP : Form
    {
        private int ProductID;
        ketnoiDN kn = new ketnoiDN();
        private bool isDragging = false;
        private Point startPoint = new Point(0, 0);
        public CapNhatThongTinSP(int productId)
        {
            InitializeComponent();
            ProductID = productId;
            LoadProductDetails();



            // Ẩn ProgressBar khi khởi tạo
            guna2ProgressBar_Update.Visible = false; 
            // Cấu hình Timer
            timer1.Interval = 50; // Thay đổi khoảng thời gian nếu cần
            timer1.Tick += timer1_Tick;
            // Di chuyển header
            panel_Header.MouseDown += new MouseEventHandler(panel_Header_MouseDown);
            panel_Header.MouseMove += new MouseEventHandler(panel_Header_MouseMove);
            panel_Header.MouseUp += new MouseEventHandler(panel_Header_MouseUp);
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
        private void LoadProductDetails()
        {

            // Truy vấn cơ sở dữ liệu để lấy thông tin sản phẩm dựa trên ProductID
            string query = $"SELECT * FROM [TT.VotYonex] WHERE SanPhamID = {ProductID}";
            DataSet ds = kn.Laydulieu(query);

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataRow row = ds.Tables[0].Rows[0];

                // Gán thông tin sản phẩm vào các control trên form
                guna2TextBox_TenSanPham.Text = row["TenSanPham"].ToString();
                guna2TextBox_ChatLieu.Text = row["ChatLieu"].ToString();
                guna2TextBox_ChieuDai.Text = row["ChieuDai"].ToString();
                guna2TextBox_MauSac.Text = row["MauSac"].ToString();
                guna2TextBox_Gia.Text = $"{Convert.ToDecimal(row["Gia"]):N0} VND"; // Hiển thị giá theo định dạng tiền tệ
                richTextBox_MoTa.Text = row["MoTa"].ToString();

                // Tìm và đặt giá trị cho các ComboBox thông qua FindStringExact
                SetComboBoxValue(guna2ComboBox_ThuongHieu, row["ThuongHieu"].ToString());
                SetComboBoxValue(guna_LoaiVot, row["LoaiVot"].ToString());
                SetComboBoxValue(guna2ComboBox_TrongLuong, row["TrongLuong"].ToString());
                SetComboBoxValue(guna2ComboBox_ChuViCanVot, row["ChuViCan"].ToString());
                SetComboBoxValue(guna2ComboBox_SucCangVot, row["SucCang"].ToString());
                SetComboBoxValue(guna2ComboBox_DiemCanBang, row["DiemCanBang"].ToString());
                SetComboBoxValue(guna2ComboBox_DoCung, row["DoCung"].ToString());
                SetComboBoxValue(guna2ComboBox_DangMatVot, row["DangMatVot"].ToString());
                SetComboBoxValue(guna2ComboBox_TrongLuongVung, row["TrongLuongVung"].ToString());
                SetComboBoxValue(guna2ComboBox_TroLuc, row["TroLuc"].ToString());
            // Hiển thị ảnh sản phẩm
        string anhDaiDien = row["AnhSanPham"].ToString(); // Đảm bảo rằng bạn có trường này trong bảng
                if (!string.IsNullOrEmpty(anhDaiDien) && File.Exists(anhDaiDien))
                {
                    pictureBox_AnhSanPham.ImageLocation = anhDaiDien;
                }
                else
                {
                    pictureBox_AnhSanPham.Image = null; // Xóa ảnh nếu không tồn tại
                }
            }
            else
            {
                MessageBox.Show("Không tìm thấy thông tin sản phẩm hoặc có lỗi xảy ra trong quá trình tải dữ liệu.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void SetComboBoxValue(ComboBox comboBox, string value)
        {
            int index = comboBox.FindStringExact(value);
            if (index != -1)
            {
                comboBox.SelectedIndex = index;
            }
            else
            {
                comboBox.SelectedIndex = -1; // Hoặc giá trị mặc định
            }
        }

        private void pictureBox_AnhSanPham_Click(object sender, EventArgs e)
        {

        }


        private void guna2Button_Back_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void ReloadProductDetails()
        {
            LoadProductDetails(); // Tải lại thông tin sản phẩm
        }
        private void TTsanphamFrm_Load(object sender, EventArgs e)
        {
            LoadComboBoxData(); // Load dữ liệu cho ComboBox
            LoadProductDetails(); // Load chi tiết sản phẩm
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

       
        int startpoint = 0;
        private void guna2Button_CapNhat_Click(object sender, EventArgs e)
        {
            // Hiển thị progress bar và bắt đầu timer
            guna2ProgressBar_Update.Visible = true;
            guna2ProgressBar_Update.Value = 0;
            timer1.Start();
        }

        

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (guna2ProgressBar_Update.Value < 100)
            {
                guna2ProgressBar_Update.Value += 1; // Tăng giá trị của progress bar
            }
            else
            {
                // Dừng timer và thực hiện cập nhật thông tin
                timer1.Stop();
                guna2ProgressBar_Update.Visible = false;
                guna2ProgressBar_Update.Value = 0;

                // Lấy thông tin từ các TextBox
                string tenSanPham = guna2TextBox_TenSanPham.Text.Trim();
                string hangSanXuat = guna2ComboBox_ThuongHieu.Text.Trim();
                string loaiVot = guna_LoaiVot.Text.Trim();
                string chatLieu = guna2TextBox_ChatLieu.Text.Trim();
                string trongLuong = guna2ComboBox_TrongLuong.Text.Trim();
                string chuvican = guna2ComboBox_ChuViCanVot.Text.Trim();
                string succang = guna2ComboBox_SucCangVot.Text.Trim();
                string chieuDai = guna2TextBox_ChieuDai.Text.Trim();
                string diemCanBang = guna2ComboBox_DiemCanBang.Text.Trim();
                string doCung = guna2ComboBox_DoCung.Text.Trim();
                string dangmatvot = guna2ComboBox_DangMatVot.Text.Trim();
                string trongluongvung = guna2ComboBox_TrongLuongVung.Text.Trim();
                string mauSac = guna2TextBox_MauSac.Text.Trim();
                string troluc = guna2ComboBox_TroLuc.Text.Trim();
                string moTa = richTextBox_MoTa.Text.Trim();

                // Kiểm tra và chuyển đổi giá trị
                decimal gia;
                if (!decimal.TryParse(guna2TextBox_Gia.Text.Replace(" VND", "").Replace(",", "").Trim(), out gia))
                {
                    // Tạo và hiển thị thông báo lỗi
                    iteam errorNotification = new iteam("Giá trị nhập vào không hợp lệ. Vui lòng kiểm tra lại!", "Error");
                    errorNotification.Show();
                    return;
                }

                // Đường dẫn ảnh đại diện
                string anhDaiDien = pictureBox_AnhSanPham.ImageLocation ?? "";

                // Gọi phương thức cập nhật dữ liệu
                //bool success = kn.CapNhatSanPham(ProductID, tenSanPham, hangSanXuat, loaiVot, chatLieu, trongLuong, chuvican, succang, chieuDai, diemCanBang, doCung, dangmatvot, trongluongvung, mauSac, troluc, gia, moTa, anhDaiDien);

                //string message = success
                //    ? "Thông tin sản phẩm đã được cập nhật thành công."
                //    : "Có lỗi xảy ra khi cập nhật thông tin sản phẩm.";

                //// Xác định loại thông báo
                //string type = success ? "Success" : "Error";

                //// Tạo và hiển thị thông báo
                //iteam notification = new iteam(message, type);
                //notification.Show();

                //if (success)
                //{
                //    // Tải lại thông tin sản phẩm sau khi cập nhật thành công
                //    ReloadProductDetails();
                //}
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {

        }

        private void guna2ProgressBar_Update_ValueChanged(object sender, EventArgs e)
        {

        }

        private void guna2TextBox_TenSanPham_TextChanged(object sender, EventArgs e)
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

        

        

        private void pictureBox_AnhSanPham_Click_1(object sender, EventArgs e)
        {

        }
        // Đổi ảnh sản phẩm
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

        private void panel_Header_Paint(object sender, PaintEventArgs e)
        {

        }
    }


}

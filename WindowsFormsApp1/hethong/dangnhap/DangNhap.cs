using System;
using System.Data;
using System.Windows.Forms;
using WindowsFormsApp1.hethong.dangky;
using WindowsFormsApp1.hethong.ITEAM;
using WindowsFormsApp1.hethong.trangchu;
using WindowsFormsApp1.ketnoi.dangnhap;

namespace WindowsFormsApp1.hethong.dangnhap
{
    public partial class DangNhap : Form
    {

        ketnoiDN kn = new ketnoiDN();
        private bool isPasswordVisible = false; // Biến để theo dõi trạng thái mật khẩu
        public DangNhap()
        {
            InitializeComponent();
            guna2PictureBox3.Image = Properties.Resources.hidden; // Mặc định là mắt đóng
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        // Dang nhap
        private void guna2Button_DangNhap_Click(object sender, EventArgs e)
        {
            string taiKhoan = guna2TextBox_TaiKhoan.Text.Trim();
            string matKhau = guna2TextBox_MatKhau.Text.Trim();

            if (string.IsNullOrEmpty(taiKhoan) || string.IsNullOrEmpty(matKhau))
            {
                // Hiển thị thông báo lỗi với form iteam
                iteam errorMessage = new iteam("Vui lòng không bỏ trống thông tin!", "Error");
                errorMessage.Show();
                return;
            }

            string truyvan = $"SELECT COUNT(*) FROM [Account] WHERE TaiKhoan = '{taiKhoan}' AND MatKhau = '{matKhau}'";
            DataSet ds = kn.Laydulieu(truyvan);

            if (ds != null && ds.Tables[0].Rows[0][0].ToString() == "1")
            {
                string quyenHan = kn.LayQuyenHan(taiKhoan, matKhau);
                if (quyenHan == "Admin")
                {
                    // Hiển thị thông báo thành công với form iteam
                    iteam successMessage = new iteam("Đăng nhập thành công chiến nào!", "Success");
                    successMessage.Show();
                    // Điều hướng đến trang chính của Admin
                    ADtrangchu ad = new ADtrangchu(); // Thay thế bằng trang chính của Admin
                    ad.Show();
                    this.Hide();
                }
                else if (quyenHan == "User")
                {
                    // Hiển thị thông báo thành công với form iteam
                    iteam successMessage = new iteam("Đăng nhập thành công chiến nào!", "Success");
                    successMessage.Show();
                    // Điều hướng đến trang chính của User
                    URTrangChu ur = new URTrangChu(); // Thay thế bằng trang chính của User
                    ur.Show();
                    this.Hide();
                }
                else
                {
                    // Hiển thị thông báo lỗi với form iteam
                    iteam errorMessage = new iteam("Thông tin quyền hạn không hợp lệ!", "Error");
                    errorMessage.Show();
                }
            }
            else
            {
                // Hiển thị thông báo lỗi với form iteam
                iteam errorMessage = new iteam("Sai thông tin tài khoản hoặc mật khẩu!", "Error");
                errorMessage.Show();
            }
        }
        // Thoat ung dung
        private void pictureBox2_Cancel_Click(object sender, EventArgs e)
        {
            // Hiển thị hộp thoại xác nhận khi người dùng nhấn nút thoát
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát không?", "Xác nhận thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            // Kiểm tra kết quả từ hộp thoại xác nhận
            if (result == DialogResult.Yes)
            {
                // Nếu người dùng chọn "Yes", đóng ứng dụng
                Application.Exit(); // Hoặc sử dụng this.Close() để chỉ đóng form hiện tại
            }
            // Nếu người dùng chọn "No", không làm gì và quay lại form
        }

        // Tat An Mat Khau
        private void guna2PictureBox3_Click(object sender, EventArgs e)
        {
            if (isPasswordVisible)
            {
                // Ẩn mật khẩu
                guna2TextBox_MatKhau.PasswordChar = '*'; // Thay thế ký tự mật khẩu với '*'
                guna2PictureBox3.Image = Properties.Resources.hidden; // Đặt lại ảnh mắt mở
                isPasswordVisible = false;
            }
            else
            {
                // Hiển thị mật khẩu
                guna2TextBox_MatKhau.PasswordChar = '\0'; // Hiển thị mật khẩu mà không có ký tự thay thế
                guna2PictureBox3.Image = Properties.Resources.eye; // Đặt ảnh mắt mở
                isPasswordVisible = true;
            }
        }
        // Chuyen trang dang ky
        

        private void linkLabel_DangKy_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Tạo thể hiện của form đăng ký
            DangKy dk = new DangKy(); // Thay thế bằng tên của form đăng ký bạn đã tạo

            // Hiển thị form đăng ký
            dk.Show();
            this.Hide();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.hethong.dangnhap;
using WindowsFormsApp1.hethong.ITEAM;
using WindowsFormsApp1.ketnoi.dangnhap;

namespace WindowsFormsApp1.hethong.dangky
{
    public partial class DangKy : Form
    {
        ketnoiDN kn = new ketnoiDN();
        private bool isPasswordVisible = false; // Biến để theo dõi trạng thái mật khẩu
        public DangKy()
        {
            InitializeComponent();
            guna2PictureBox3.Image = Properties.Resources.eye; // Mặc định là mắt đóng
            guna2PictureBox1.Image = Properties.Resources.eye; // Mặc định là mắt đóng
        }

        private void guna2CheckBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void guna2PictureBox3_Click(object sender, EventArgs e)
        {
            if (isPasswordVisible)
            {
                // Ẩn mật khẩu
                guna2TextBox_MatKhau.PasswordChar = '*'; // Thay thế ký tự mật khẩu với '*'
                guna2PictureBox3.Image = Properties.Resources.eye; // Đặt lại ảnh mắt mở
                isPasswordVisible = false;
            }
            else
            {
                // Hiển thị mật khẩu
                guna2TextBox_MatKhau.PasswordChar = '\0'; // Hiển thị mật khẩu mà không có ký tự thay thế
                guna2PictureBox3.Image = Properties.Resources.hidden; // Đặt ảnh mắt mở
                isPasswordVisible = true;
            }
        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {
            if (isPasswordVisible)
            {
                // Ẩn mật khẩu
                guna2TextBox_CheckMK.PasswordChar = '*'; // Thay thế ký tự mật khẩu với '*'
                guna2PictureBox1.Image = Properties.Resources.eye; // Đặt lại ảnh mắt mở
                isPasswordVisible = false;
            }
            else
            {
                // Hiển thị mật khẩu
                guna2TextBox_CheckMK.PasswordChar = '\0'; // Hiển thị mật khẩu mà không có ký tự thay thế
                guna2PictureBox1.Image = Properties.Resources.hidden; // Đặt ảnh mắt mở
                isPasswordVisible = true;
            }
        }

        private void pictureBox2_Cancel_Click(object sender, EventArgs e)
        {
            // Tạo thể hiện của form đăng nhập
            DangNhap dn = new DangNhap(); // Thay thế bằng tên của form đăng nhập bạn đã tạo

            // Hiển thị form đăng nhập
            dn.Show();

            // Đóng hoặc ẩn form hiện tại
            this.Close(); // Hoặc this.Hide() nếu bạn muốn ẩn form hiện tại
        }

        private void guna2CheckBox_Accept_CheckedChanged(object sender, EventArgs e)
        {

        }
        // Đăng Ký 
        private void guna2Butt25_DangKy_Click(object sender, EventArgs e)
        {
            // Lấy thông tin từ các điều khiển trên form
            string hoten = guna2TextBox_HoVaTen.Text.Trim();
            string taiKhoan = guna2TextBox_TaiKhoan.Text.Trim();
            string matKhau = guna2TextBox_MatKhau.Text.Trim();
            string xacNhanMatKhau = guna2TextBox_CheckMK.Text.Trim();
            string email = guna2TextBox_Email.Text.Trim();

            // Kiểm tra nếu tất cả các trường không được để trống
            if (string.IsNullOrEmpty(hoten) || string.IsNullOrEmpty(taiKhoan) || string.IsNullOrEmpty(matKhau) ||
                string.IsNullOrEmpty(xacNhanMatKhau) || string.IsNullOrEmpty(email))
            {
                iteam errorMessage = new iteam("Vui lòng điền đầy đủ thông tin!", "Error");
                errorMessage.Show();
                return;
            }

            // Kiểm tra mật khẩu có ít nhất 6 ký tự
            if (matKhau.Length < 6)
            {
                iteam errorMessage = new iteam("Mật khẩu phải có ít nhất 6 ký tự!", "Error");
                errorMessage.Show();
                return;
            }

            // Kiểm tra xác nhận mật khẩu có ít nhất 6 ký tự
            if (xacNhanMatKhau.Length < 6)
            {
                iteam errorMessage = new iteam("Xác nhận mật khẩu phải có ít nhất 6 ký tự!", "Error");
                errorMessage.Show();
                return;
            }

            // Kiểm tra xác nhận mật khẩu khớp với mật khẩu
            if (matKhau != xacNhanMatKhau)
            {
                iteam errorMessage = new iteam("Mật khẩu không khớp!", "Error");
                errorMessage.Show();
                return;
            }
            // Kiểm tra định dạng email
            string emailPattern = @"^[a-zA-Z0-9._%+-]+@gmail\.com(\.vn)?$";
            if (!System.Text.RegularExpressions.Regex.IsMatch(email, emailPattern))
            {
                iteam errorMessage = new iteam("Email không đúng định dạng! (Phải là @gmail.com hoặc @gmail.com.vn)", "Error");
                errorMessage.Show();
                return;
            }
            // Kiểm tra đã chấp nhận điều khoản
            if (!guna2CheckBox_Accept.Checked)
            {
                iteam errorMessage = new iteam("Vui lòng chấp nhận điều khoản trước khi đăng ký!", "Error");
                errorMessage.Show();
                return;
            }
            // Thực hiện đăng ký
            // Tạo câu lệnh SQL để thêm dữ liệu vào cơ sở dữ liệu
            string truyVan = $"INSERT INTO [Account] (TaiKhoan, MatKhau,HoVaTen ,Email) VALUES ('{taiKhoan}', '{matKhau}','{hoten}', '{email}')";
            bool result = kn.Thucthi(truyVan);

            // Hiển thị thông báo thành công hoặc lỗi
            if (result)
            {
                iteam successMessage = new iteam("Cảm ơn bạn đã đăng ký. Quay trở lại đăng nhập nào!", "Success");
                successMessage.Show();
                // Tạo thể hiện của form đăng nhập và hiển thị
                DangNhap dn = new DangNhap(); // Tạo một đối tượng của form đăng nhập
                dn.Show();

                // Đóng form đăng ký
                this.Close(); // Hoặc this.Hide() nếu bạn muốn ẩn form hiện tại thay vì đóng
            }
            else
            {
                iteam errorMessage = new iteam("Đăng ký không thành công. Vui lòng thử lại sau!", "Error");
                errorMessage.Show();
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }
    }
}

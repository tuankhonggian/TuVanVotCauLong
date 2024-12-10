using System;
using System.Windows.Forms;

namespace WindowsFormsApp1.hethong.sanpham
{
    public partial class SanPhamControl : UserControl
    {

        // Các thuộc tính để gán dữ liệu sản phẩm
        public string TenSanPham
        {
            get { return label_Ten.Text; }
            set { label_Ten.Text = value; }
        }

        public decimal GiaSanPham
        {
            get
            {
                // Kiểm tra nếu có thể chuyển đổi chuỗi sang decimal
                decimal gia;
                if (decimal.TryParse(label_Gia.Text.Replace("VND", "").Replace(",", "").Trim(), out gia))
                {
                    return gia;
                }
                else
                {
                    return 0; // Giá trị mặc định nếu không thể chuyển đổi
                }
            }
            set { label_Gia.Text = $"{value:N0} VND"; }
        }


        // Thuộc tính để gán đường dẫn hình ảnh từ cơ sở dữ liệu
        public string HinhAnh
        {
            get { return pictureBox_HinhAnh.ImageLocation; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    try
                    {
                        // Kiểm tra nếu đường dẫn hình ảnh hợp lệ
                        pictureBox_HinhAnh.ImageLocation = value;
                        pictureBox_HinhAnh.Load(); // Tải hình ảnh từ đường dẫn
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Không thể tải hình ảnh: " + ex.Message);
                    }
                }
            }
        }

        public string ProductID { get; set; } // Thay đổi kiểu thành string
        public event EventHandler OnSanPhamClick;
        public SanPhamControl()
        {
            InitializeComponent();
            this.Click += SanPhamControl_Click;
            label_Ten.Click += SanPhamControl_Click;
            label_Gia.Click += SanPhamControl_Click;
            pictureBox_HinhAnh.Click += SanPhamControl_Click;
        }
        private void SanPhamControl_Click(object sender, EventArgs e)
        {
            OnSanPhamClick?.Invoke(this, new EventArgs());
        }
        private void pictureBox1_ID_Click(object sender, EventArgs e)
        {

        }
        
        private void pictureBox_HinhAnh_Click(object sender, EventArgs e)
        {
        }

        private void label_Ten_Click(object sender, EventArgs e)
        {
        }

        private void label_Gia_Click(object sender, EventArgs e)
        {
        }

        private void guna2RatingStar_Ratting_ValueChanged(object sender, EventArgs e)
        {

        }

        private void SanPhamControl_Load(object sender, EventArgs e)
        {

        }

        private void SanPhamControl_Click_1(object sender, EventArgs e)
        {
            
        }

        private void guna2RatingStar_Ratting_Click(object sender, EventArgs e)
        {
        }
    }
}

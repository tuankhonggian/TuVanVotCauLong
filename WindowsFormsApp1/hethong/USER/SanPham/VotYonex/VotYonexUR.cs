using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using WindowsFormsApp1.hethong.sanpham;
using WindowsFormsApp1.hethong.USER.SanPham.ThongTinChiTiet;
using WindowsFormsApp1.ketnoi.dangnhap;

namespace WindowsFormsApp1.hethong.USER.SanPham.VotYonex
{
    public partial class VotYonexAD : Form
    {
        public VotYonexAD()
        {
            InitializeComponent();
            // Cài đặt placeholder text
            gunuHighligh_TimKiem.PlaceholderText = "Tìm kiếm sản phẩm...";
            // Thêm các tùy chọn giá vào ComboBox
            guna2ComboBox_TKgia.Items.Add("Dưới 1 triệu");
            guna2ComboBox_TKgia.Items.Add("Trên 1 triệu || Dưới 2 triệu");
            guna2ComboBox_TKgia.Items.Add("Trên 2 triệu || Dưới 5 triệu");
            guna2ComboBox_TKgia.Items.Add("Trên > 5 triệu");
        }
        private void LoadDanhSachSanPham()
        {
            DataTable dtSanPham = LayDanhSachSanPham();
            HienThiSanPham(dtSanPham);
        }
        // hàm lấy danh sách sản phẩm
        private DataTable LayDanhSachSanPham()
        {
            ketnoiDN kn = new ketnoiDN();
            string query = "SELECT * FROM [Vot] WHERE ThuongHieu = 'Yonex' ";
            DataSet ds = kn.Laydulieu(query);
            return ds?.Tables.Count > 0 ? ds.Tables[0] : null;
        }
        // hàm hiển thị sản phẩm
        private void HienThiSanPham(DataTable dtSanPham)
        {
            if (dtSanPham == null) return;

            flowLayoutPanel1.Controls.Clear();

            foreach (DataRow row in dtSanPham.Rows)
            {
                AddSanPhamControl(row);
            }
        }
        // hàm xử lý thêm sản phẩm lên panel
        private void AddSanPhamControl(DataRow row)
        {
            SanPhamControl spControl = new SanPhamControl
            {
                TenSanPham = row["TenSanPham"]?.ToString() ?? "Tên sản phẩm không có",
                GiaSanPham = decimal.TryParse(row["Gia"]?.ToString(), out decimal gia) ? gia : 0,
                HinhAnh = row["AnhSanPham"]?.ToString() ?? string.Empty,
                ProductID = row["SanPhamID"]?.ToString() ?? string.Empty
            };
            // Đăng ký sự kiện click để mở trang TTCTSanPhamUR với ProductID
            spControl.OnSanPhamClick += (s, e) =>
            {
                // Truyền ProductID sang trang TTCTSanPhamUR
                TTCTSanPhamUR tTsanphamFrm = new TTCTSanPhamUR(spControl.ProductID);
                tTsanphamFrm.ShowDialog(); // Mở trang chi tiết sản phẩm
            };
            flowLayoutPanel1.Controls.Add(spControl);
        }
        private void VotYonexUR_Load(object sender, EventArgs e)
        {
            LoadDanhSachSanPham(); // Sử dụng hàm LoadDanhSachSanPham khi form tải
        }
        private void gunuHighligh_TimKiem_TextChanged(object sender, EventArgs e)
        {
            TimKiemSanPham();
        }
        // hàm xử lý tìm kiếm
        private void TimKiemSanPham()
        {
            // Lấy danh sách sản phẩm từ cơ sở dữ liệu
            DataTable dtSanPham = LayDanhSachSanPham();
            if (dtSanPham != null)
            {
                string filterExpression = "";
                // Nếu ô nhập không trống, tạo điều kiện lọc
                if (!string.IsNullOrEmpty(gunuHighligh_TimKiem.Text))
                {
                    // Xóa placeholder text
                    gunuHighligh_TimKiem.PlaceholderText = "";
                    // Tạo điều kiện lọc dữ liệu
                    filterExpression = $"TenSanPham LIKE '%{gunuHighligh_TimKiem.Text}%'";
                }
                else
                {
                    // Hiện placeholder text nếu ô nhập trống
                    gunuHighligh_TimKiem.PlaceholderText = "Tìm kiếm sản phẩm...";
                }
                // Lọc dữ liệu sử dụng DataView
                DataView dv = new DataView(dtSanPham);
                dv.RowFilter = filterExpression;

                // Hiển thị kết quả tìm kiếm lên FlowLayoutPanel
                flowLayoutPanel1.Controls.Clear();
                if (dv.Count == 0)
                {
                    // Không có kết quả tìm kiếm, hiển thị dòng chữ thông báo
                    Label lblThongBao = new Label
                    {
                        Text = "Sản phẩm bạn cần tìm không có",
                        AutoSize = true, // Tự động điều chỉnh kích thước
                        Font = new Font("Segoe UI", 10, FontStyle.Bold), // Thiết lập font chữ và kích thước
                        ForeColor = Color.FromArgb(50, 149, 240), // Màu chữ
                        TextAlign = ContentAlignment.MiddleCenter // Căn giữa
                    };
                    // Thêm Label vào FlowLayoutPanel
                    flowLayoutPanel1.Controls.Add(lblThongBao);
                }
                else
                {
                    foreach (DataRowView row in dv)
                    {
                        SanPhamControl spControl = new SanPhamControl();
                        // Gán dữ liệu từ DataRow vào các thuộc tính của SanPhamControl
                        spControl.TenSanPham = row["TenSanPham"].ToString();
                        // Sử dụng TryParse để chuyển đổi giá sản phẩm
                        if (Decimal.TryParse(row["Gia"].ToString(), out decimal gia))
                        {
                            spControl.GiaSanPham = gia;
                        }
                        else
                        {
                            spControl.GiaSanPham = 0; // Hoặc giá mặc định khác
                        }
                        spControl.HinhAnh = row["AnhSanPham"].ToString();
                        // Lấy ProductID dưới dạng chuỗi
                        string productIdString = row["SanPhamID"].ToString();
                        if (!string.IsNullOrWhiteSpace(productIdString))
                        {
                            spControl.ProductID = productIdString; // Gán ProductID nếu hợp lệ
                        }
                        else
                        {
                            continue; // Bỏ qua sản phẩm nếu ProductID không hợp lệ
                        }
                        // Thêm SanPhamControl vào FlowLayoutPanel
                        flowLayoutPanel1.Controls.Add(spControl);
                    }
                }
            }
        }
        // combobox giá tìm kiếm theo giá
        private void guna2ComboBox_TKgia_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Kiểm tra nếu SelectedItem khác null
            if (guna2ComboBox_TKgia.SelectedItem == null)
            {
                // Không có lựa chọn nào, có thể thông báo cho người dùng
                return; // Thoát hàm nếu không có lựa chọn nào
            }
            // Tiếp tục xử lý nếu có mục được chọn
            DataTable dtSanPham = LayDanhSachSanPham();
            string filterExpression = "";
            switch (guna2ComboBox_TKgia.SelectedItem.ToString())
            {
                case "Dưới 1 triệu":
                    filterExpression = "Gia < 1000000";
                    break;
                case "Trên 1 triệu || Dưới 2 triệu":
                    filterExpression = "Gia >= 1000000 AND Gia < 2000000";
                    break;
                case "Trên 2 triệu || Dưới 5 triệu":
                    filterExpression = "Gia >= 2000000 AND Gia < 5000000";
                    break;
                case "Trên 5 triệu":
                    filterExpression = "Gia >= 5000000";
                    break;
                default:
                    break;
            }
            // Tiến hành lọc và hiển thị sản phẩm như trước đó
            DataView dv = new DataView(dtSanPham)
            {
                RowFilter = filterExpression
            };

            flowLayoutPanel1.Controls.Clear();

            if (dv.Count == 0)
            {
                Label lblThongBao = new Label
                {
                    Text = "Không Có Sản Phẩm Bạn Cần Tìm Trong Mức Giá Này ...",
                    AutoSize = true,
                    Font = new Font("Segoe UI", 10, FontStyle.Bold),
                    ForeColor = Color.FromArgb(50, 149, 240),
                    TextAlign = ContentAlignment.MiddleCenter
                };

                flowLayoutPanel1.Controls.Add(lblThongBao);
            }
            else
            {
                foreach (DataRowView row in dv)
                {
                    SanPhamControl spControl = new SanPhamControl
                    {
                        TenSanPham = row["TenSanPham"].ToString(),
                        GiaSanPham = Decimal.TryParse(row["Gia"].ToString(), out decimal gia) ? gia : 0,
                        HinhAnh = row["AnhSanPham"].ToString(),
                        ProductID = row["SanPhamID"].ToString()
                    };
                    flowLayoutPanel1.Controls.Add(spControl);
                }
            }
        } 
        // Tìm Kiếm
        private void gunuHighligh_TimKiem_KeyDown(object sender, KeyEventArgs e)
        {
            // Kiểm tra nếu phím Enter được nhấn
            if (e.KeyCode == Keys.Enter)
            {
                // Gọi phương thức tìm kiếm
                TimKiemSanPham();
                // Để tránh âm thanh "beep" khi nhấn Enter
                e.SuppressKeyPress = true;
            }
        }
        // Làm mới xử lý
        private void guna2Button_LamMoi_Click(object sender, EventArgs e)
        {
            // Đặt lại ô tìm kiếm
            gunuHighligh_TimKiem.Text = "";
            gunuHighligh_TimKiem.PlaceholderText = "Tìm kiếm sản phẩm...";
            // Đặt lại ComboBox giá
            guna2ComboBox_TKgia.SelectedIndex = -1; // Đặt lại lựa chọn của ComboBox
            // Tải lại danh sách sản phẩm
            LoadDanhSachSanPham();
        }
        private void sanPhamControl1_Click(object sender, EventArgs e)
        {        
        }
        private void sanPhamControl1_Load(object sender, EventArgs e)
        {
        }
    }
}

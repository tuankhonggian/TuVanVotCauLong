using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using WindowsFormsApp1.hethong.ITEAM;
using WindowsFormsApp1.ketnoi.dangnhap;

namespace WindowsFormsApp1.hethong.sanpham
{
    public partial class VotVictor : Form
    {
        public VotVictor()
        {
            InitializeComponent();
            // Cài đặt placeholder text
            gunuHighligh_TimKiem.PlaceholderText = "Tìm kiếm sản phẩm...";
            // Thêm các tùy chọn giá vào ComboBox
            guna2ComboBox_TKgia.Items.Add("< 1 triệu");
            guna2ComboBox_TKgia.Items.Add("1 triệu - 2 triệu");
            guna2ComboBox_TKgia.Items.Add("2 triệu - 5 triệu");
            guna2ComboBox_TKgia.Items.Add("> 5 triệu");
        }
        // Lấy danh sách sản phẩm
        private DataTable LayDanhSachSanPham()
        {
            ketnoiDN kn = new ketnoiDN();
            string query = "SELECT * FROM [TT.VotVictor]";
            DataSet ds = kn.Laydulieu(query);

            if (ds != null && ds.Tables.Count > 0)
            {
                return ds.Tables[0];
            }
            else
            {
                return null;
            }
        }
        // Hiển thị sản phẩm
        private void HienThiSanPham()
        {
            DataTable dtSanPham = LayDanhSachSanPham();

            if (dtSanPham != null)
            {
                flowLayoutPanel1.Controls.Clear();

                foreach (DataRow row in dtSanPham.Rows)
                {
                    SanPhamControl spControl = new SanPhamControl();

                    // Gán dữ liệu từ DataRow vào các thuộc tính của SanPhamControl
                    spControl.TenSanPham = row["TenSanPham"].ToString();
                    spControl.GiaSanPham = Convert.ToDecimal(row["Gia"]);
                    spControl.HinhAnh = row["AnhSanPham"].ToString();
                    spControl.ProductID = Convert.ToString(row["SanPhamID"]);

                    //Đăng ký sự kiện ProductSelected
                    spControl.OnSanPhamClick += SpControl_ProductSelected;

                    // Thêm SanPhamControl vào FlowLayoutPanel
                    flowLayoutPanel1.Controls.Add(spControl);
                }
            }
        }
        // xử lý khi click vào sản phẩm sẽ hiển thị trang chi tiết thông tin sản phẩm
        private void SpControl_ProductSelected(object sender, EventArgs e)
        {
            SanPhamControl selectedProduct = sender as SanPhamControl;
            if (selectedProduct != null)
            {
                // Chuyển đổi ProductID từ string sang int
                if (Int32.TryParse(selectedProduct.ProductID, out int productId))
                {
                    TTCTSanPham tTsanphamFrm = new TTCTSanPham(productId);
                    tTsanphamFrm.ShowDialog(); // Mở trang chi tiết sản phẩm
                }
                else
                {
                    MessageBox.Show("ID sản phẩm không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void VotVictor_Load(object sender, EventArgs e)
        {
            HienThiSanPham();
        }

        private void gunuHighligh_TimKiem_TextChanged(object sender, EventArgs e)
        {
            TimKiemSanPham();
        }
        // chức năng tìm kiếm sản phẩm
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
                    Label lblThongBao = new Label();
                    lblThongBao.Text = "Sản phẩm bạn cần tìm không có";
                    lblThongBao.AutoSize = true; // Tự động điều chỉnh kích thước
                    lblThongBao.Font = new Font("Segoe UI", 10, FontStyle.Bold); // Thiết lập font chữ và kích thước
                    lblThongBao.ForeColor = Color.FromArgb(50, 149, 240); // Màu chữ
                    lblThongBao.TextAlign = ContentAlignment.MiddleCenter; // Căn giữa

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
                        spControl.GiaSanPham = Convert.ToDecimal(row["Gia"]);
                        spControl.HinhAnh = row["AnhSanPham"].ToString();
                        spControl.ProductID = Convert.ToString(row["SanPhamID"]);

                        // Đăng ký sự kiện ProductSelected
                        spControl.OnSanPhamClick += SpControl_ProductSelected;

                        // Thêm SanPhamControl vào FlowLayoutPanel
                        flowLayoutPanel1.Controls.Add(spControl);
                    }
                }
            }
        }
        // tìm xác phẩm trong tầm giá
        private void guna2ComboBox_TKgia_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Lấy danh sách sản phẩm từ cơ sở dữ liệu
            DataTable dtSanPham = LayDanhSachSanPham();

            if (dtSanPham != null)
            {
                string filterExpression = "";

                // Xác định điều kiện lọc giá dựa trên lựa chọn của ComboBox
                switch (guna2ComboBox_TKgia.SelectedItem.ToString())
                {
                    case "< 1 triệu":
                        filterExpression = "Gia < 1000000";
                        break;
                    case "1 triệu - 2 triệu":
                        filterExpression = "Gia >= 1000000 AND Gia < 2000000";
                        break;
                    case "2 triệu - 5 triệu":
                        filterExpression = "Gia >= 2000000 AND Gia < 5000000";
                        break;
                    case "> 5 triệu":
                        filterExpression = "Gia >= 5000000";
                        break;
                    default:
                        break;
                }

                // Lọc dữ liệu sử dụng DataView
                DataView dv = new DataView(dtSanPham);

                // Thiết lập điều kiện lọc
                dv.RowFilter = filterExpression;

                // Hiển thị kết quả lọc lên FlowLayoutPanel
                flowLayoutPanel1.Controls.Clear();

                if (dv.Count == 0)
                {
                    // Không có kết quả tìm kiếm, hiển thị dòng chữ thông báo
                    Label lblThongBao = new Label();
                    lblThongBao.Text = "Không Có Sản Phẩm Bạn Cần Tìm Trong Mức Giá Này ...";
                    lblThongBao.AutoSize = true; // Tự động điều chỉnh kích thước
                    lblThongBao.Font = new Font("Segoe UI", 10, FontStyle.Bold); // Thiết lập font chữ và kích thước
                    lblThongBao.ForeColor = Color.FromArgb(50, 149, 240); // Màu chữ
                    lblThongBao.TextAlign = ContentAlignment.MiddleCenter; // Căn giữa

                    // Thêm Label vào FlowLayoutPanel
                    flowLayoutPanel1.Controls.Add(lblThongBao);
                }
                else
                {
                    foreach (DataRowView row in dv)
                    {
                        SanPhamControl spControl = new SanPhamControl();

                        // Kiểm tra và chuyển đổi giá trị cột Gia
                        decimal giaSanPham;
                        if (decimal.TryParse(row["Gia"].ToString(), out giaSanPham))
                        {
                            // Gán dữ liệu từ DataRow vào các thuộc tính của SanPhamControl
                            spControl.TenSanPham = row["TenSanPham"].ToString();
                            spControl.GiaSanPham = giaSanPham;
                            spControl.HinhAnh = row["AnhSanPham"].ToString();
                            spControl.ProductID = Convert.ToString(row["SanPhamID"]);

                            // Đăng ký sự kiện ProductSelected
                            spControl.OnSanPhamClick += SpControl_ProductSelected;

                            // Thêm SanPhamControl vào FlowLayoutPanel
                            flowLayoutPanel1.Controls.Add(spControl);
                        }
                        else
                        {
                            // Xử lý trường hợp giá trị không hợp lệ (có thể hiển thị thông báo lỗi hoặc bỏ qua)
                            MessageBox.Show("Giá sản phẩm không hợp lệ: " + row["Gia"].ToString());
                        }
                    }
                }
            }
        }

        private void guna2Button_Them_Click(object sender, EventArgs e)
        {
            // Tạo đối tượng của form ThemSP
            ThemSP themSPForm = new ThemSP();

            // Hiển thị form ThemSP
            themSPForm.ShowDialog(); // Hoặc themSPForm.Show() tùy thuộc vào cách bạn muốn hiển thị form (Modal hoặc không modal)
        }

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
    }
}

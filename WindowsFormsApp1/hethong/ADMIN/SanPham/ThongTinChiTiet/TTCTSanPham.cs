using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using WindowsFormsApp1.hethong.ITEAM;
using WindowsFormsApp1.ketnoi.dangnhap;

namespace WindowsFormsApp1.hethong.sanpham
{ 
    public partial class TTCTSanPham : Form
    {
        ketnoiDN kn = new ketnoiDN();

        private int ProductID;
        private bool isDragging = false;
        private Point startPoint = new Point(0, 0);
        public TTCTSanPham(int productId)
        {
            InitializeComponent();
            ProductID = productId;
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

        private void panel_Header_Paint(object sender, PaintEventArgs e)
        {

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
                label_TenSanPham.Text = row["TenSanPham"].ToString();
                label_ThuongHieu.Text = row["ThuongHieu"].ToString();
                label_LoaiVot.Text = row["LoaiVot"].ToString();
                label_ChatLieu.Text = row["ChatLieu"].ToString();
                label_TrongLuongVot.Text = row["TrongLuong"].ToString();
                label_ChuViCanVot.Text = row["ChuViCan"].ToString();
                label_SucCangVot.Text = row["SucCang"].ToString();
                label_ChieuDaiVot.Text = row["ChieuDai"].ToString();
                label_DiemCanBangVot.Text = row["DiemCanBang"].ToString();
                label_DoCungVot.Text = row["DoCung"].ToString();
                label_DangMatVot.Text = row["DangMatVot"].ToString();
                label_TrongLuongVungVot.Text = row["TrongLuongVung"].ToString();
                label_MauSac.Text = row["MauSac"].ToString();
                label_TroLuc.Text = row["TroLuc"].ToString();
                label_Gia.Text = $"{Convert.ToDecimal(row["Gia"])}";
                richTextBox_MoTa.Text = row["MoTa"].ToString();

                // Hiển thị hình ảnh
                string imagePath = row["AnhSanPham"].ToString();

                try
                {
                    if (File.Exists(imagePath))
                    {
                        pictureBox_AnhSanPham.ImageLocation = imagePath;
                        pictureBox_AnhSanPham.Load();
                    }
                    else
                    {
                        MessageBox.Show($"Không thể tìm thấy hình ảnh tại đường dẫn: {imagePath}");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Có lỗi xảy ra khi tải hình ảnh: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Không tìm thấy thông tin sản phẩm.");
            }
        }

        private void guna2Button_Back_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TTCTSanPham_Load(object sender, EventArgs e)
        {
            LoadProductDetails();
        }

        private void guna2Button_SuaThongTin_Click(object sender, EventArgs e)
        {
            // Tạo và hiển thị form TTsanphamFrm
            CapNhatThongTinSP editForm = new CapNhatThongTinSP(ProductID);

            // Ẩn form hiện tại trước khi hiển thị form mới
            this.Close();

            // Hiển thị form dưới dạng hộp thoại
            editForm.ShowDialog();

            // Sau khi đóng form mới, hiển thị lại form hiện tại (nếu cần)
            this.Show();

        }

        private void guna2Button_Xoa_Click(object sender, EventArgs e)
        {
            // Hiển thị hộp thoại xác nhận
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa sản phẩm này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                int productId = ProductID; // Giả định rằng bạn đã có ProductID từ đâu đó

                string truyvan = $"DELETE FROM [TT.VotYonex] WHERE SanPhamID = {productId}";

                ketnoiDN ketnoi = new ketnoiDN();
                bool ketQua = ketnoi.Thucthi(truyvan);

                if (ketQua)
                {
                    // Hiển thị thông báo thành công
                    iteam successToast = new iteam("Sản phẩm đã được xóa thành công.", "Success");
                    successToast.Show();
                    // Thoát trang thông tin sản phẩm (đóng form hiện tại)
                    this.Close();
                }
                else
                {
                    // Hiển thị thông báo lỗi
                    iteam errorToast = new iteam("Xóa sản phẩm thất bại. Vui lòng thử lại.", "Error");
                    errorToast.Show();
                }
            }
        }
    }
}

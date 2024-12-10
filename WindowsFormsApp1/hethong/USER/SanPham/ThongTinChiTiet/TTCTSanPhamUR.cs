using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using WindowsFormsApp1.ketnoi.dangnhap;

namespace WindowsFormsApp1.hethong.USER.SanPham.ThongTinChiTiet
{
    public partial class TTCTSanPhamUR : Form
    {
        ketnoiDN ketnoi = new ketnoiDN();
        private string productId;
        private bool isDragging = false;
        private Point startPoint = new Point(0, 0);
        public TTCTSanPhamUR(string productId)
        {
            InitializeComponent();

            this.productId = productId; // Lưu lại productId
            // Di chuyển header
            panel_Header.MouseDown += new MouseEventHandler(panel_Header_MouseDown);
            panel_Header.MouseMove += new MouseEventHandler(panel_Header_MouseMove);
            panel_Header.MouseUp += new MouseEventHandler(panel_Header_MouseUp);
        }

        private void LoadProductDetails()
        {
            
            string query = $"SELECT * FROM [TT.Vot] WHERE SanPhamID = '{productId}'"; // Thay đổi tên bảng và cột nếu cần
            DataSet ds = ketnoi.Laydulieu(query);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                // Giả sử bạn có các control như Label để hiển thị thông tin sản phẩm
                DataRow row = ds.Tables[0].Rows[0];
                // Hiển thị thông tin lên form
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
                label_Gia.Text = String.Format("{0:N0} VND", row["Gia"]);
                richTextBox_MoTa.Text = row["MoTa"].ToString(); // Hiển thị mô tả
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
        
        private void TTCTSanPhamUR_Load(object sender, EventArgs e)
        {
            LoadProductDetails(); // Gọi hàm để tải thông tin sản phẩm
        }

        private void guna2Button_Back_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

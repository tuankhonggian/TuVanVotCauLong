using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using WindowsFormsApp1.hethong.sanpham;
using WindowsFormsApp1.hethong.USER.SanPham.ThongTinChiTiet;
using WindowsFormsApp1.ketnoi.dangnhap;

namespace WindowsFormsApp1.hethong.FrmTuVan
{
    public partial class KetQua : Form
    {
        private ketnoiDN ketnoi = new ketnoiDN();
        private bool run = false;
        // Các biến lưu trữ kết quả
        private string mucdich;
        private string trinhdo;
        private string doCung;
        private string sucCang;
        private string trongLuong;
        private string chuViCan;
        private string chatLuong;
        private string chieuDai;
        private string trongLuongVung;
        private string dangMatVot;
        private string taiChinh;
        private string hang;
        private string ketqua;
        private string idketqua;
        private string plvot;
        private string plnguoi;
        private string NDplvot;
        private string NDplnguoi;

        private DateTime endTime;
        private DateTime startTime;
        private bool isDragging = false;
        private Point startPoint = new Point(0, 0);
        public KetQua()
        {
            InitializeComponent();
            // Di chuyển header
            panel_Header.MouseDown += new MouseEventHandler(panel_Header_MouseDown);
            panel_Header.MouseMove += new MouseEventHandler(panel_Header_MouseMove);
            panel_Header.MouseUp += new MouseEventHandler(panel_Header_MouseUp);
        }
        // Phương thức để nhận dữ liệu từ form tư vấn
        public void SetResults(string mucdich, string trinhdo, string doCung, string sucCang, string trongLuong, string chuViCan, string chatLuong, string chieuDai,
                               string trongLuongVung, string dangMatVot, string taiChinh, string hang, string ketqua, string idketqua, DateTime endTime, DateTime startTime,
                               string plvot, string plnguoi, string NDplvot, string NDplnguoi

                             )
        {
            // Gán các kết quả vào biến tương ứng
            this.mucdich = mucdich;
            this.trinhdo = trinhdo;
            this.doCung = doCung;
            this.sucCang = sucCang;
            this.trongLuong = trongLuong;
            this.chuViCan = chuViCan;
            this.chatLuong = chatLuong;
            this.chieuDai = chieuDai;
            this.trongLuongVung = trongLuongVung;
            this.dangMatVot = dangMatVot;
            this.taiChinh = taiChinh;
            this.hang = hang;
            this.ketqua = ketqua;
            this.idketqua = idketqua;
            this.endTime = endTime;
            this.startTime = startTime;
            this.plvot = plvot;
            this.plnguoi = plnguoi;
            this.NDplvot = NDplvot;
            this.NDplnguoi = NDplnguoi;
        }
        private string mtmucdich;
        private string mttrinhdo;
        private string mtdoCung;
        private string mtsucCang;
        private string mttrongLuong;
        private string mtchuViCan;
        private string mtchatLuong;
        private string mtchieuDai;
        private string mttrongLuongVung;
        private string mtdangMatVot;
        private string mttaiChinh;
        private string mthang;

        public void SetKetQua(string ketQua)
        {
        }
        private void label5_Click(object sender, EventArgs e)
        {
        }

        private void DisplayResults()
        {
            string query = $"SELECT NDmucdich FROM [MUCDICH] WHERE MucDichID = '{mucdich}'";
            DataTable dt = ketnoi.getTable(query);
            // Kiểm tra nếu kết quả trả về có dữ liệu
            if (dt.Rows.Count > 0)
            {
                // Lấy giá trị NDmucdich từ DataTable
                mtmucdich = dt.Rows[0]["NDmucdich"].ToString();

                // Cập nhật label với nội dung lấy được
                label_MucDich.Text = mtmucdich;
            }
            else
            {
                // Nếu không có kết quả, hiển thị thông báo lỗi hoặc giá trị mặc định
                label_MucDich.Text = "Không tìm thấy mục đích!";
            }
            // Lấy thông tin cho các trường khác (trinhdo, doCung, sucCang, ...)
            // Trình độ
            query = $"SELECT NDtrinhdo FROM [TRINHDO] WHERE TrinhDoID = '{trinhdo}'";
            dt = ketnoi.getTable(query);
            if (dt.Rows.Count > 0)
            {
                mttrinhdo = dt.Rows[0]["NDtrinhdo"].ToString();
                label_TrinhDo.Text = mttrinhdo;
            }
            else
            {
                label_TrinhDo.Text = "Không tìm thấy trình độ!";
            }

            // Độ cứng
            query = $"SELECT NDdocung FROM [DOCUNG] WHERE DoCungID = '{doCung}'";
            dt = ketnoi.getTable(query);
            if (dt.Rows.Count > 0)
            {
                mtdoCung = dt.Rows[0]["NDdocung"].ToString();
                label_DoCung.Text = mtdoCung;
            }
            else
            {
                label_DoCung.Text = "Không tìm thấy độ cứng!";
            }

            // Sức căng
            query = $"SELECT NDsuccang FROM [SUCCANG] WHERE SucCangID = '{sucCang}'";
            dt = ketnoi.getTable(query);
            if (dt.Rows.Count > 0)
            {
                mtsucCang = dt.Rows[0]["NDsuccang"].ToString();
                label_SucCang.Text = mtsucCang;
            }
            else
            {
                label_SucCang.Text = "Không tìm thấy sức căng!";
            }

            // Trọng lượng
            query = $"SELECT NDtrongluong FROM [TRONGLUONG] WHERE TrongLuongID = '{trongLuong}'";
            dt = ketnoi.getTable(query);
            if (dt.Rows.Count > 0)
            {
                mttrongLuong = dt.Rows[0]["NDtrongluong"].ToString();
                label_TrongLuong.Text = mttrongLuong;
            }
            else
            {
                label_TrongLuong.Text = "Không tìm thấy trọng lượng!";
            }

            // Chu vi cần
            query = $"SELECT NDchuvi FROM [CHUVI] WHERE ChuViID = '{chuViCan}'";
            dt = ketnoi.getTable(query);
            if (dt.Rows.Count > 0)
            {
                mtchuViCan = dt.Rows[0]["NDchuvi"].ToString();
                label_ChuVi.Text = mtchuViCan;
            }
            else
            {
                label_ChuVi.Text = "Không tìm thấy chu vi cần!";
            }

            // Chất lượng
            query = $"SELECT NDchatlieu FROM [CHATLIEU] WHERE ChatLieuID = '{chatLuong}'";
            dt = ketnoi.getTable(query);
            if (dt.Rows.Count > 0)
            {
                mtchatLuong = dt.Rows[0]["NDchatlieu"].ToString();
                label_CLDB.Text = mtchatLuong;
            }
            else
            {
                label_CLDB.Text = "Không tìm thấy chất lượng!";
            }

            // Chiều dài
            query = $"SELECT NDchieudai FROM [CHIEUDAI] WHERE ChieuDaiID = '{chieuDai}'";
            dt = ketnoi.getTable(query);
            if (dt.Rows.Count > 0)
            {
                mtchieuDai = dt.Rows[0]["NDchieudai"].ToString();
                label_ChieuDai.Text = mtchieuDai;
            }
            else
            {
                label_ChieuDai.Text = "Không tìm thấy chiều dài!";
            }

            // Trọng lượng vung
            query = $"SELECT NDtrongluongvung FROM [TRONGLUONGVUNG] WHERE TrongLuongVungID = '{trongLuongVung}'";
            dt = ketnoi.getTable(query);
            if (dt.Rows.Count > 0)
            {
                mttrongLuongVung = dt.Rows[0]["NDtrongluongvung"].ToString();
                labe_TLVung.Text = mttrongLuongVung;
            }
            else
            {
                labe_TLVung.Text = "Không tìm thấy trọng lượng vung!";
            }

            // Dạng mặt vợt
            query = $"SELECT NDmatvot FROM [MATVOT] WHERE MatVotID = '{dangMatVot}'";
            dt = ketnoi.getTable(query);
            if (dt.Rows.Count > 0)
            {
                mtdangMatVot = dt.Rows[0]["NDmatvot"].ToString();
                label_MatVot.Text = mtdangMatVot;
            }
            else
            {
                label_MatVot.Text = "Không tìm thấy dạng mặt vợt!";
            }

            // Tài chính
            query = $"SELECT NDtaichinh FROM [TAICHINH] WHERE TaiChinhID = '{taiChinh}'";
            dt = ketnoi.getTable(query);
            if (dt.Rows.Count > 0)
            {
                mttaiChinh = dt.Rows[0]["NDtaichinh"].ToString();
                label_TaiChinh.Text = mttaiChinh;
            }
            else
            {
                label_TaiChinh.Text = "Không tìm thấy tài chính!";
            }

            // Hãng
            query = $"SELECT NDhangvot FROM [HANGVOT] WHERE HangVotID = '{hang}'";
            dt = ketnoi.getTable(query);
            if (dt.Rows.Count > 0)
            {
                mthang = dt.Rows[0]["NDhangvot"].ToString();
                label_HangVot.Text = mthang;
            }
            else
            {
                label_HangVot.Text = "Không tìm thấy hãng!";
            }
            // Phân Loại Vợt
            label_PhanLoaiVot.Text = NDplvot;
            // Phân Loại Người
            label_PhanLoaiNguoi.Text = NDplnguoi;
            // Tách danh sách SanPhamID từ idketqua
            string[] productIds = idketqua.Split(); // Sử dụng khoảng trắng làm dấu phân cách

            // Sau khi tách, truyền mảng ID sản phẩm vào hàm để tải thông tin sản phẩm
            LoadProductInfo(productIds);
        }

        private void LoadProductInfo(string[] productIds)
        {
            foreach (var productId in productIds)
            {
                // Truy vấn dữ liệu sản phẩm theo ID
                string query = $"SELECT * FROM Vot WHERE SanPhamID = '{productId}'";
                DataTable dt = ketnoi.getTable(query);

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        // Tạo instance mới của SanPhamControl
                        SanPhamControl spControl = new SanPhamControl
                        {
                            TenSanPham = row["TenSanPham"]?.ToString() ?? "Tên sản phẩm không có",
                            GiaSanPham = decimal.TryParse(row["Gia"]?.ToString(), out decimal gia) ? gia : 0,
                            HinhAnh = row["AnhSanPham"]?.ToString() ?? string.Empty,
                            ProductID = row["SanPhamID"]?.ToString() ?? string.Empty
                        };

                        // Thêm sự kiện ProductSelected
                        spControl.OnSanPhamClick += ProductControl_ProductSelected;

                        // Thêm vào FlowLayoutPanel
                        flowLayoutPanel_KetQua.Controls.Add(spControl);
                    }
                }
            }
        }
        private void ProductControl_ProductSelected(object sender, EventArgs e)
        {
            var selectedProduct = sender as SanPhamControl;
            if (selectedProduct != null)
            {
                // Tạo biến productId và gán giá trị từ selectedProduct.ProductID
                string productId = selectedProduct.ProductID;

                // Mở trang chi tiết sản phẩm và truyền productId của sản phẩm
                TTCTSanPhamUR detailForm = new TTCTSanPhamUR(productId);

                // Hiển thị form TTCTSanPham
                detailForm.ShowDialog();
            }
        }


        private void KetQua_Load(object sender, EventArgs e)
        {
            DisplayResults(); // Hiển thị kết quả nếu cần

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

        private void guna2Button_Back_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Save_Click(object sender, EventArgs e)
        {
            // Tạo câu lệnh SQL để lưu kết quả vào bảng
            string query = $"INSERT INTO [dbo].[KetQua] (MucDich, TrinhDo, LucCoTay, PhongCachChoi, DoCung, SucCang, TrongLuong, ChuVi, ChatLuong, ChieuDai, TrongLuongVung, MatVot, TaiChinh, HangVot, Vot, ThoiGianBatDau, ThoiGianKetThuc) VALUES ( N'{mtmucdich}', N'{mttrinhdo}', N'{mtdoCung}', N'{mtsucCang}', N'{mttrongLuong}', N'{mtchuViCan}', N'{mtchatLuong}', N'{mtchieuDai}', N'{mttrongLuongVung}', N'{mtdangMatVot}', N'{mttaiChinh}', N'{mthang}', N'{ketqua}', N'{startTime}', N'{endTime}')";
            // Tạo đối tượng kết nối và thực thi câu lệnh
            ketnoiDN ketnoi = new ketnoiDN();

            // Gọi phương thức Thucthi để thực thi câu lệnh SQL
            bool isSuccess = ketnoi.Thucthi(query);

            // Kiểm tra kết quả thực thi
            if (isSuccess)
            {
                MessageBox.Show("Lưu kết quả thành công!");
            }
            else
            {
                MessageBox.Show("Lưu kết quả thất bại!");
            }
        }
    }
}

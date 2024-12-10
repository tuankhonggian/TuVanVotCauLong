using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using WindowsFormsApp1.ketnoi.dangnhap;

namespace WindowsFormsApp1.hethong.ADMIN.TuVan
{
    public partial class ThemLuat : Form
    {
        ketnoiDN kn = new ketnoiDN();
        // Biến để lưu trữ sự kiện hiện tại trong DataGridView
        private string currentEvent = string.Empty;

        private bool isDragging = false;
        private Point startPoint = new Point(0, 0);
        public ThemLuat()
        {
            InitializeComponent();   
            pictureBox2.Visible = false;
            guna2Button_TaoLuat2.Visible = false;
            guna2Button_TaoLuat3.Visible = false;
            label_Luat1.Visible = false;
            label_Luat2.Visible = false;
            label_Luat3.Visible = false;

            // Di chuyển header
            panel_Header.MouseDown += new MouseEventHandler(panel_Header_MouseDown);
            panel_Header.MouseMove += new MouseEventHandler(panel_Header_MouseMove);
            panel_Header.MouseUp += new MouseEventHandler(panel_Header_MouseUp);
        }
        private void InitializeComboBoxes()
        {
            DoCung.Tag = "Độ Cứng";
            ChieuDai.Tag = "Chiều Dài";
            SucCang.Tag = "Sức Căng";
            TrongLuongVung.Tag = "Trọng Lượng Vung";
            TrongLuong.Tag = "Trọng Lượng";
            DangMatVot.Tag = "Dạng Mặt Vợt";
            ChuVi.Tag = "Chu Vi";
            ChatLieu.Tag = "Chất Liệu";
            NhomLoaiVot.Tag = "Nhóm Loại Vợt";
            MucDich.Tag = "Mục Đích";
            TrinhDo.Tag = "Trinh Độ";
            NhomLoaiNguoi.Tag = "Nhóm Loại Người";
            TaiChinh.Tag = "Tài Chính";
            HangVot.Tag = "Thương Hiêu";
            SanPham.Tag = "Vợt";
        }//Xử Lý Sự Kiện Trên DTGR
        private void UpdateDataGridView(string selectedEvent, Guna2ComboBox comboBox)
        {

            // Kiểm tra xem Tag của ComboBox có giá trị hợp lệ không
            if (comboBox.Tag == null)
            {
                MessageBox.Show("Tag của ComboBox chưa được gán giá trị!");
                return;
            }

            // Xóa các dòng liên quan đến ComboBox đang thay đổi
            foreach (DataGridViewRow row in dataGridView_DieuKienThem1.Rows)
            {
                // Kiểm tra xem dòng có chứa sự kiện từ ComboBox đang thay đổi không
                if (row.Cells[0].Value.ToString().StartsWith(comboBox.Tag.ToString() + ":"))
                {
                    dataGridView_DieuKienThem1.Rows.Remove(row);
                }
            }

            // Thêm sự kiện mới vào DataGridView với định dạng ComboBox: Sự kiện
            if (!string.IsNullOrEmpty(selectedEvent))
            {
                dataGridView_DieuKienThem1.Rows.Add(comboBox.Tag.ToString() + ": " + selectedEvent);
            }
        }
        
        private void LoadComboBoxData(string query, ComboBox comboBox)
        {
            DataSet ds = kn.Laydulieu(query);

            if (ds != null && ds.Tables.Count > 0)
            {
                comboBox.Items.Clear();
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    comboBox.Items.Add(row[1].ToString());
                }
            }

        }
        private void LoadComboBoxData()
        {
            // Cập nhật dữ liệu cho ComboBox
            LoadComboBoxData("SELECT DoCungID, NDdocung FROM [DOCUNG]", DoCung);
            LoadComboBoxData("SELECT ChieuDaiID, NDchieudai FROM [CHIEUDAI]", ChieuDai);
            LoadComboBoxData("SELECT SucCangID, NDsuccang FROM [SUCCANG]", SucCang);
            LoadComboBoxData("SELECT TrongLuongVungID, NDtrongluongvung FROM [TRONGLUONGVUNG]", TrongLuongVung);
            LoadComboBoxData("SELECT TrongLuongID, NDtrongluong FROM [TRONGLUONG]", TrongLuong);
            LoadComboBoxData("SELECT MatVotID, NDmatvot FROM [MATVOT]", DangMatVot);
            LoadComboBoxData("SELECT ChuViID, NDchuvi FROM [CHUVI]", ChuVi);
            LoadComboBoxData("SELECT PhanLoaiVotID, NDphanloaivot FROM [PHANLOAIVOT]", NhomLoaiVot);
            LoadComboBoxData("SELECT MucDichID, NDmucdich FROM [MUCDICH]", MucDich);
            LoadComboBoxData("SELECT ChatLieuID, NDchatlieu FROM [CHATLIEU]", ChatLieu);
            LoadComboBoxData("SELECT TrinhDoID, NDtrinhdo FROM [TRINHDO]", TrinhDo);
            LoadComboBoxData("SELECT PhanLoaiNguoiID, NDphanloainguoi FROM [PHANLOAINGUOI]", NhomLoaiNguoi);
            LoadComboBoxData("SELECT TaiChinhID, NDtaichinh FROM [TAICHINH]", TaiChinh);
            LoadComboBoxData("SELECT HangVotID, NDhangvot FROM [HANGVOT]", HangVot);
            LoadComboBoxData("SELECT SanPhamID, TenSanPham FROM [VOT]", SanPham);
        }

        private void ThemLuat_Load(object sender, EventArgs e)
        {
            LoadComboBoxData();
            // Gọi hàm InitializeComboBoxes để đảm bảo Tag đã được gán giá trị
            InitializeComboBoxes();
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

        private void guna2Button_Back_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        

        private void DoCung_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DoCung.SelectedItem != null)
            {
                string docung = DoCung.SelectedItem.ToString();
                UpdateDataGridView(docung, DoCung);

                // Tạo câu lệnh SQL để truy vấn DoCungID từ bảng [DOCUNG]
                string query = $"SELECT DoCungID FROM [DOCUNG] WHERE NDdocung = N'{docung}'";

                // Sử dụng phương thức truy vấn để lấy kết quả từ cơ sở dữ liệu
                DataSet ds = kn.Laydulieu(query);

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    // Lấy DoCungID từ kết quả truy vấn
                    string docungID = ds.Tables[0].Rows[0]["DoCungID"].ToString();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy DoCungID cho giá trị đã chọn.");
                }
            }
        }

        private void ChieuDai_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ChieuDai.SelectedItem != null)
            {
                string docung = ChieuDai.SelectedItem.ToString();
                UpdateDataGridView(docung, ChieuDai);

                // Tạo câu lệnh SQL để truy vấn DoCungID từ bảng [DOCUNG]
                string query = $"SELECT ChieuDaiID FROM [CHIEUDAI] WHERE NDchieudai = N'{docung}'";

                // Sử dụng phương thức truy vấn để lấy kết quả từ cơ sở dữ liệu
                DataSet ds = kn.Laydulieu(query);

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    // Lấy DoCungID từ kết quả truy vấn
                    string docungID = ds.Tables[0].Rows[0]["ChieuDaiID"].ToString();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy DoCungID cho giá trị đã chọn.");
                }
            }
        }

        private void TrongLuongVung_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TrongLuongVung.SelectedItem != null)
            {
                string docung = TrongLuongVung.SelectedItem.ToString();
                UpdateDataGridView(docung, TrongLuongVung);

                // Tạo câu lệnh SQL để truy vấn DoCungID từ bảng [DOCUNG]
                string query = $"SELECT TrongLuongVungID FROM [TRONGLUONGVUNG] WHERE NDtrongluongvung = N'{docung}'";

                // Sử dụng phương thức truy vấn để lấy kết quả từ cơ sở dữ liệu
                DataSet ds = kn.Laydulieu(query);

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    // Lấy DoCungID từ kết quả truy vấn
                    string docungID = ds.Tables[0].Rows[0]["TrongLuongVungID"].ToString();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy DoCungID cho giá trị đã chọn.");
                }
            }
        }

        private void SucCang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SucCang.SelectedItem != null)
            {
                string selectedEvent = SucCang.SelectedItem.ToString();
                UpdateDataGridView(selectedEvent, SucCang);
            }
        }

        private void TrongLuong_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TrongLuong.SelectedItem != null)
            {
                string docung = TrongLuong.SelectedItem.ToString();
                UpdateDataGridView(docung, TrongLuong);

                // Tạo câu lệnh SQL để truy vấn DoCungID từ bảng [DOCUNG]
                string query = $"SELECT TrongLuongID FROM [TRONGLUONG] WHERE NDtrongluong = N'{docung}'";

                // Sử dụng phương thức truy vấn để lấy kết quả từ cơ sở dữ liệu
                DataSet ds = kn.Laydulieu(query);

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    // Lấy DoCungID từ kết quả truy vấn
                    string docungID = ds.Tables[0].Rows[0]["TrongLuongID"].ToString();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy DoCungID cho giá trị đã chọn.");
                }
            }
        }

        private void DangMatVot_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DangMatVot.SelectedItem != null)
            {
                string docung = DangMatVot.SelectedItem.ToString();
                UpdateDataGridView(docung, DangMatVot);

                // Tạo câu lệnh SQL để truy vấn DoCungID từ bảng [DOCUNG]
                string query = $"SELECT MatVotID FROM [MATVOT] WHERE NDmatvot = N'{docung}'";

                // Sử dụng phương thức truy vấn để lấy kết quả từ cơ sở dữ liệu
                DataSet ds = kn.Laydulieu(query);

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    // Lấy DoCungID từ kết quả truy vấn
                    string docungID = ds.Tables[0].Rows[0]["MatVotID"].ToString();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy DoCungID cho giá trị đã chọn.");
                }
            }
        }

        private void ChatLieu_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ChatLieu.SelectedItem != null)
            {
                string docung = ChatLieu.SelectedItem.ToString();
                UpdateDataGridView(docung, ChatLieu);

                // Tạo câu lệnh SQL để truy vấn DoCungID từ bảng [DOCUNG]
                string query = $"SELECT ChatLieuID FROM [CHATLIEU] WHERE NDchatlieu = N'{docung}'";

                // Sử dụng phương thức truy vấn để lấy kết quả từ cơ sở dữ liệu
                DataSet ds = kn.Laydulieu(query);

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    // Lấy DoCungID từ kết quả truy vấn
                    string docungID = ds.Tables[0].Rows[0]["ChatLieuID"].ToString();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy DoCungID cho giá trị đã chọn.");
                }
            }
        }

        private void ChuVi_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ChuVi.SelectedItem != null)
            {
                string docung = ChuVi.SelectedItem.ToString();
                UpdateDataGridView(docung, ChuVi);

                // Tạo câu lệnh SQL để truy vấn DoCungID từ bảng [DOCUNG]
                string query = $"SELECT ChuViID FROM [CHUVI] WHERE NDchuvi = N'{docung}'";

                // Sử dụng phương thức truy vấn để lấy kết quả từ cơ sở dữ liệu
                DataSet ds = kn.Laydulieu(query);

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    // Lấy DoCungID từ kết quả truy vấn
                    string docungID = ds.Tables[0].Rows[0]["ChuViID"].ToString();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy DoCungID cho giá trị đã chọn.");
                }
            }
        }

        private void NhomLoaiVot_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (NhomLoaiVot.SelectedItem != null)
            {
                string nhomloaivot = NhomLoaiVot.SelectedItem.ToString();
                UpdateDataGridView(nhomloaivot, NhomLoaiVot);

                // Tạo câu lệnh SQL để truy vấn DoCungID từ bảng [DOCUNG]
                string query = $"SELECT PhanLoaiVotID FROM [PHANLOAIVOT] WHERE NDphanloaivot = N'{nhomloaivot}'";

                // Sử dụng phương thức truy vấn để lấy kết quả từ cơ sở dữ liệu
                DataSet ds = kn.Laydulieu(query);

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    // Lấy DoCungID từ kết quả truy vấn
                    string docungID = ds.Tables[0].Rows[0]["PhanLoaiVotID"].ToString();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy DoCungID cho giá trị đã chọn.");
                }
            }
        }

        private void MucDich_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (MucDich.SelectedItem != null)
            {
                string selectedEvent = MucDich.SelectedItem.ToString();
                UpdateDataGridView(selectedEvent, MucDich);
            }
        }

        private void TrinhDo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TrinhDo.SelectedItem != null)
            {
                string selectedEvent = TrinhDo.SelectedItem.ToString();
                UpdateDataGridView(selectedEvent, TrinhDo);
            }
        }

        private void NhomLoaiNguoi_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (NhomLoaiNguoi.SelectedItem != null)
            {
                string selectedEvent = NhomLoaiNguoi.SelectedItem.ToString();
                UpdateDataGridView(selectedEvent, NhomLoaiNguoi);
            }
        }

        private void TaiChinh_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TaiChinh.SelectedItem != null)
            {
                string selectedEvent = TaiChinh.SelectedItem.ToString();
                UpdateDataGridView(selectedEvent, TaiChinh);
            }
        }

        private void HangVot_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (HangVot.SelectedItem != null)
            {
                string selectedEvent = HangVot.SelectedItem.ToString();
                UpdateDataGridView(selectedEvent, HangVot);
                // Lấy danh sách sản phẩm từ hãng vợt đã chọn
                UpdateSanPhamList(selectedEvent);
            }
        }
        private void UpdateSanPhamList(string hangVot)
        {
            // Giả sử bạn có hàm GetSanPhamByHangVot để lấy danh sách sản phẩm từ cơ sở dữ liệu
            string query = $"SELECT TenSanPham FROM [VOT] WHERE ThuongHieu = N'{hangVot}'";
            DataSet ds = kn.Laydulieu(query);

            // Xóa hết các sản phẩm cũ trong ComboBox SanPham
            SanPham.Items.Clear();

            // Nếu có dữ liệu trả về, thêm các sản phẩm vào ComboBox
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    SanPham.Items.Add(row["TenSanPham"].ToString());
                }
            }
            else
            {
                MessageBox.Show($"Không có sản phẩm nào thuộc hãng '{hangVot}'");
            }
        }
        private void SanPham_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (HangVot.SelectedItem == null)
            {
                // Nếu chưa chọn hãng vợt, yêu cầu người dùng chọn hãng vợt trước
                MessageBox.Show("Vui lòng chọn hãng vợt trước khi chọn sản phẩm.");
                // Làm mới lại ComboBox SanPham (có thể làm mới bằng cách xóa và thêm lại danh sách sản phẩm)
                SanPham.Items.Clear();
                return;  // Dừng lại nếu chưa chọn hãng vợt
            }

            if (SanPham.SelectedItem != null)
            {
                string selectedProduct = SanPham.SelectedItem.ToString();

                // Cập nhật lại DataGridView với sản phẩm đã chọn
                UpdateDataGridView(selectedProduct, SanPham);

                // Lấy đường dẫn ảnh của sản phẩm đã chọn
                string productImagePath = GetProductImagePath(selectedProduct);

                // Hiển thị ảnh sản phẩm trong PictureBox
                if (!string.IsNullOrEmpty(productImagePath))
                {
                    pictureBox_AnhSanPham.Image = Image.FromFile(productImagePath);
                }
                else
                {
                    MessageBox.Show($"Không tìm thấy ảnh cho sản phẩm '{selectedProduct}'.");
                }
            }
        }
        private string GetProductImagePath(string productName)
        {
            string imagePath = string.Empty;  // Khai báo biến imagePath

            // Tạo câu truy vấn SQL để lấy đường dẫn ảnh từ tên sản phẩm
            string query = $"SELECT AnhSanPham FROM [VOT] WHERE TenSanPham = N'{productName}'";

            // Lấy dữ liệu từ cơ sở dữ liệu
            DataSet ds = kn.Laydulieu(query);

            // Kiểm tra nếu có kết quả trả về từ truy vấn
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                // Lấy đường dẫn ảnh từ kết quả truy vấn
                imagePath = ds.Tables[0].Rows[0]["AnhSanPham"].ToString();
            }
            else
            {
                MessageBox.Show($"Không tìm thấy ảnh cho sản phẩm '{productName}' trong cơ sở dữ liệu.");
            }

            return imagePath;
        }


        // Phương thức hỗ trợ lấy ID từ cơ sở dữ liệu
        private string GetIDFromDatabase(string table, string column, string value)
        {
            if (string.IsNullOrEmpty(value)) return string.Empty;

            // Tạo câu lệnh SQL để truy vấn ID từ bảng
            string query = $"SELECT {table}ID FROM [{table}] WHERE {column} = N'{value}'";

            // Sử dụng phương thức truy vấn để lấy kết quả từ cơ sở dữ liệu
            DataSet ds = kn.Laydulieu(query);

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                // Lấy ID từ kết quả truy vấn
                return ds.Tables[0].Rows[0][table + "ID"].ToString();
            }

            return string.Empty;
        }

        private string GetVotID(string vot)
        {
            string votID = string.Empty;  // Khai báo biến votID

            // Tạo câu truy vấn SQL để lấy SanPhamID từ tên sản phẩm
            string query = $"SELECT SanPhamID FROM [VOT] WHERE TenSanPham = N'{vot}'";

            // Lấy dữ liệu từ cơ sở dữ liệu
            DataSet ds = kn.Laydulieu(query);

            // Kiểm tra nếu có kết quả trả về từ truy vấn
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                votID = ds.Tables[0].Rows[0]["SanPhamID"].ToString();
            }
            else
            {
                MessageBox.Show($"Không tìm thấy sản phẩm '{vot}' trong cơ sở dữ liệu.");
            }

            return votID;
        }
        private string GetMaxLuatID()
        {
            // Tạo câu lệnh SQL để lấy ID lớn nhất từ LuatID có dạng 'R%'
            string query = "SELECT MAX(CAST(SUBSTRING(LuatID, 2, LEN(LuatID)) AS INT)) AS MaxId " +
                           "FROM [TapLuat] " +
                           "WHERE LuatID LIKE 'R%'";

            // Sử dụng phương thức truy vấn để lấy kết quả từ cơ sở dữ liệu
            DataSet ds = kn.Laydulieu(query);

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                // Lấy giá trị MaxId từ câu truy vấn
                int maxLuatID = Convert.ToInt32(ds.Tables[0].Rows[0]["MaxId"]);

                // Tạo ID mới bằng cách tăng giá trị MaxId lên 1 và thêm tiền tố 'R'
                string nextLuatID = "R" + (maxLuatID + 1).ToString("D3");

                return nextLuatID;
            }
            else
            {
                // Trường hợp không tìm thấy giá trị nào, trả về 'R001'
                return "R001";
            }
        }
        // Phương thức kiểm tra xem nội dung luật đã tồn tại chưa
        private bool IsLuatExist(string result)
        {
            // Tạo câu truy vấn SQL để kiểm tra sự tồn tại của NoiDung
            string query = "SELECT COUNT(*) FROM [TapLuat] WHERE NoiDung = '" + result.Replace("'", "''") + "'";

            // Sử dụng phương thức truy vấn để lấy kết quả
            DataSet ds = kn.Laydulieu(query);

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                int count = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
                return count > 0; // Nếu count > 0, tức là đã tồn tại
            }
            return false; // Nếu không tìm thấy, trả về false
        }

        private void guna2Button_TaoLuat1_Click(object sender, EventArgs e)
        {
            // Khởi tạo chuỗi kết quả
            string result = "";

            // Lấy các giá trị đã chọn từ các ComboBox và tạo câu truy vấn để lấy ID
            string docung = DoCung.SelectedItem?.ToString();
            string sucCang = SucCang.SelectedItem?.ToString();
            string trongLuongVung = TrongLuongVung.SelectedItem?.ToString();
            string chuVi = ChuVi.SelectedItem?.ToString();
            string chatLieu = ChatLieu.SelectedItem?.ToString();
            string chieuDai = ChieuDai.SelectedItem?.ToString();
            string trongLuong = TrongLuong.SelectedItem?.ToString();
            string dangMatVot = DangMatVot.SelectedItem?.ToString();
            string nhomLoaiVot = NhomLoaiVot.SelectedItem?.ToString();

            // Tạo câu truy vấn SQL để lấy các ID tương ứng cho các lựa chọn
            string docungID = GetIDFromDatabase("DoCung", "NDdocung", docung);
            string sucCangID = GetIDFromDatabase("SucCang", "NDsuccang", sucCang);
            string trongLuongVungID = GetIDFromDatabase("TrongLuongVung", "NDtrongluongvung", trongLuongVung);
            string chuViID = GetIDFromDatabase("ChuVi", "NDchuvi", chuVi);
            string chatLieuID = GetIDFromDatabase("ChatLieu", "NDchatlieu", chatLieu);
            string chieuDaiID = GetIDFromDatabase("ChieuDai", "NDchieudai", chieuDai);
            string trongLuongID = GetIDFromDatabase("TrongLuong", "NDtrongluong", trongLuong);
            string dangMatVotID = GetIDFromDatabase("MatVot", "NDmatvot", dangMatVot);
            string nhomLoaiVotID = GetIDFromDatabase("PhanLoaiVot", "NDphanloaivot", nhomLoaiVot);

            // Kiểm tra tất cả các ID đã lấy
            if (!string.IsNullOrEmpty(docungID) && !string.IsNullOrEmpty(sucCangID) &&
                !string.IsNullOrEmpty(trongLuongVungID) && !string.IsNullOrEmpty(chuViID) &&
                !string.IsNullOrEmpty(chatLieuID) && !string.IsNullOrEmpty(chieuDaiID) &&
                !string.IsNullOrEmpty(trongLuongID) && !string.IsNullOrEmpty(dangMatVotID) &&
                !string.IsNullOrEmpty(nhomLoaiVotID))
            {
                // Kết hợp thành chuỗi theo định dạng yêu cầu
                result = $"{docungID}^{sucCangID}^{trongLuongID}^{chuViID}^{chatLieuID}^{chieuDaiID}^{trongLuongVungID}^{dangMatVotID}>{nhomLoaiVotID}";
                // Kiểm tra nếu nội dung đã tồn tại trong cơ sở dữ liệu
                if (IsLuatExist(result))
                {
                    MessageBox.Show("Nội dung luật này đã tồn tại trong cơ sở dữ liệu.");
                    return; // Không tiếp tục thêm mới nếu đã tồn tại
                }


                label_Luat1.Text = result;
                label_Luat1.Visible = true;
                guna2Button_TaoLuat2.Visible = true;
                pictureBox2.Visible = true;

                // Lấy ID cho luật mới
                string luatID = GetMaxLuatID();

                // Tạo câu truy vấn SQL để thêm luật vào bảng TapLuat
                string query = "INSERT INTO [TapLuat] (LuatID, NoiDung) VALUES ('" + luatID + "', '" + result + "')";

                // Thực thi câu lệnh SQL để thêm luật
                bool success = kn.Thucthi(query);

                if (success)
                {
                    MessageBox.Show("Thêm luật thành công.");
                }
                else
                {
                    MessageBox.Show("Có lỗi xảy ra khi thêm luật.");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn tất cả các giá trị.");
            }
        }
        private void guna2Button_TaoLuat2_Click(object sender, EventArgs e)
        {
            // Khởi tạo chuỗi kết quả
            string result = "";

            // Lấy các giá trị đã chọn từ các ComboBox và tạo câu truy vấn để lấy ID
            string mucdich = MucDich.SelectedItem?.ToString();
            string trinhdo = TrinhDo.SelectedItem?.ToString();
            string nhomLoaiNguoi = NhomLoaiNguoi.SelectedItem?.ToString();

            // Tạo câu truy vấn SQL để lấy các ID tương ứng cho các lựa chọn
            string mucdichID = GetIDFromDatabase("MucDich", "NDmucdich", mucdich);
            string trinhdoID = GetIDFromDatabase("TrinhDo", "NDtrinhdo", trinhdo);
            string nhomLoaiNguoiID = GetIDFromDatabase("PhanLoaiNguoi", "NDphanloainguoi", nhomLoaiNguoi);

            // Kiểm tra tất cả các ID đã lấy
            if (!string.IsNullOrEmpty(mucdichID) && !string.IsNullOrEmpty(trinhdoID) &&
                !string.IsNullOrEmpty(nhomLoaiNguoiID))
            {
                // Kết hợp thành chuỗi theo định dạng yêu cầu
                result = $"{mucdichID}^{trinhdoID}>{nhomLoaiNguoiID}";
                // Kiểm tra nếu nội dung đã tồn tại trong cơ sở dữ liệu
                if (IsLuatExist(result))
                {
                    MessageBox.Show("Nội dung luật này đã tồn tại trong cơ sở dữ liệu.");
                    return; // Không tiếp tục thêm mới nếu đã tồn tại
                }
                label_Luat2.Text = result;
                label_Luat2.Visible = true;
                guna2Button_TaoLuat3.Visible = true;
                // Lấy ID cho luật mới
                string luatID = GetMaxLuatID();

                // Tạo câu truy vấn SQL để thêm luật vào bảng TapLuat
                string query = "INSERT INTO [TapLuat] (LuatID, NoiDung) VALUES ('" + luatID + "', '" + result + "')";

                // Thực thi câu lệnh SQL để thêm luật
                bool success = kn.Thucthi(query);

                if (success)
                {
                    MessageBox.Show("Thêm luật thành công.");
                }
                else
                {
                    MessageBox.Show("Có lỗi xảy ra khi thêm luật.");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn tất cả các giá trị.");
            }
        }
        

        private void guna2Button_TaoLuat3_Click(object sender, EventArgs e)
        {

            // Khởi tạo chuỗi kết quả
            string result = "";

            // Lấy các giá trị đã chọn từ các ComboBox và tạo câu truy vấn để lấy ID
            string taichinh = TaiChinh.SelectedItem?.ToString();
            string hangvot = HangVot.SelectedItem?.ToString();
            string vot = SanPham.SelectedItem?.ToString();

            // Kiểm tra nếu bất kỳ ComboBox nào không được chọn
            if (string.IsNullOrEmpty(taichinh) || string.IsNullOrEmpty(hangvot) || string.IsNullOrEmpty(vot))
            {
                MessageBox.Show("Vui lòng chọn tất cả các giá trị.");
                return; // Nếu không chọn đủ giá trị, thoát ra ngay
            }

            // Tạo câu truy vấn SQL để lấy các ID tương ứng cho các lựa chọn
            string taichinhID = GetIDFromDatabase("TaiChinh", "NDtaichinh", taichinh);
            string hangvotID = GetIDFromDatabase("HangVot", "NDhangvot", hangvot);

            // Lấy votID bằng cách gọi hàm GetVotID
            string votID = GetVotID(vot);

            // Kiểm tra tất cả các ID đã lấy
            if (!string.IsNullOrEmpty(taichinhID) && !string.IsNullOrEmpty(hangvotID) &&
                !string.IsNullOrEmpty(votID))
            {
                // Kết hợp thành chuỗi theo định dạng yêu cầu
                result = $"{taichinhID}^{hangvotID}>{votID}";
                // Kiểm tra nếu nội dung đã tồn tại trong cơ sở dữ liệu
                if (IsLuatExist(result))
                {
                    MessageBox.Show("Nội dung luật này đã tồn tại trong cơ sở dữ liệu.");
                    return; // Không tiếp tục thêm mới nếu đã tồn tại
                }
                label_Luat3.Text = result;
                label_Luat3.Visible = true;

                // Lấy ID cho luật mới
                string luatID = GetMaxLuatID();

                // Tạo câu truy vấn SQL để thêm luật vào bảng TapLuat
                string query = "INSERT INTO [TapLuat] (LuatID, NoiDung) VALUES ('" + luatID + "', '" + result + "')";

                // Thực thi câu lệnh SQL để thêm luật
                bool success = kn.Thucthi(query);

                if (success)
                {
                    MessageBox.Show("Thêm luật thành công.");
                }
                else
                {
                    MessageBox.Show("Có lỗi xảy ra khi thêm luật.");
                }
            }
            else
            {
                MessageBox.Show("Có lỗi khi lấy thông tin từ cơ sở dữ liệu.");
    }
        }
       
    }
}

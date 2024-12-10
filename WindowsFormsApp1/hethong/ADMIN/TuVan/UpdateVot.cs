using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WindowsFormsApp1.ketnoi.dangnhap;

namespace WindowsFormsApp1.hethong.ADMIN.TuVan
{
    public partial class UpdateVot : Form
    {
        ketnoiDN kn = new ketnoiDN();
        private string luatID;
        private bool isDragging = false;
        private Point startPoint = new Point(0, 0);
        public UpdateVot(string luatID)
        {
            InitializeComponent();
            this.luatID = luatID;
            label_Luat.Visible = false;
            guna2Button_Luu.Visible = false;

            // Di chuyển header
            panel_Header.MouseDown += new MouseEventHandler(panel_Header_MouseDown);
            panel_Header.MouseMove += new MouseEventHandler(panel_Header_MouseMove);
            panel_Header.MouseUp += new MouseEventHandler(panel_Header_MouseUp);
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
            LoadComboBoxData("SELECT TaiChinhID, NDtaichinh FROM [TAICHINH]", TaiChinh);
            LoadComboBoxData("SELECT HangVotID, NDhangvot FROM [HANGVOT]", HangVot);
            LoadComboBoxData("SELECT PhanLoaiNguoiID, NDphanloainguoi FROM [PHANLOAINGUOI]", NhomLoaiNguoi);
            LoadComboBoxData("SELECT PhanLoaiVotID, NDphanloaivot FROM [PHANLOAINVOT]", NhomLoaiVot);
            LoadComboBoxData("SELECT SanPhamID, TenSanPham FROM [VOT]", SanPham);
        }
        private string GetNoiDungLuat(string luatID)
        {
            string noiDungLuat = string.Empty;  // Biến để lưu nội dung của luật

            // Truy vấn SQL để lấy nội dung dựa trên luatID
            string query = $"SELECT NoiDung FROM [TapLuat] WHERE LuatID = '{luatID}'";

            // Lấy dữ liệu từ cơ sở dữ liệu
            DataSet ds = kn.Laydulieu(query);

            // Kiểm tra kết quả trả về
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                noiDungLuat = ds.Tables[0].Rows[0]["NoiDung"].ToString();
            }
            else
            {
                MessageBox.Show("Không tìm thấy nội dung của luật với ID: " + luatID);

            }
            return noiDungLuat;

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
        private void UpdateVot_Load(object sender, EventArgs e)
        {
            LoadComboBoxData();
            // Lấy nội dung của luật từ luatID và cập nhật lên label_Luat
            string noiDung = GetNoiDungLuat(luatID);
            label_Luat.Text = noiDung;  // Gán nội dung cho label
            // Tách chuỗi thành 2 phần: trước và sau dấu '>'
            string[] splitContent = label_Luat.Text.Split('>');
            string beforeContent = splitContent[0];  // Phần trước dấu '>'
            string afterContent = splitContent.Length > 1 ? splitContent[1] : "";  // Phần sau dấu '>'

            // Xử lý phần trước dấu '>'
            string[] beforeParts = beforeContent.Split('^');  // Tách chuỗi theo dấu ^
            // Kiểm tra và lấy DoCungID
            foreach (string part in beforeParts)
            {
                if (part.StartsWith("TC"))  // Kiểm tra phần tử có bắt đầu bằng "DC"
                {
                    string docungID = part;  // Lấy DoCungID (ví dụ: DC01)

                    // Lấy thông tin từ bảng DOCUNG dựa trên DoCungID
                    string query = $"SELECT NDtaichinh FROM [TAICHINH] WHERE TaiChinhID = '{docungID}'";
                    DataSet ds = kn.Laydulieu(query);

                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        string nddocung = ds.Tables[0].Rows[0]["NDtaichinh"].ToString();

                        // Kiểm tra giá trị đã có trong ComboBox chưa
                        if (!TaiChinh.Items.Contains(nddocung))
                        {
                            TaiChinh.Items.Add(nddocung); // Thêm nếu chưa có
                        }

                        // Chọn mục đầu tiên sau khi thêm
                        TaiChinh.SelectedItem = nddocung;
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy dữ liệu cho TaiChinhID: " + docungID);
                    }
                }
                else if (part.StartsWith("HV"))
                {
                    string succangID = part;

                    string query = $"SELECT NDhangvot FROM [HANGVOT] WHERE HangVotID = '{succangID}'";
                    DataSet ds = kn.Laydulieu(query);

                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        string ndsuccang = ds.Tables[0].Rows[0]["NDhangvot"].ToString();

                        // Kiểm tra giá trị đã có trong ComboBox chưa
                        if (!HangVot.Items.Contains(ndsuccang))
                        {
                            HangVot.Items.Add(ndsuccang); // Thêm nếu chưa có
                        }

                        // Chọn mục đầu tiên sau khi thêm
                        HangVot.SelectedItem = ndsuccang;
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy dữ liệu cho HangVotID: " + succangID);
                    }
                }
                else if (part.StartsWith("PLV"))
                {
                    string succangID = part;

                    string query = $"SELECT NDphanloaivot FROM [PHANLOAIVOT] WHERE PhanLoaiVotID = '{succangID}'";
                    DataSet ds = kn.Laydulieu(query);

                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        string ndsuccang = ds.Tables[0].Rows[0]["NDphanloaivot"].ToString();

                        // Kiểm tra giá trị đã có trong ComboBox chưa
                        if (!NhomLoaiVot.Items.Contains(ndsuccang))
                        {
                            NhomLoaiVot.Items.Add(ndsuccang); // Thêm nếu chưa có
                        }

                        // Chọn mục đầu tiên sau khi thêm
                        NhomLoaiVot.SelectedItem = ndsuccang;
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy dữ liệu cho PhanLoaiVotID: " + succangID);
                    }
                }
                else if (part.StartsWith("PLN"))
                {
                    string succangID = part;

                    string query = $"SELECT NDphanloainguoi FROM [PHANLOAINGUOI] WHERE PhanLoaiNguoiID = '{succangID}'";
                    DataSet ds = kn.Laydulieu(query);

                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        string ndsuccang = ds.Tables[0].Rows[0]["NDphanloainguoi"].ToString();

                        // Kiểm tra giá trị đã có trong ComboBox chưa
                        if (!NhomLoaiNguoi.Items.Contains(ndsuccang))
                        {
                            NhomLoaiNguoi.Items.Add(ndsuccang); // Thêm nếu chưa có
                        }

                        // Chọn mục đầu tiên sau khi thêm
                        NhomLoaiNguoi.SelectedItem = ndsuccang;
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy dữ liệu cho PhanLoaiNguoiID: " + succangID);
                    }
                }
            }

            // Tách chuỗi phần sau dấu '^'
            string[] afterParts = afterContent.Split('^');
            foreach (string part in afterParts)
            {
                if (part.StartsWith("V"))
                {
                    string phanloaivotID = part;

                    // Truy vấn dữ liệu từ cơ sở dữ liệu
                    string query = $"SELECT TenSanPham FROM [VOT] WHERE SanPhamID = '{phanloaivotID}'";
                    DataSet ds = kn.Laydulieu(query);

                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        // Lấy giá trị NDphanloaivot từ kết quả truy vấn
                        string ndphanloaivot = ds.Tables[0].Rows[0]["TenSanPham"].ToString();

                        // Kiểm tra xem giá trị này đã có trong ComboBox chưa
                        if (SanPham.Items.Contains(ndphanloaivot))
                        {
                            // Nếu có, thì hiển thị giá trị đó lên
                            SanPham.SelectedItem = ndphanloaivot;

                            // Lấy đường dẫn ảnh từ tên sản phẩm
                            string imagePath = GetProductImagePath(ndphanloaivot);

                            // Hiển thị ảnh lên PictureBox nếu đường dẫn không rỗng
                            if (!string.IsNullOrEmpty(imagePath))
                            {
                                try
                                {
                                    pictureBox_AnhSanPham.Image = Image.FromFile(imagePath);
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show($"Không thể tải ảnh từ đường dẫn: {imagePath}\nLỗi: {ex.Message}");
                                }
                            }
                        }
                        else
                        {
                            // Nếu không có, bạn có thể tùy chọn xử lý khác hoặc để trống
                            MessageBox.Show("Giá trị không tồn tại trong ComboBox.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy dữ liệu cho SanPhamID: " + phanloaivotID);
                    }
                }
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

        private void guna2Button_Back_Click(object sender, EventArgs e)
        {

            this.Close();
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
        private void guna2Button_TaoLuat_Click(object sender, EventArgs e)
        {
            // Khởi tạo chuỗi kết quả
            string result = "";

            // Lấy các giá trị đã chọn từ các ComboBox và tạo câu truy vấn để lấy ID
            string taichinh = TaiChinh.SelectedItem?.ToString();
            string hangvot = HangVot.SelectedItem?.ToString();
            string nhomLoaiNguoi = NhomLoaiNguoi.SelectedItem?.ToString();
            string nhomLoaiVot = NhomLoaiVot.SelectedItem?.ToString();
            string vot = SanPham.SelectedItem?.ToString();

            // Tạo câu truy vấn SQL để lấy các ID tương ứng cho các lựa chọn
            string taichinhID = GetIDFromDatabase("TaiChinh", "NDtaichinh", taichinh);
            string hangvotID = GetIDFromDatabase("HangVot", "NDhangvot", hangvot);
            string nhomLoaiNguoiID = GetIDFromDatabase("PhanLoaiNguoi", "NDphanloainguoi", nhomLoaiNguoi);
            string nhomLoaiVotID = GetIDFromDatabase("PhanLoaiVot", "NDphanloaivot", nhomLoaiVot);

            // Lấy votID bằng cách gọi hàm GetVotID
            string votID = GetVotID(vot);

            // Kiểm tra tất cả các ID đã lấy
            if (!string.IsNullOrEmpty(taichinhID) && !string.IsNullOrEmpty(hangvotID) && !string.IsNullOrEmpty(votID) &&
                !string.IsNullOrEmpty(nhomLoaiNguoiID) &&
                !string.IsNullOrEmpty(nhomLoaiVotID))
            {
                // Kết hợp thành chuỗi theo định dạng yêu cầu
                result = $"{hangvotID}^{taichinhID}^{nhomLoaiVotID}^{nhomLoaiNguoiID}>{votID}";
                // Kiểm tra nếu nội dung đã tồn tại trong cơ sở dữ liệu


                label_Luat.Visible = true;
                guna2Button_Luu.Visible = true;
                label_Luat.Text = result;

            }
        }

        private void guna2Button_Luu_Click(object sender, EventArgs e)
        {

            // Kiểm tra nếu chuỗi luật đã được tạo và có giá trị
            string result = label_Luat.Text;

            if (!string.IsNullOrEmpty(result))
            {
                // Tạo câu truy vấn SQL để cập nhật luật vào bảng TapLuat
                string query = "UPDATE [TapLuat] SET NoiDung = '" + result + "' WHERE LuatID = '" + luatID + "'";

                // Thực thi câu lệnh SQL để cập nhật luật
                bool success = kn.Thucthi(query);

                if (success)
                {
                    MessageBox.Show("Cập nhật luật thành công.");
                }
                else
                {
                    MessageBox.Show("Có lỗi xảy ra khi cập nhật luật.");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng tạo luật trước khi lưu.");
            }
        }

        private void HangVot_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedHangVot = HangVot.SelectedItem?.ToString();
            UpdateSanPhamList(selectedHangVot);
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
    }
}

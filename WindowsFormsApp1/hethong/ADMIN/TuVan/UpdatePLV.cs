using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using WindowsFormsApp1.ketnoi.dangnhap;

namespace WindowsFormsApp1.hethong.ADMIN.TuVan
{
    public partial class UpdatePLV : Form
    {
        ketnoiDN kn = new ketnoiDN();
        private string luatID;
        private bool isDragging = false;
        private Point startPoint = new Point(0, 0);
        public UpdatePLV(string luatID)
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
        private void guna2Button_TaoLuat_Click(object sender, EventArgs e)
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
            LoadComboBoxData("SELECT ChatLieuID, NDchatlieu FROM [CHATLIEU]", ChatLieu);
            LoadComboBoxData("SELECT SucCangID, NDsuccang FROM [SUCCANG]", SucCang);
            LoadComboBoxData("SELECT TrongLuongVungID, NDtrongluongvung FROM [TRONGLUONGVUNG]", TrongLuongVung);
            LoadComboBoxData("SELECT TrongLuongID, NDtrongluong FROM [TRONGLUONG]", TrongLuong);
            LoadComboBoxData("SELECT MatVotID, NDmatvot FROM [MATVOT]", DangMatVot);
            LoadComboBoxData("SELECT ChuViID, NDchuvi FROM [CHUVI]", ChuVi);
            LoadComboBoxData("SELECT PhanLoaiVotID, NDphanloaivot FROM [PHANLOAIVOT]", NhomLoaiVot);
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
        private void UpdatePLV_Load(object sender, EventArgs e)
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
                if (part.StartsWith("DC"))  // Kiểm tra phần tử có bắt đầu bằng "DC"
                {
                    string docungID = part;  // Lấy DoCungID (ví dụ: DC01)

                    // Lấy thông tin từ bảng DOCUNG dựa trên DoCungID
                    string query = $"SELECT NDdocung FROM [DOCUNG] WHERE DoCungID = '{docungID}'";
                    DataSet ds = kn.Laydulieu(query);

                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        string nddocung = ds.Tables[0].Rows[0]["NDdocung"].ToString();

                        // Kiểm tra giá trị đã có trong ComboBox chưa
                        if (!DoCung.Items.Contains(nddocung))
                        {
                            DoCung.Items.Add(nddocung); // Thêm nếu chưa có
                        }

                        // Chọn mục đầu tiên sau khi thêm
                        DoCung.SelectedItem = nddocung;
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy dữ liệu cho DoCungID: " + docungID);
                    }
                }
                else if (part.StartsWith("SC"))
                {
                    string succangID = part;

                    string query = $"SELECT NDsuccang FROM [SUCCANG] WHERE SucCangID = '{succangID}'";
                    DataSet ds = kn.Laydulieu(query);

                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        string ndsuccang = ds.Tables[0].Rows[0]["NDsuccang"].ToString();

                        // Kiểm tra giá trị đã có trong ComboBox chưa
                        if (!SucCang.Items.Contains(ndsuccang))
                        {
                            SucCang.Items.Add(ndsuccang); // Thêm nếu chưa có
                        }

                        // Chọn mục đầu tiên sau khi thêm
                        SucCang.SelectedItem = ndsuccang;
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy dữ liệu cho SucCangID: " + succangID);
                    }
                }
                else if (part.StartsWith("TLV"))  // Kiểm tra trước nếu là "TLV"
                {
                    string succangID = part;

                    string query = $"SELECT NDtrongluongvung FROM [TRONGLUONGVUNG] WHERE TrongLuongVungID = '{succangID}'";
                    DataSet ds = kn.Laydulieu(query);

                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        string ndtrongluongvung = ds.Tables[0].Rows[0]["NDtrongluongvung"].ToString();

                        // Kiểm tra giá trị đã có trong ComboBox chưa
                        if (!TrongLuongVung.Items.Contains(ndtrongluongvung))
                        {
                            TrongLuongVung.Items.Add(ndtrongluongvung); // Thêm nếu chưa có
                        }

                        // Chọn mục đầu tiên sau khi thêm
                        TrongLuongVung.SelectedItem = ndtrongluongvung;
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy dữ liệu cho TrongLuongVungID: " + succangID);
                    }
                }
                else if (part.StartsWith("TL"))  // Kiểm tra tiếp nếu không phải "TLV"
                {
                    string succangID = part;

                    string query = $"SELECT NDtrongluong FROM [TRONGLUONG] WHERE TrongLuongID = '{succangID}'";
                    DataSet ds = kn.Laydulieu(query);

                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        string ndtrongluong = ds.Tables[0].Rows[0]["NDtrongluong"].ToString();

                        // Kiểm tra giá trị đã có trong ComboBox chưa
                        if (!TrongLuong.Items.Contains(ndtrongluong))
                        {
                            TrongLuong.Items.Add(ndtrongluong); // Thêm nếu chưa có
                        }

                        // Chọn mục đầu tiên sau khi thêm
                        TrongLuong.SelectedItem = ndtrongluong;
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy dữ liệu cho TrongLuongID: " + succangID);
                    }
                }
                else if (part.StartsWith("CV"))
                {
                    string succangID = part;

                    string query = $"SELECT NDchuvi FROM [CHUVI] WHERE ChuViID = '{succangID}'";
                    DataSet ds = kn.Laydulieu(query);

                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        string ndchuvi = ds.Tables[0].Rows[0]["NDchuvi"].ToString();

                        // Kiểm tra giá trị đã có trong ComboBox chưa
                        if (!ChuVi.Items.Contains(ndchuvi))
                        {
                            ChuVi.Items.Add(ndchuvi); // Thêm nếu chưa có
                        }

                        // Chọn mục đầu tiên sau khi thêm
                        ChuVi.SelectedItem = ndchuvi;
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy dữ liệu cho ChuViID: " + succangID);
                    }
                }
                else if (part.StartsWith("CL"))
                {
                    string succangID = part;

                    string query = $"SELECT NDchatlieu FROM [CHATLIEU] WHERE ChatLieuID = '{succangID}'";
                    DataSet ds = kn.Laydulieu(query);

                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        string ndchatlieu = ds.Tables[0].Rows[0]["NDchatlieu"].ToString();

                        // Kiểm tra giá trị đã có trong ComboBox chưa
                        if (!ChatLieu.Items.Contains(ndchatlieu))
                        {
                            ChatLieu.Items.Add(ndchatlieu); // Thêm nếu chưa có
                        }

                        // Chọn mục đầu tiên sau khi thêm
                        ChatLieu.SelectedItem = ndchatlieu;
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy dữ liệu cho ChatLieuID: " + succangID);
                    }
                }
                else if (part.StartsWith("CD"))
                {
                    string succangID = part;

                    string query = $"SELECT NDchieudai FROM [CHIEUDAI] WHERE ChieuDaiID = '{succangID}'";
                    DataSet ds = kn.Laydulieu(query);

                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        string ndchieudai = ds.Tables[0].Rows[0]["NDchieudai"].ToString();

                        // Kiểm tra giá trị đã có trong ComboBox chưa
                        if (!ChieuDai.Items.Contains(ndchieudai))
                        {
                            ChieuDai.Items.Add(ndchieudai); // Thêm nếu chưa có
                        }

                        // Chọn mục đầu tiên sau khi thêm
                        ChieuDai.SelectedItem = ndchieudai;
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy dữ liệu cho ChieuDaiID: " + succangID);
                    }
                }
                else if (part.StartsWith("MV"))
                {
                    string succangID = part;

                    string query = $"SELECT NDmatvot FROM [MATVOT] WHERE MatVotID = '{succangID}'";
                    DataSet ds = kn.Laydulieu(query);

                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        string ndmatvot = ds.Tables[0].Rows[0]["NDmatvot"].ToString();

                        // Kiểm tra giá trị đã có trong ComboBox chưa
                        if (!DangMatVot.Items.Contains(ndmatvot))
                        {
                            DangMatVot.Items.Add(ndmatvot); // Thêm nếu chưa có
                        }

                        // Chọn mục đầu tiên sau khi thêm
                        DangMatVot.SelectedItem = ndmatvot;
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy dữ liệu cho MatVotID: " + succangID);
                    }
                }
            }

            // Tách chuỗi phần sau dấu '^'
            string[] afterParts = afterContent.Split('^');
            foreach (string part in afterParts)
            {
                if (part.StartsWith("PLV"))
                {
                    string phanloaivotID = part;

                    // Truy vấn dữ liệu từ cơ sở dữ liệu
                    string query = $"SELECT NDphanloaivot FROM [PHANLOAIVOT] WHERE PhanLoaiVotID = '{phanloaivotID}'";
                    DataSet ds = kn.Laydulieu(query);

                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        // Lấy giá trị NDphanloaivot từ kết quả truy vấn
                        string ndphanloaivot = ds.Tables[0].Rows[0]["NDphanloaivot"].ToString();

                        // Kiểm tra xem giá trị này đã có trong ComboBox chưa
                        if (NhomLoaiVot.Items.Contains(ndphanloaivot))
                        {
                            // Nếu có, thì hiển thị giá trị đó lên
                            NhomLoaiVot.SelectedItem = ndphanloaivot;
                        }
                        else
                        {
                            // Nếu không có, bạn có thể tùy chọn xử lý khác hoặc để trống
                            MessageBox.Show("Giá trị không tồn tại trong ComboBox.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy dữ liệu cho PhanLoaiVotID: " + phanloaivotID);
                    }
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

        private void guna2Button_Back_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
    }
}

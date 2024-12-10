using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using WindowsFormsApp1.ketnoi.dangnhap;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace WindowsFormsApp1.hethong.ADMIN.TuVan
{
    public partial class UpdatePLN : Form
    {
        ketnoiDN kn = new ketnoiDN();
        private string luatID;
        private bool isDragging = false;
        private Point startPoint = new Point(0, 0);
        public UpdatePLN(string luatID)
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
            LoadComboBoxData("SELECT MucDichID, NDmucdich FROM [MUCDICH]", MucDich);
            LoadComboBoxData("SELECT TrinhDoID, NDtrinhdo FROM [TRINHDO]", TrinhDo);
            LoadComboBoxData("SELECT PhanLoaiNguoiID, NDphanloainguoi FROM [PHANLOAINGUOI]", NhomLoaiNguoi);
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
        private void guna2Button_Back_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void UpdatePLN_Load(object sender, EventArgs e)
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
                if (part.StartsWith("MD"))  // Kiểm tra phần tử có bắt đầu bằng "DC"
                {
                    string docungID = part;  // Lấy DoCungID (ví dụ: DC01)

                    // Lấy thông tin từ bảng DOCUNG dựa trên DoCungID
                    string query = $"SELECT NDmucdich FROM [MUCDICH] WHERE MucDichID = '{docungID}'";
                    DataSet ds = kn.Laydulieu(query);

                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        string nddocung = ds.Tables[0].Rows[0]["NDmucdich"].ToString();

                        // Kiểm tra giá trị đã có trong ComboBox chưa
                        if (!MucDich.Items.Contains(nddocung))
                        {
                            MucDich.Items.Add(nddocung); // Thêm nếu chưa có
                        }

                        // Chọn mục đầu tiên sau khi thêm
                        MucDich.SelectedItem = nddocung;
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy dữ liệu cho MucDichID: " + docungID);
                    }
                }
                else if (part.StartsWith("TD"))
                {
                    string succangID = part;

                    string query = $"SELECT NDtrinhdo FROM [TRINHDO] WHERE TrinhDoID = '{succangID}'";
                    DataSet ds = kn.Laydulieu(query);

                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        string ndsuccang = ds.Tables[0].Rows[0]["NDtrinhdo"].ToString();

                        // Kiểm tra giá trị đã có trong ComboBox chưa
                        if (!TrinhDo.Items.Contains(ndsuccang))
                        {
                            TrinhDo.Items.Add(ndsuccang); // Thêm nếu chưa có
                        }

                        // Chọn mục đầu tiên sau khi thêm
                        TrinhDo.SelectedItem = ndsuccang;
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy dữ liệu cho TrinhDoID: " + succangID);
                    }
                }
                
            }

            // Tách chuỗi phần sau dấu '^'
            string[] afterParts = afterContent.Split('^');
            foreach (string part in afterParts)
            {
                if (part.StartsWith("PLN"))
                {
                    string phanloaivotID = part;

                    // Truy vấn dữ liệu từ cơ sở dữ liệu
                    string query = $"SELECT NDphanloainguoi FROM [PHANLOAINGUOI] WHERE PhanLoaiNguoiID = '{phanloaivotID}'";
                    DataSet ds = kn.Laydulieu(query);

                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        // Lấy giá trị NDphanloaivot từ kết quả truy vấn
                        string ndphanloaivot = ds.Tables[0].Rows[0]["NDphanloainguoi"].ToString();

                        // Kiểm tra xem giá trị này đã có trong ComboBox chưa
                        if (NhomLoaiNguoi.Items.Contains(ndphanloaivot))
                        {
                            // Nếu có, thì hiển thị giá trị đó lên
                            NhomLoaiNguoi.SelectedItem = ndphanloaivot;
                        }
                        else
                        {
                            // Nếu không có, bạn có thể tùy chọn xử lý khác hoặc để trống
                            MessageBox.Show("Giá trị không tồn tại trong ComboBox.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy dữ liệu cho PhanLoaiNguoitID: " + phanloaivotID);
                    }
                }
            }
        }

        private void guna2Button_TaoLuat_Click(object sender, EventArgs e)
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
    }
}

using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using WindowsFormsApp1.hethong.sanpham;
using WindowsFormsApp1.ketnoi.dangnhap;

namespace WindowsFormsApp1.hethong.ADMIN.TuVan
{
    public partial class DSLuat : Form
    {
        ketnoiDN ketnoi = new ketnoiDN();
        public DSLuat()
        {
            InitializeComponent();
            gunuHighligh_TimKiem.PlaceholderText = "Tìm kiếm luật...";
            guna2Button_LamMoi.Visible = false;
            guna2Button_SuaLuat.Visible = false;
            guna2Button_XoaLuat.Visible=false;
        }
        private void DSLuat_Load(object sender, EventArgs e)
        {
            // Câu lệnh SQL để lấy dữ liệu từ bảng TapLuat
            string query = "SELECT LuatID, NoiDung FROM [TapLuat]"; // Chỉ lấy những cột cần thiết

            // Lấy dữ liệu từ cơ sở dữ liệu
            DataSet ds = ketnoi.Laydulieu(query);
            DataTable dt = ds.Tables[0];

            // Gọi hàm thêm cột vào DataGridView
            AddColumnsToDataGridView();

            // Gán dữ liệu cho DataGridView
            dataGridView_DSLUAT.DataSource = dt;
        }
        private void AddColumnsToDataGridView()
        {
            // Kiểm tra nếu cột đã được thêm
            if (dataGridView_DSLUAT.Columns.Count == 0)
            {
                // Thêm cột "LuatID"
                DataGridViewTextBoxColumn colLuatID = new DataGridViewTextBoxColumn();
                colLuatID.Name = "LuatID";
                colLuatID.HeaderText = "Mã Luật";
                colLuatID.Width = 150;
                colLuatID.DataPropertyName = "LuatID";  // Đảm bảo này khớp với tên cột trong DataTable
                dataGridView_DSLUAT.Columns.Add(colLuatID);

                // Thêm cột "NoiDung"
                DataGridViewTextBoxColumn colNoiDung = new DataGridViewTextBoxColumn();
                colNoiDung.Name = "NoiDung";
                colNoiDung.HeaderText = "Nội Dung";
                colNoiDung.Width = 430;
                colNoiDung.DataPropertyName = "NoiDung"; // Đảm bảo này khớp với tên cột trong DataTable
                dataGridView_DSLUAT.Columns.Add(colNoiDung);
            }
        }
        private void dataGridView_DSLUAT_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra nếu click vào một hàng hợp lệ (không phải header)
            if (e.RowIndex >= 0)
            {
                // Lấy dòng được chọn
                DataGridViewRow row = dataGridView_DSLUAT.Rows[e.RowIndex];

                // Kiểm tra nếu giá trị của các ô không null
                if (row.Cells["LuatID"].Value != null && row.Cells["NoiDung"].Value != null)
                {
                    // Hiển thị giá trị vào các TextBox
                    guna2TextBox_IDluat.Text = row.Cells["LuatID"].Value.ToString();
                    guna2TextBox_NDluat.Text = row.Cells["NoiDung"].Value.ToString();
                    guna2Button_XoaLuat.Visible = true;
                    guna2Button_SuaLuat.Visible = true;
                    guna2Button_LamMoi.Visible = true;
                }
            }
        }


        private void guna2Button_SuaLuat_Click(object sender, EventArgs e)
        {
            // Lấy nội dung luật từ TextBox
            string noiDungLuat = guna2TextBox_NDluat.Text;
            // Lấy LuatID từ TextBox
            string luatID = guna2TextBox_IDluat.Text;

            // Kiểm tra nếu nội dung có dấu '>'
            if (noiDungLuat.Contains(">"))
            {
                // Tách nội dung theo dấu ">"
                string[] parts = noiDungLuat.Split('>');

                // Kiểm tra phần sau dấu ">"
                if (parts.Length > 1)
                {
                    // Lấy phần sau dấu '>' và loại bỏ khoảng trắng thừa
                    string partAfterGreaterThan = parts[1].Trim();


                    // So sánh với giá trị bắt đầu là "PLN"
                    if (partAfterGreaterThan.StartsWith("PLN", StringComparison.OrdinalIgnoreCase))
                    {
                        // Nếu bắt đầu bằng "PLN", điều hướng đến trang UpdatePLN
                        UpdatePLN updatePLNForm = new UpdatePLN(luatID); // Tạo instance của form UpdatePLN
                        updatePLNForm.Show(); // Hiển thị form UpdatePLN
                    }
                    // So sánh với giá trị bắt đầu là "PLV"
                    if (partAfterGreaterThan.StartsWith("PLV", StringComparison.OrdinalIgnoreCase))
                    {
                        // Nếu bắt đầu bằng "PLN", điều hướng đến trang UpdatePLN
                        UpdatePLV updatePLNForm = new UpdatePLV(luatID); // Tạo instance của form UpdatePLN
                        updatePLNForm.Show(); // Hiển thị form UpdatePLN
                    }
                    // So sánh với giá trị bắt đầu là "V"
                    if (partAfterGreaterThan.StartsWith("V", StringComparison.OrdinalIgnoreCase))
                    {
                        // Nếu bắt đầu bằng "PLN", điều hướng đến trang UpdatePLN
                        UpdateVot updatePLNForm = new UpdateVot(luatID); // Tạo instance của form UpdatePLN
                        updatePLNForm.Show(); // Hiển thị form UpdatePLN
                    }
                }
                else
                {
                    // Nếu không có phần sau dấu '>', thông báo lỗi
                    MessageBox.Show("Nội dung không có phần sau dấu '>'!");
                }
            }
            }
        private void guna2Button_LamMoi_Click(object sender, EventArgs e)
        {
            // Làm trống các TextBox
            guna2TextBox_IDluat.Text = "";
            guna2TextBox_NDluat.Text = "";

            // Ẩn các nút "Sửa", "Xóa", và "Làm mới" khi làm mới
            guna2Button_SuaLuat.Visible = false;
            guna2Button_XoaLuat.Visible = false;
            guna2Button_LamMoi.Visible = false;

            // Nếu bạn muốn làm mới bảng DataGridView, có thể reload lại dữ liệu từ cơ sở dữ liệu
            DSLuat_Load(sender, e);
        }
        private void guna2Button_XoaLuat_Click(object sender, EventArgs e)
        {
            // Lấy giá trị LuatID từ TextBox
            string luatID = guna2TextBox_IDluat.Text;

            // Kiểm tra nếu LuatID không rỗng
            if (string.IsNullOrEmpty(luatID))
            {
                MessageBox.Show("Vui lòng chọn một luật để xóa.");
                return;
            }

            // Xác nhận hành động xóa
            DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn xóa luật này?", "Xóa Luật", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                // Câu lệnh SQL để xóa bản ghi từ bảng TapLuat
                string query = $"DELETE FROM [TapLuat] WHERE LuatID = '{luatID}'";

                // Thực thi câu lệnh SQL
                try
                {
                    // Gọi phương thức Thucthi để thực thi câu lệnh SQL xóa
                    ketnoi.Thucthi(query);

                    // Thông báo xóa thành công
                    MessageBox.Show("Xóa luật thành công!");

                    // Làm mới lại danh sách luật
                    DSLuat_Load(sender, e);
                }
                catch (Exception ex)
                {
                    // Thông báo lỗi nếu có vấn đề khi xóa
                    MessageBox.Show("Có lỗi xảy ra: " + ex.Message);
                }
            }
        }
        private void guna2Button_Them_Click(object sender, EventArgs e)
        {
            // Tạo instance của form ThemLuat
            ThemLuat themLuatForm = new ThemLuat();

            // Hiển thị form ThemLuat
            themLuatForm.Show();

            // Ẩn form hiện tại (nếu muốn)
            this.Hide();
        }
        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
        private void TimKiemLuat()
        {
            // Lấy giá trị từ TextBox tìm kiếm
            string searchKeyword = gunuHighligh_TimKiem.Text.Trim().ToLower(); // Lọc theo từ khóa nhập vào, chuyển thành chữ thường để tìm kiếm không phân biệt chữ hoa chữ thường

            // Câu lệnh SQL để lấy tất cả dữ liệu từ bảng TapLuat
            string query = "SELECT LuatID, NoiDung FROM [TapLuat]";

            // Lấy dữ liệu từ cơ sở dữ liệu
            DataSet ds = ketnoi.Laydulieu(query);
            DataTable dt = ds.Tables[0];

            // Lọc DataTable theo từ khóa tìm kiếm
            DataView dv = dt.DefaultView;
            dv.RowFilter = string.Format("LuatID LIKE '%{0}%' OR NoiDung LIKE '%{0}%'", searchKeyword); // Lọc theo LuatID và NoiDung

            // Gán dữ liệu đã lọc vào DataGridView
            dataGridView_DSLUAT.DataSource = dv.ToTable();
        }

        private void gunuHighligh_TimKiem_TextChanged(object sender, EventArgs e)
        {
            TimKiemLuat();
        }
    }
}

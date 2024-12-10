using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.ketnoi.dangnhap;

namespace WindowsFormsApp1.hethong.ADMIN.SuKien
{
    public partial class DoCung : Form
    {
        ketnoiDN ketnoi = new ketnoiDN();
        public DoCung()
        {
            InitializeComponent();
            gunuHighligh_TimKiem.PlaceholderText = "Tìm kiếm ...";
            guna2Button_LamMoi.Visible = false;
            guna2Button_SuaLuat.Visible = false;
            guna2Button_XoaLuat.Visible = false;
        }

        private void DoCung_Load(object sender, EventArgs e)
        {
            // Câu lệnh SQL để lấy dữ liệu từ bảng TapLuat
            string query = "SELECT DoCungID, NDdocung FROM [DOCUNG]"; // Chỉ lấy những cột cần thiết

            // Lấy dữ liệu từ cơ sở dữ liệu
            DataSet ds = ketnoi.Laydulieu(query);
            DataTable dt = ds.Tables[0];

            // Gọi hàm thêm cột vào DataGridView
            AddColumnsToDataGridView();

            // Gán dữ liệu cho DataGridView
            dataGridView_DanhSach.DataSource = dt;
        }
        private void AddColumnsToDataGridView()
        {
            // Kiểm tra nếu cột đã được thêm
            if (dataGridView_DanhSach.Columns.Count == 0)
            {
                // Thêm cột "LuatID"
                DataGridViewTextBoxColumn colLuatID = new DataGridViewTextBoxColumn();
                colLuatID.Name = "DoCungID";
                colLuatID.HeaderText = "Mã Mục Đích";
                colLuatID.Width = 50;
                colLuatID.DataPropertyName = "DoCungID";  // Đảm bảo này khớp với tên cột trong DataTable
                dataGridView_DanhSach.Columns.Add(colLuatID);

                // Thêm cột "NoiDung"
                DataGridViewTextBoxColumn colNoiDung = new DataGridViewTextBoxColumn();
                colNoiDung.Name = "NDdocung";
                colNoiDung.HeaderText = "Nội Dung";
                colNoiDung.Width = 250;
                colNoiDung.DataPropertyName = "NDdocung"; // Đảm bảo này khớp với tên cột trong DataTable
                dataGridView_DanhSach.Columns.Add(colNoiDung);
            }
        }
        private void dataGridView_DanhSach_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            // Kiểm tra nếu click vào một hàng hợp lệ (không phải header)
            if (e.RowIndex >= 0)
            {
                // Lấy dòng được chọn
                DataGridViewRow row = dataGridView_DanhSach.Rows[e.RowIndex];

                // Kiểm tra nếu giá trị của các ô không null
                if (row.Cells["DoCungID"].Value != null && row.Cells["NDdocung"].Value != null)
                {
                    // Hiển thị giá trị vào các TextBox
                    guna2TextBox_IDSuKien.Text = row.Cells["DoCungID"].Value.ToString();
                    guna2TextBox_NDSuKien.Text = row.Cells["NDdocung"].Value.ToString();
                    guna2Button_XoaLuat.Visible = true;
                    guna2Button_SuaLuat.Visible = true;
                    guna2Button_LamMoi.Visible = true;
                    guna2TextBox_IDSuKien.Enabled = false;
                    guna2Button_Them.Enabled = false;
                }
            }
        }

        private void guna2Button_LamMoi_Click(object sender, EventArgs e)
        {
            // Làm trống các TextBox
            guna2TextBox_IDSuKien.Text = "";
            guna2TextBox_NDSuKien.Text = "";

            // Ẩn các nút "Sửa", "Xóa", và "Làm mới" khi làm mới
            guna2Button_SuaLuat.Visible = false;
            guna2Button_XoaLuat.Visible = false;
            guna2Button_LamMoi.Visible = false;
            guna2TextBox_IDSuKien.Enabled = true;
            guna2Button_Them.Enabled = true;

            // Nếu bạn muốn làm mới bảng DataGridView, có thể reload lại dữ liệu từ cơ sở dữ liệu
            DoCung_Load(sender, e);
        }

        private void guna2Button_Them_Click(object sender, EventArgs e)
        {
            // Lấy giá trị từ các TextBox
            string mucDichID = guna2TextBox_IDSuKien.Text.Trim();
            string ndMucDich = guna2TextBox_NDSuKien.Text.Trim();

            // Kiểm tra dữ liệu nhập
            if (string.IsNullOrEmpty(mucDichID) || string.IsNullOrEmpty(ndMucDich))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin trước khi thêm mục đích.");
                return;
            }

            // Câu lệnh SQL để kiểm tra nếu MucDichID hoặc NDmucdich đã tồn tại
            string checkQuery = $"SELECT COUNT(*) FROM [DOCUNG] WHERE DoCungID = '{mucDichID}' OR NDdocung = N'{ndMucDich}'";

            try
            {
                // Lấy kết quả kiểm tra
                DataSet ds = ketnoi.Laydulieu(checkQuery);

                // Kiểm tra nếu ds không phải là null và chứa ít nhất một bảng
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    int exists = Convert.ToInt32(ds.Tables[0].Rows[0][0]);

                    if (exists > 0)
                    {
                        MessageBox.Show("Đã tồn tại, vui lòng nhập thông tin khác.");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Không có dữ liệu trả về hoặc có lỗi xảy ra.");
                    return;
                }

                // Câu lệnh SQL để thêm dữ liệu vào bảng CHATLIEU
                string insertQuery = $"INSERT INTO [DOCUNG] (DoCungID, NDdocung) VALUES ('{mucDichID}', N'{ndMucDich}')";

                // Thực thi câu lệnh SQL
                ketnoi.Thucthi(insertQuery);

                // Thông báo thêm thành công
                MessageBox.Show("Thêm thành công!");

                // Làm mới lại danh sách
                DoCung_Load(sender, e);

                // Làm sạch các TextBox
                guna2Button_LamMoi_Click(sender, e);
            }
            catch (Exception ex)
            {
                // Thông báo lỗi nếu có vấn đề
                MessageBox.Show("Có lỗi xảy ra khi thêm: " + ex.Message);
            }
        }

        private void guna2Button_SuaLuat_Click(object sender, EventArgs e)
        {

            // Lấy giá trị từ các TextBox
            string mucDichID = guna2TextBox_IDSuKien.Text.Trim();
            string ndMucDich = guna2TextBox_NDSuKien.Text.Trim();

            // Kiểm tra dữ liệu nhập
            if (string.IsNullOrEmpty(mucDichID) || string.IsNullOrEmpty(ndMucDich))
            {
                MessageBox.Show("Vui lòng chọn mục đích cần sửa và điền đầy đủ thông tin.");
                return;
            }

            // Câu lệnh SQL để kiểm tra nếu NDmucdich đã tồn tại nhưng không phải của chính MucDichID hiện tại
            string checkQuery = $"SELECT COUNT(*) FROM [DOCUNG] WHERE NDdocung = N'{ndMucDich}' AND DoCungID != '{mucDichID}'";

            try
            {
                // Lấy kết quả kiểm tra
                int exists = Convert.ToInt32(ketnoi.Laydulieu(checkQuery).Tables[0].Rows[0][0]);

                if (exists > 0)
                {
                    MessageBox.Show("Nội dung mục đích đã tồn tại, vui lòng nhập nội dung khác.");
                    return;
                }

                // Câu lệnh SQL để cập nhật dữ liệu
                string updateQuery = $"UPDATE [DOCUNG] SET NDdocung = N'{ndMucDich}' WHERE DoCungID = '{mucDichID}'";

                // Thực thi câu lệnh SQL
                ketnoi.Thucthi(updateQuery);

                // Thông báo sửa thành công
                MessageBox.Show("Cập nhật thành công!");

                // Làm mới lại danh sách
                DoCung_Load(sender, e);

                // Làm sạch các TextBox
                guna2Button_LamMoi_Click(sender, e);
            }
            catch (Exception ex)
            {
                // Thông báo lỗi nếu có vấn đề
                MessageBox.Show("Có lỗi xảy ra khi cập nhật: " + ex.Message);
            }
        }

        private void guna2Button_XoaLuat_Click(object sender, EventArgs e)
        {

            // Lấy giá trị LuatID từ TextBox
            string luatID = guna2TextBox_IDSuKien.Text;

            // Kiểm tra nếu LuatID không rỗng
            if (string.IsNullOrEmpty(luatID))
            {
                MessageBox.Show("Vui lòng chọn một sự kiện để xóa.");
                return;
            }

            // Xác nhận hành động xóa
            DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn xóa sự kiện này?", "Xóa Sự Kiện", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                // Câu lệnh SQL để xóa bản ghi từ bảng TapLuat
                string query = $"DELETE FROM [DOCUNG] WHERE DoCungID = '{luatID}'";

                // Thực thi câu lệnh SQL
                try
                {
                    // Gọi phương thức Thucthi để thực thi câu lệnh SQL xóa
                    ketnoi.Thucthi(query);

                    // Thông báo xóa thành công
                    MessageBox.Show("Xóa luật thành công!");

                    // Làm mới lại danh sách luật
                    DoCung_Load(sender, e);
                }
                catch (Exception ex)
                {
                    // Thông báo lỗi nếu có vấn đề khi xóa
                    MessageBox.Show("Có lỗi xảy ra: " + ex.Message);
                }
            }
        }
        private void TimKiem()
        {
            // Lấy giá trị từ TextBox tìm kiếm
            string searchKeyword = gunuHighligh_TimKiem.Text.Trim().ToLower(); // Lọc theo từ khóa nhập vào, chuyển thành chữ thường để tìm kiếm không phân biệt chữ hoa chữ thường

            // Câu lệnh SQL để lấy tất cả dữ liệu từ bảng TapLuat
            string query = "SELECT DoCungID, NDdocung FROM [DOCUNG]";

            // Lấy dữ liệu từ cơ sở dữ liệu
            DataSet ds = ketnoi.Laydulieu(query);
            DataTable dt = ds.Tables[0];

            // Lọc DataTable theo từ khóa tìm kiếm
            DataView dv = dt.DefaultView;
            dv.RowFilter = string.Format("DoCungID LIKE '%{0}%' OR NDdocung LIKE '%{0}%'", searchKeyword); // Lọc theo LuatID và NoiDung

            // Gán dữ liệu đã lọc vào DataGridView
            dataGridView_DanhSach.DataSource = dv.ToTable();
        }
        private void gunuHighligh_TimKiem_TextChanged(object sender, EventArgs e)
        {
            TimKiem();
        }
    }
}

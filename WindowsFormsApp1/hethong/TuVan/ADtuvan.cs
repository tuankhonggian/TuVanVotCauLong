using Guna.UI2.WinForms;
using System;
using System.Data;
using System.Windows.Forms;
using WindowsFormsApp1.ketnoi.dangnhap;

namespace WindowsFormsApp1.hethong.TuVan
{
    public partial class ADtuvan : Form
    {
        ketnoiDN kn = new ketnoiDN();
        // Biến để lưu trữ sự kiện hiện tại trong DataGridView
        private string currentEvent = string.Empty;
        private void InitializeComboBoxes()
        {
            LoiChoi.Tag = "Lối Chơi";
            LucCoTay.Tag = "Lực Cổ Tay";
            ThoiGianChoi.Tag = "Thời Gian Chơi";
            GioiTinh.Tag = "Giới Tính";
            MucDichCaiThien.Tag = "Mục Đích Cải Thiện";
            DacDiemVot.Tag = "Đặc Điểm Vợt";
            TrongLuongVot.Tag = "Trọng Lượng Vợt";
            ChatLuongVaDoBen.Tag = "Chất Lượng Và Độ Bền";
            HangVot.Tag = "Hãng Vợt";
        }
        private void LoadProductsIntoComboBox()
        {
            string sql = "SELECT TenSanPham FROM [TT.VotYonex]";
            DataSet ds = kn.Laydulieu(sql);

            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                SanPhamChon.Items.Clear();

                foreach (DataRow row in dt.Rows)
                {
                    SanPhamChon.Items.Add(row["TenSanPham"].ToString());
                }
            }
            else
            {
                MessageBox.Show("Không thể lấy dữ liệu từ cơ sở dữ liệu.");
            }
        }
        public ADtuvan()
        {
            InitializeComponent();
            InitializeComboBoxes();
            LoadProductsIntoComboBox();
        }
        //Xử Lý Sự Kiện Trên DTGR
        private void UpdateDataGridView(string selectedEvent, Guna2ComboBox comboBox)
        {
            if (comboBox.Tag == null)
            {
                MessageBox.Show($"Tag is not set for ComboBox: {comboBox.Name}");
                return;
            }

            // Xóa các dòng liên quan đến ComboBox đang thay đổi
            foreach (DataGridViewRow row in dataGridView_DieuKienThem.Rows)
            {
                // Kiểm tra xem dòng có chứa sự kiện từ ComboBox đang thay đổi không
                if (row.Cells[0].Value.ToString().StartsWith(comboBox.Tag.ToString() + ":"))
                {
                    dataGridView_DieuKienThem.Rows.Remove(row);
                }
            }

            // Thêm sự kiện mới vào DataGridView với định dạng ComboBox: Sự kiện
            if (!string.IsNullOrEmpty(selectedEvent))
            {
                dataGridView_DieuKienThem.Rows.Add(comboBox.Tag.ToString() + ": " + selectedEvent);
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }


        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ThemLuat_Click(object sender, EventArgs e)
        {

        }

        private void dtgr_DieuKienMoiThem_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dtgr_SanPhamPhuHop_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void LoiChoi_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (LoiChoi.SelectedItem != null)
            {
                string selectedEvent = LoiChoi.SelectedItem.ToString();
                UpdateDataGridView(selectedEvent, LoiChoi);
            }
        }
        private void LucCoTay_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (LucCoTay.SelectedItem != null)
            {
                string selectedEvent = LucCoTay.SelectedItem.ToString();
                UpdateDataGridView(selectedEvent, LucCoTay);
            }
        }

        private void ThoiGianChoi_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ThoiGianChoi.SelectedItem != null)
            {
                string selectedEvent = ThoiGianChoi.SelectedItem.ToString();
                UpdateDataGridView(selectedEvent, ThoiGianChoi);
            }
        }

        private void GioiTinh_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (GioiTinh.SelectedItem != null)
            {
                string selectedEvent = GioiTinh.SelectedItem.ToString();
                UpdateDataGridView(selectedEvent, GioiTinh);
            }
        }

        private void MucDichCaiThien_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (MucDichCaiThien.SelectedItem != null)
            {
                string selectedEvent = MucDichCaiThien.SelectedItem.ToString();
                UpdateDataGridView(selectedEvent, MucDichCaiThien);
            }
        }

        private void DacDiemVot_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DacDiemVot.SelectedItem != null)
            {
                string selectedEvent = DacDiemVot.SelectedItem.ToString();
                UpdateDataGridView(selectedEvent, DacDiemVot);
            }
        }

        private void TrongLuongVot_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TrongLuongVot.SelectedItem != null)
            {
                string selectedEvent = TrongLuongVot.SelectedItem.ToString();
                UpdateDataGridView(selectedEvent, TrongLuongVot);
            }
        }

        private void ChatLuongVaDoBen_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ChatLuongVaDoBen.SelectedItem != null)
            {
                string selectedEvent = ChatLuongVaDoBen.SelectedItem.ToString();
                UpdateDataGridView(selectedEvent, ChatLuongVaDoBen);
            }
        }  
        private void HangVot_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (HangVot.SelectedItem != null)
            {
                string selectedEvent = HangVot.SelectedItem.ToString();
                UpdateDataGridView(selectedEvent, HangVot);
            }
        }

        

        private void SanPhamChon_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SanPhamChon.SelectedItem != null)
            {
                string selectedProduct = SanPhamChon.SelectedItem.ToString();

                dtgr_SanPhamPhuHop.Rows.Clear();
                dtgr_SanPhamPhuHop.Rows.Add(selectedProduct);
                // Truy vấn để lấy đường dẫn ảnh sản phẩm
                string sql = "SELECT AnhSanPham FROM [TT.VotYonex] WHERE TenSanPham = '" + selectedProduct.Replace("'", "''") + "'";

                DataSet ds = kn.Laydulieu(sql);

                if (ds != null && ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];

                    if (dt.Rows.Count > 0)
                    {
                        string imagePath = dt.Rows[0]["AnhSanPham"].ToString();

                        if (!string.IsNullOrEmpty(imagePath) && System.IO.File.Exists(imagePath))
                        {
                            // Hiển thị ảnh lên PictureBox
                            pictureBox_AnhSanPham.Image = System.Drawing.Image.FromFile(imagePath);
                        }
                        else
                        {
                            MessageBox.Show("Đường dẫn ảnh không hợp lệ hoặc không tồn tại.");
                            pictureBox_AnhSanPham.Image = null; // Xóa ảnh hiện tại nếu không có ảnh mới
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Không tìm thấy dữ liệu ảnh sản phẩm.");
                }
            
            }
        }

        private void dataGridView_DieuKienThem_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}

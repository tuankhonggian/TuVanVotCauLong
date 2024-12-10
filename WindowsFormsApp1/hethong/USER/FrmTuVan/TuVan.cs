using System;
using System.Windows.Forms;
using WindowsFormsApp1.hethong.trangchu;


namespace WindowsFormsApp1.hethong.FrmTuVan
{
    public partial class TuVan : Form
    {
        private URTrangChu parentForm;

        public TuVan(URTrangChu parent)
        {
            InitializeComponent();

            parentForm = parent;
        }

        // Khai báo biến lưu thời gian bắt đầu (nên khai báo ở cấp lớp nếu cần sử dụng lại sau này)
        private DateTime startTime;
        private void guna2Button_BatDau_Click(object sender, EventArgs e)
        {
            // Lưu thời gian khi click nút "Bắt đầu"
            startTime = DateTime.Now;
            // Ẩn form TuVan hiện tại
            this.Hide();

            // Tạo instance của FormTuVan và hiển thị nó trên panel HinhNen
            FormTuVan formTuVan1 = new FormTuVan()
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };
            formTuVan1.SetStartTime(startTime); // Truyền startTime

            // Hiển thị form FormTuVan trên panel HinhNen của URTrangChu
            parentForm.HinhNen.Controls.Clear();
            parentForm.HinhNen.Controls.Add(formTuVan1);
            formTuVan1.Show();


        }

        private void FrmTuVan_Load(object sender, EventArgs e)
        {

        }

        private void panel_Header_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
        private void Nen_Paint(object sender, PaintEventArgs e)
        {
        }
        private void FormTuVan1_DapAnDaChon(string dapAn)
        {
        }
        private void ThongTinTuVan_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        private void guna2Button_Back_Click(object sender, EventArgs e)
        {

        }
 
    }
}

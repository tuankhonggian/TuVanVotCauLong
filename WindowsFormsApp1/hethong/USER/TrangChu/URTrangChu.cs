using System;
using System.Drawing;
using System.Windows.Forms;
using WindowsFormsApp1.hethong.ADMIN.TrangChu;
using WindowsFormsApp1.hethong.dangnhap;

namespace WindowsFormsApp1.hethong.trangchu
{
    public partial class URTrangChu : Form
    {
        
        bool sidebarExpand;
        bool sanphamCollapsed;
        private bool isDragging = false;
        private Point startPoint = new Point(0, 0);



        public URTrangChu()
        {
            InitializeComponent();
            // Di chuyển header
            panel4.MouseDown += new MouseEventHandler(panel_Header_MouseDown);
            panel4.MouseMove += new MouseEventHandler(panel_Header_MouseMove);
            panel4.MouseUp += new MouseEventHandler(panel_Header_MouseUp);
            panel7.MouseDown += new MouseEventHandler(panel_Header_MouseDown);
            panel7.MouseMove += new MouseEventHandler(panel_Header_MouseMove);
            panel7.MouseUp += new MouseEventHandler(panel_Header_MouseUp);
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
        

        private void CapNhatTenTrang(string tenTrang)
        {
            TenTrang.Text = tenTrang;
        }
        private void button_TuVan_Click(object sender, EventArgs e)
        {

            // Cập nhật tên trang
            CapNhatTenTrang("Trang Tư Vấn Chọn Vợt");

            // Xóa các control hiện tại trong panel HinhNen
            HinhNen.Controls.Clear();

            // Tạo instance của TuVan và truyền tham chiếu của URTrangChu
            WindowsFormsApp1.hethong.FrmTuVan.TuVan tuvanForm = new WindowsFormsApp1.hethong.FrmTuVan.TuVan(this)
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };

            // Thêm form TuVan vào panel HinhNen và hiển thị nó
            HinhNen.Controls.Add(tuvanForm);
            tuvanForm.Show();

        }

        private void button_home_Click(object sender, EventArgs e)
        {
            // đặt khoảng thời gian hẹn giờ ở mức thấp nhất để làm cho nó mượt mà hơn
            SanPhamTimer.Start();
        }

        private void HinhNen_Paint(object sender, PaintEventArgs e)
        {

        }

        private void URTrangChu_Load(object sender, EventArgs e)
        {
            timerGio.Start();
        }

        private void timerGio_Tick(object sender, EventArgs e)
        {
            label_DongHo.Text = DateTime.Now.ToString("hh:mm:ss");
        }

        

        private void SanPhamTimer_Tick(object sender, EventArgs e)
        {
            if (sanphamCollapsed)
            {
                panel5.Height += 10;
                if (panel5.Height == panel5.MaximumSize.Height)
                {
                    sanphamCollapsed = false;
                    SanPhamTimer.Stop();
                }
            }
            else
            {
                panel5.Height -= 10;
                if (panel5.Height == panel5.MinimumSize.Height)
                {
                    sanphamCollapsed = true;
                    SanPhamTimer.Stop();
                }
            }
        }

        private void pictureBox1_Menu_Click_1(object sender, EventArgs e)
        {
            // đặt khoảng thời gian hẹn giờ ở mức thấp nhất để làm cho nó mượt mà hơn
            sidebarTimer.Start();
        }

        private void sidebarTimer_Tick_1(object sender, EventArgs e)
        {
            // //SET kích thước tối thiểu và tối đa của bảng điều khiển bên

            if (sidebarExpand)
            {
                // nếu thanh bên mở rộng, thu nhỏ

                sidebar.Width -= 10;
                if (sidebar.Width == sidebar.MinimumSize.Width)
                {
                    sidebarExpand = false;
                    sidebarTimer.Stop();
                }
            }
            else
            {
                sidebar.Width += 10;
                if (sidebar.Width == sidebar.MaximumSize.Width)
                {
                    sidebarExpand = true;
                    sidebarTimer.Stop();
                }
            }
        }

        private void button_Yonex_Click(object sender, EventArgs e)
        {
        }

        private void button_Victor_Click(object sender, EventArgs e)
        {
        }

        private void button_Lining_Click(object sender, EventArgs e)
        {
        }

        private void button_Kumpoo_Click(object sender, EventArgs e)
        {
        }

        private void button_Mizuno_Click(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
        }

        private void button_Felet_Click(object sender, EventArgs e)
        {
        }

        private void button_Flypower_Click(object sender, EventArgs e)
        {
        }

        private void button_Kawasaki_Click(object sender, EventArgs e)
        {
        }

        private void pictureBox_Close_Click(object sender, EventArgs e)
        {
            this.Hide();
            DangNhap dn = new DangNhap();
            dn.Show();
        }

        private void panel8_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button_Home_Click_1(object sender, EventArgs e)
        {
            CapNhatTenTrang("Trang Chủ");
            // Tạo instance của form ADtuvan
            TrangChu tuvanForm = new TrangChu();

            // Thiết lập ADtuvan như một form con của ADtrangchu
            tuvanForm.TopLevel = false;
            tuvanForm.FormBorderStyle = FormBorderStyle.None;
            tuvanForm.AutoScroll = true; // Bật tính năng cuộn nếu form lớn hơn FlowLayoutPanel

            // Hiển thị form ADtuvan trên FlowLayoutPanel
            HinhNen.Controls.Clear(); // Xóa các control hiện tại trong FlowLayoutPanel
            HinhNen.Controls.Add(tuvanForm); // Thêm form ADtuvan vào FlowLayoutPanel
            tuvanForm.Show(); // Hiển thị form ADtuvan
        }
    }
}

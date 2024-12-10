using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.hethong.ADMIN.SuKien;
using WindowsFormsApp1.hethong.ADMIN.TrangChu;
using WindowsFormsApp1.hethong.ADMIN.TuVan;
using WindowsFormsApp1.hethong.dangnhap;
using WindowsFormsApp1.hethong.sanpham;
using WindowsFormsApp1.hethong.TuVan;

namespace WindowsFormsApp1.hethong.trangchu
{
    public partial class ADtrangchu : Form
    {
        bool sidebarExpand;
        bool sanphamCollapsed;
        private bool isDragging = false;
        private Point startPoint = new Point(0, 0);

        public ADtrangchu()
        {
            InitializeComponent();
            // Di chuyển header
            panel4.MouseDown += new MouseEventHandler(panel_Header_MouseDown);
            panel4.MouseMove += new MouseEventHandler(panel_Header_MouseMove);
            panel4.MouseUp += new MouseEventHandler(panel_Header_MouseUp);
            
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

        private void sidebarTimer_Tick(object sender, EventArgs e)
        {
            // //SET kích thước tối thiểu và tối đa của bảng điều khiển bên
           
            if (sidebarExpand) {
                // nếu thanh bên mở rộng, thu nhỏ

                sidebar.Width -= 10;
                if (sidebar.Width == sidebar.MinimumSize.Width) { 
                    sidebarExpand = false;
                    sidebarTimer .Stop();
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

        private void pictureBox1_Menu_Click(object sender, EventArgs e)
        {
            // đặt khoảng thời gian hẹn giờ ở mức thấp nhất để làm cho nó mượt mà hơn
            sidebarTimer.Start();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void SanPhamTimer_Tick(object sender, EventArgs e)
        {
            if (sanphamCollapsed) {
                panel5.Height += 10;
                if(panel5.Height == panel5.MaximumSize.Height)
                {
                   sanphamCollapsed = false;
                   SanPhamTimer.Stop();
                }
            }
            else
            {
                panel5.Height -= 10;
                if(panel5.Height ==  panel5.MinimumSize.Height)
                {
                    sanphamCollapsed = true;
                    SanPhamTimer.Stop();
                }
            }

        }

        private void button_home_Click(object sender, EventArgs e)
        {
            // đặt khoảng thời gian hẹn giờ ở mức thấp nhất để làm cho nó mượt mà hơn
            SanPhamTimer.Start();
        }

        private void ADtrangchu_Load(object sender, EventArgs e)
        {
            timerGio.Start();
        }

        private void TenTrang_Click(object sender, EventArgs e)
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

        private void timerGio_Tick(object sender, EventArgs e)
        {
            label_DongHo.Text = DateTime.Now.ToString("hh:mm:ss");
        }

        private void button_TuVan_Click(object sender, EventArgs e)
        {
            CapNhatTenTrang("Trang Tư Vấn");
            // Tạo instance của form ADtuvan
            DSLuat tuvanForm = new DSLuat();

            // Thiết lập ADtuvan như một form con của ADtrangchu
            tuvanForm.TopLevel = false;
            tuvanForm.FormBorderStyle = FormBorderStyle.None;
            tuvanForm.AutoScroll = true; // Bật tính năng cuộn nếu form lớn hơn FlowLayoutPanel

            // Hiển thị form ADtuvan trên FlowLayoutPanel
            HinhNen.Controls.Clear(); // Xóa các control hiện tại trong FlowLayoutPanel
            HinhNen.Controls.Add(tuvanForm); // Thêm form ADtuvan vào FlowLayoutPanel
            tuvanForm.Show(); // Hiển thị form ADtuvan
        }
        // Hiển thị trang danh sách sản phẩm Vợt Yonex
        private void button_Yonex_Click(object sender, EventArgs e)
        {
            CapNhatTenTrang("Vợt Yonex");
            // Tạo instance của form ADtuvan
            VotYonex yonexForm = new VotYonex();

            // Thiết lập ADtuvan như một form con của ADtrangchu
            yonexForm.TopLevel = false;
            yonexForm.FormBorderStyle = FormBorderStyle.None;
            yonexForm.AutoScroll = true; // Bật tính năng cuộn nếu form lớn hơn FlowLayoutPanel

            // Hiển thị form ADtuvan trên FlowLayoutPanel
            HinhNen.Controls.Clear(); // Xóa các control hiện tại trong FlowLayoutPanel
            HinhNen.Controls.Add(yonexForm); // Thêm form ADtuvan vào FlowLayoutPanel
            yonexForm.Show(); // Hiển thị form ADtuvan
        }

        private void sideTieuDe_Paint(object sender, PaintEventArgs e)
        {

        }
        // Hiển thị trang danh sách sản phẩm Vợt Victor
        private void button_Victor_Click(object sender, EventArgs e)
        {
            CapNhatTenTrang("Vợt Victor");
            // Tạo instance của form ADtuvan
            VotVictor yonexForm = new VotVictor();

            // Thiết lập ADtuvan như một form con của ADtrangchu
            yonexForm.TopLevel = false;
            yonexForm.FormBorderStyle = FormBorderStyle.None;
            yonexForm.AutoScroll = true; // Bật tính năng cuộn nếu form lớn hơn FlowLayoutPanel

            // Hiển thị form ADtuvan trên FlowLayoutPanel
            HinhNen.Controls.Clear(); // Xóa các control hiện tại trong FlowLayoutPanel
            HinhNen.Controls.Add(yonexForm); // Thêm form ADtuvan vào FlowLayoutPanel
            yonexForm.Show(); // Hiển thị form ADtuvan
        }

        private void pictureBox_Close_Click(object sender, EventArgs e)
        {
            this.Hide();
            DangNhap dn = new DangNhap();
            dn.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CapNhatTenTrang("Sự Kiện");
            // Tạo instance của form ADtuvan
            DSsukien yonexForm = new DSsukien();

            // Thiết lập ADtuvan như một form con của ADtrangchu
            yonexForm.TopLevel = false;
            yonexForm.FormBorderStyle = FormBorderStyle.None;
            yonexForm.AutoScroll = true; // Bật tính năng cuộn nếu form lớn hơn FlowLayoutPanel

            // Hiển thị form ADtuvan trên FlowLayoutPanel
            HinhNen.Controls.Clear(); // Xóa các control hiện tại trong FlowLayoutPanel
            HinhNen.Controls.Add(yonexForm); // Thêm form ADtuvan vào FlowLayoutPanel
            yonexForm.Show(); // Hiển thị form ADtuvan
        }
    }
}

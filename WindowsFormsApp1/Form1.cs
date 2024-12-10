using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using WindowsFormsApp1.hethong.dangnhap;
using WindowsFormsApp1.hethong.trangchu;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }
        int startpoint = 0;

        private void timer1_Tick(object sender, EventArgs e)
        {
            //timer1.Stop(); // Dừng Timer sau khi hoàn thành
            //guna2ProgressBar1.Visible = false; // Ẩn ProgressBar
            //// Khởi tạo và hiển thị form trang chủ
            //DangNhap mainForm = new DangNhap();
            //mainForm.Show();

            //// Đóng form hiện tại (form đăng nhập)
            //this.Hide(); // Hoặc dùng this.Close(); nếu bạn muốn đóng hoàn toàn

            startpoint += 1;
            guna2ProgressBar1.Value = startpoint;
            if(guna2ProgressBar1.Value == 100)
            {
                guna2ProgressBar1.Value = 0;
                timer1.Stop();
                DangNhap log = new DangNhap();
                this.Hide();    
                log.Show();
            }


        }

        

        private void guna2ProgressBar1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}

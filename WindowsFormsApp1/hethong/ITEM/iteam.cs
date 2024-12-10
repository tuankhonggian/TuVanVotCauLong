using System;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp1.hethong.ITEAM
{
    public partial class iteam : Form
    {
        int toastX, toastY;
        private string messageType;
        private Timer autoCloseTimer;
        private int displayDuration = 3000; // Thời gian hiển thị thông báo (ms)

        public iteam(string message, string type)
        {
            InitializeComponent();
            label_Messsage.Text = message; // Thiết lập thông điệp
            messageType = type; // Thiết lập loại thông báo
            SetAppearance();
            InitializeAutoCloseTimer();
        }

        private void iteam_Load(object sender, EventArgs e)
        {
            Possition();
        }

        private void SetAppearance()
        {
            if (messageType == "Success")
            {
                label_Type.Text = "Thành công";
                this.BackColor = Color.White; // Màu nền cho thông báo thành công
                pictureBox_Icon.Image = Properties.Resources.icons8_check_50; // Đặt ảnh thành công
                panel_Border.BackColor = Color.LimeGreen; // Màu viền cho thông báo thành công
            }
            else if (messageType == "Error")
            {
                label_Type.Text = "Lỗi";
                this.BackColor = Color.White; // Màu nền cho thông báo lỗi
                pictureBox_Icon.Image = Properties.Resources.error; // Đặt ảnh lỗi
                panel_Border.BackColor = Color.Red; // Màu viền cho thông báo lỗi
            }
        }

        private void InitializeAutoCloseTimer()
        {
            autoCloseTimer = new Timer();
            autoCloseTimer.Interval = displayDuration; // Thời gian tự động đóng sau displayDuration
            autoCloseTimer.Tick += AutoCloseTimer_Tick;
            autoCloseTimer.Start();
        }

        private void AutoCloseTimer_Tick(object sender, EventArgs e)
        {
            autoCloseTimer.Stop();
            this.Close(); // Đóng form sau khi hết thời gian
        }

        private void Possition()
        {
            int screenWidth = Screen.PrimaryScreen.WorkingArea.Width;
            int screenHeight = Screen.PrimaryScreen.WorkingArea.Height;

            toastX = screenWidth - this.Width - 10; // Cách mép màn hình 10px
            toastY = screenHeight - this.Height - 10; // Cách mép màn hình 10px

            this.Location = new Point(toastX, toastY);
        }
    }
}

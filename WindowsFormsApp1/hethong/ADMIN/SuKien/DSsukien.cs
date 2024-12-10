using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.hethong.sanpham;

namespace WindowsFormsApp1.hethong.ADMIN.SuKien
{
    public partial class DSsukien : Form
    {
        public DSsukien()
        {
            InitializeComponent();
        }

        private void guna2Button_MucDich_Click(object sender, EventArgs e)
        {
            MucDich mucdichForm = new MucDich();

            mucdichForm.TopLevel = false;
            mucdichForm.FormBorderStyle = FormBorderStyle.None;
            mucdichForm.AutoScroll = true; 
            HinhNen.Controls.Clear(); 
            HinhNen.Controls.Add(mucdichForm);
            mucdichForm.Show();
        }

        private void guna2Button_TrinhDo_Click(object sender, EventArgs e)
        {
            TrinhDo mucdichForm = new TrinhDo();

            mucdichForm.TopLevel = false;
            mucdichForm.FormBorderStyle = FormBorderStyle.None;
            mucdichForm.AutoScroll = true;
            HinhNen.Controls.Clear();
            HinhNen.Controls.Add(mucdichForm);
            mucdichForm.Show();
        }

        private void guna2Button_DoCung_Click(object sender, EventArgs e)
        {
            DoCung mucdichForm = new DoCung();

            mucdichForm.TopLevel = false;
            mucdichForm.FormBorderStyle = FormBorderStyle.None;
            mucdichForm.AutoScroll = true;
            HinhNen.Controls.Clear();
            HinhNen.Controls.Add(mucdichForm);
            mucdichForm.Show();
        }

        private void guna2Button_SucCang_Click(object sender, EventArgs e)
        {
            SucCang mucdichForm = new SucCang();

            mucdichForm.TopLevel = false;
            mucdichForm.FormBorderStyle = FormBorderStyle.None;
            mucdichForm.AutoScroll = true;
            HinhNen.Controls.Clear();
            HinhNen.Controls.Add(mucdichForm);
            mucdichForm.Show();
        }

        private void guna2Button_TrongLuong_Click(object sender, EventArgs e)
        {
            TrongLuong mucdichForm = new TrongLuong();

            mucdichForm.TopLevel = false;
            mucdichForm.FormBorderStyle = FormBorderStyle.None;
            mucdichForm.AutoScroll = true;
            HinhNen.Controls.Clear();
            HinhNen.Controls.Add(mucdichForm);
            mucdichForm.Show();
        }

        private void guna2Button_ChuVi_Click(object sender, EventArgs e)
        {
            ChuVi mucdichForm = new ChuVi();

            mucdichForm.TopLevel = false;
            mucdichForm.FormBorderStyle = FormBorderStyle.None;
            mucdichForm.AutoScroll = true;
            HinhNen.Controls.Clear();
            HinhNen.Controls.Add(mucdichForm);
            mucdichForm.Show();
        }

        private void guna2Button_ChatLieu_Click(object sender, EventArgs e)
        {
            ChatLieu mucdichForm = new ChatLieu();

            mucdichForm.TopLevel = false;
            mucdichForm.FormBorderStyle = FormBorderStyle.None;
            mucdichForm.AutoScroll = true;
            HinhNen.Controls.Clear();
            HinhNen.Controls.Add(mucdichForm);
            mucdichForm.Show();
        }

        private void guna2Button_ChieuDai_Click(object sender, EventArgs e)
        {
            ChieuDai mucdichForm = new ChieuDai();

            mucdichForm.TopLevel = false;
            mucdichForm.FormBorderStyle = FormBorderStyle.None;
            mucdichForm.AutoScroll = true;
            HinhNen.Controls.Clear();
            HinhNen.Controls.Add(mucdichForm);
            mucdichForm.Show();
        }

        private void guna2Button_MatVot_Click(object sender, EventArgs e)
        {
            MatVot mucdichForm = new MatVot();

            mucdichForm.TopLevel = false;
            mucdichForm.FormBorderStyle = FormBorderStyle.None;
            mucdichForm.AutoScroll = true;
            HinhNen.Controls.Clear();
            HinhNen.Controls.Add(mucdichForm);
            mucdichForm.Show();
        }

        private void guna2Button_TaiChinh_Click(object sender, EventArgs e)
        {
            TaiChinh mucdichForm = new TaiChinh();

            mucdichForm.TopLevel = false;
            mucdichForm.FormBorderStyle = FormBorderStyle.None;
            mucdichForm.AutoScroll = true;
            HinhNen.Controls.Clear();
            HinhNen.Controls.Add(mucdichForm);
            mucdichForm.Show();
        }

        private void guna2Button_TrongLuongVung_Click(object sender, EventArgs e)
        {
            TrongLuongVung mucdichForm = new TrongLuongVung();

            mucdichForm.TopLevel = false;
            mucdichForm.FormBorderStyle = FormBorderStyle.None;
            mucdichForm.AutoScroll = true;
            HinhNen.Controls.Clear();
            HinhNen.Controls.Add(mucdichForm);
            mucdichForm.Show();
        }

        private void guna2Button_PhanLoaiVot_Click(object sender, EventArgs e)
        {
            PhanLoaiVot mucdichForm = new PhanLoaiVot();

            mucdichForm.TopLevel = false;
            mucdichForm.FormBorderStyle = FormBorderStyle.None;
            mucdichForm.AutoScroll = true;
            HinhNen.Controls.Clear();
            HinhNen.Controls.Add(mucdichForm);
            mucdichForm.Show();
        }

        private void guna2Button_PhanLoaiNguoi_Click(object sender, EventArgs e)
        {
            PhanLoaiNguoi mucdichForm = new PhanLoaiNguoi();

            mucdichForm.TopLevel = false;
            mucdichForm.FormBorderStyle = FormBorderStyle.None;
            mucdichForm.AutoScroll = true;
            HinhNen.Controls.Clear();
            HinhNen.Controls.Add(mucdichForm);
            mucdichForm.Show();
        }
    }
}

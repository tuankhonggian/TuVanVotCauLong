using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using WindowsFormsApp1.classes;
using WindowsFormsApp1.ketnoi.dangnhap;

namespace WindowsFormsApp1.hethong
{
    public class ThuatToanSuyDienTien
    {
        private ketnoiDN conn = new ketnoiDN();
        List<Rule_Define> luuTru = new List<Rule_Define>();
        List<Rule_Define> SAT = new List<Rule_Define>();

        public void DocLuatTuFile()
        {
            //lấy cột nội dung của bảng TapLuat
            string temp = "SELECT NoiDung FROM [TapLuat]"; //Lấy dữ liệu
            DataTable tbLuat = conn.getTable(temp);
            //lấy tập luật ra thực hiện tách, vế trái, vế phải và các sự kiện bên trong vế trái
            for (int i = 0; i < tbLuat.Rows.Count; i++) // duyệt từng dòng tập luật
            {
                //lấy ra tập luật i
                string buff = tbLuat.Rows[i][0].ToString();
                Rule_Define luatTG = new Rule_Define();
                char[] tachChuoi = { '>' };
                string[] tg = buff.Split(tachChuoi); //tách chuỗi khi gặp ký tự '>'

                //vế trái: tách các sự kiện trong vế trái
                char[] tachChuoi1 = { '^' };
                string[] veTrai = tg[0].Split(tachChuoi1); //tách chuỗi khi gặp ký tự  '^'
                int j = 0;
                string buff1 = veTrai[0];
                while (buff1 != null)
                {
                    //thên luật vào vế trai luatTG
                    luatTG.VeTrai.Add(buff1);
                    j++;
                    try
                    {
                        buff1 = veTrai[j];
                    }
                    catch { buff1 = null; };

                }
                j = 0;

                //Vế phải; đẩy vế trái vào vế phải
                char[] tachChuoi2 = { ',' };
                string[] vePhai = tg[1].Split(tachChuoi2); //tách chuỗi khi gặp ký tự  ','
                buff1 = vePhai[0];
                while (buff1 != null)
                {
                    luatTG.VePhai.Add(buff1); //thêm luật vào vế phải luatTG ; chứa kết luận của luat
                    j++;
                    try
                    {
                        buff1 = vePhai[j];
                    }
                    catch
                    {

                        buff1 = null;
                    };
                }

                luuTru.Add(luatTG); // add LuatTG vừa tách được vào tapluat được xử lý LuuTru

            }

        }
        //Kiểm tra xem tập sự kiện nhập vào có tồn tại trong vế trái của luật không
        public bool KiemTra(List<string> a, List<string> b) //a : ve trai cua luat; b: tap su kien dau vao
        {
            //Kiểm tra nếu tất cả sự kiện nhập vào trùng với vế trái trong tập luật -> true 
            int dem = 0;
            foreach (string tg1 in a)
            {
                foreach (string tg2 in b)
                {
                    if (tg1 == tg2)
                    {
                        dem++;
                    }
                }
            }
            //số lượng sự kiện KL trùng với toàn bộ số lượng SK nhập vào
            if (dem == a.Count)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //tìm tập SAT những sự kiện hiện có - truyền vào vế trái và tập luật gốc
        public void TimTapSAT(List<string> L, List<Rule_Define> tapluat) //
        {
            foreach (Rule_Define lTG in tapluat)
            {
                //kiểm tra từng luật xem có thỏa mãn với sự kiện nhập vào ban đầu không - có thì add vào tập SAT
                if (KiemTra(lTG.VeTrai, L) == true && !SAT.Contains(lTG))
                {
                    SAT.Add(lTG); //them vao tap SAT
                }
            }
        }
        //hàm suy diễn tiến. Vế trái là tập sự kiện nhập vào, vế phải là danh sách mã trường(tập sự kiện kết luận)
        public bool SuyDienMain(List<string> veTrai, List<string> vePhai)
        {
            List<Rule_Define> tapluat = new List<Rule_Define>();
            //tapluat là danh sách tập luật gốc
            tapluat = luuTru;
            List<string> KL = vePhai; //ma truong
            List<string> TG = veTrai;  //su kiện nhập vào

            //kiểm tra tập SAT, nếu luật nào thỏa mãn được thêm vào tập SAT
            TimTapSAT(TG, tapluat); //có mã ngành

            //lay ra tất cả mã trường thỏa mãn điều kiện nhập vào
            while (SAT.Count > 0 && KiemTra(KL, TG) == false)
            {
                //đọc lần lượt các SAT cho đến cái luật cuối cùng
                //lay luat r cuoi cung ra ap dung
                Rule_Define r = SAT.ElementAt(0);
                tapluat.Remove(r);  //xoa cai vua lay ra dung
                SAT.RemoveAt(0); //xoa ra khoi tap SAT

                //nếu vế phải của SAT chưa có trong tập luật thỏa mãn thì sẽ được thêm vào
                //them cai chua co vao tap TG
                foreach (string tg in r.VePhai) //tg: ma truong
                {
                    if (!TG.Contains(tg))
                    {
                        TG.Add(tg); //them vao TG ban đầu
                    }
                }
            }
            //kiểm tra xem KL(Mã trường) có là một trong những mà trường thoả mã được thêm vào TG hay không
            if (KiemTra(KL, TG) == false)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        //hàm suy diễn tiến. Vế trái là tập sự kiện nhập vào, vế phải là danh sách phân loại vợt(tập sự kiện kết luận)
        public bool SuyDienVot(List<string> veTrai, List<string> vePhai)
        {
            List<Rule_Define> tapluat = new List<Rule_Define>();
            //tapluat là danh sách tập luật gốc
            tapluat = luuTru;
            List<string> KL = vePhai; //ma truong
            List<string> TG = veTrai;  //su kiện nhập vào

            //kiểm tra tập SAT, nếu luật nào thỏa mãn được thêm vào tập SAT
            TimTapSAT(TG, tapluat); //có mã ngành

            //Lấy ra tất cả mã phân loại vợt thỏa mãn điều kiện nhập vào
            while (SAT.Count > 0 && KiemTra(KL, TG) == false)
            {
                //đọc lần lượt các SAT cho đến cái luật cuối cùng
                //lay luat r cuoi cung ra ap dung
                Rule_Define r = SAT.ElementAt(0);
                tapluat.Remove(r);  //xoa cai vua lay ra dung
                SAT.RemoveAt(0); //xoa ra khoi tap SAT

                //nếu vế phải của SAT chưa có trong tập luật thỏa mãn thì sẽ được thêm vào
                //them cai chua co vao tap TG
                foreach (string tg in r.VePhai) //tg: ma phân loại vợt
                {
                    if (!TG.Contains(tg))
                    {
                        TG.Add(tg); //them vao TG ban đầu
                    }
                }
            }
            //kiểm tra xem KL(Mã trường) có là một trong những mà trường thoả mã được thêm vào TG hay không
            if (KiemTra(KL, TG) == false)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        //hàm suy diễn tiến. Vế trái là tập sự kiện nhập vào, vế phải là danh sách phân loại vợt(tập sự kiện kết luận)
        public bool SuyDienNguoi(List<string> veTrai, List<string> vePhai)
        {
            List<Rule_Define> tapluat = new List<Rule_Define>();
            //tapluat là danh sách tập luật gốc
            tapluat = luuTru;
            List<string> KL = vePhai; //ma truong
            List<string> TG = veTrai;  //su kiện nhập vào

            //kiểm tra tập SAT, nếu luật nào thỏa mãn được thêm vào tập SAT
            TimTapSAT(TG, tapluat); //có mã ngành

            //Lấy ra tất cả mã phân loại vợt thỏa mãn điều kiện nhập vào
            while (SAT.Count > 0 && KiemTra(KL, TG) == false)
            {
                //đọc lần lượt các SAT cho đến cái luật cuối cùng
                //lay luat r cuoi cung ra ap dung
                Rule_Define r = SAT.ElementAt(0);
                tapluat.Remove(r);  //xoa cai vua lay ra dung
                SAT.RemoveAt(0); //xoa ra khoi tap SAT

                //nếu vế phải của SAT chưa có trong tập luật thỏa mãn thì sẽ được thêm vào
                //them cai chua co vao tap TG
                foreach (string tg in r.VePhai) //tg: ma phân loại vợt
                {
                    if (!TG.Contains(tg))
                    {
                        TG.Add(tg); //them vao TG ban đầu
                    }
                }
            }
            //kiểm tra xem KL(Mã trường) có là một trong những mà trường thoả mã được thêm vào TG hay không
            if (KiemTra(KL, TG) == false)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}

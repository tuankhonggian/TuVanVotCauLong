using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.hethong.ITEAM;
using WindowsFormsApp1.ketnoi.dangnhap;
namespace WindowsFormsApp1.hethong.FrmTuVan
{
    public partial class FormTuVan : Form
    {
        private ketnoiDN ketnoi = new ketnoiDN();
        private int currentQuestionIndex = 0; // Biến để theo dõi câu hỏi hiện tại
        private List<string> ListCauHoi = new List<string>();
        private Guna.UI2.WinForms.Guna2Button[] answerButtons;

        // Khai báo biến lưu đáp án của các câu hỏi
        private string mucdich;
        private string trinhdo;
        private string doCung;
        private string sucCang;
        private string trongLuong;
        private string chuViCan;
        private string chatLuong;
        private string chieuDai;
        private string trongLuongVung;
        private string dangMatVot;
        private string taiChinh;
        private string hang;
        private string ketqua;
        private string idketqua;
        private List<string> dapAnDaChon;  // Lưu các đáp án đã chọn


        public FormTuVan()
        {
            InitializeComponent();
            // Ẩn nút "Xem Kết Quả" ngay từ đầu
            guna2Button_XemKetQua.Visible = false;
            // Ẩn progressBar1 ngay từ đầu
            progressBar1.Visible = false;
            // Khởi tạo mảng nút đáp án
            answerButtons = new Guna.UI2.WinForms.Guna2Button[]
        {
            guna2Button_DapAn1,guna2Button_DapAn2,guna2Button_DapAn3,guna2Button_DapAn4,guna2Button_DapAn5,guna2Button_DapAn6,
            guna2Button_DapAn7,guna2Button_DapAn8,guna2Button_DapAn9,guna2Button_DapAn10,guna2Button_DapAn11,guna2Button_DapAn12
        }; 
        }
        // load
        private void FormTuVan_Load(object sender, EventArgs e)
        {
            string tempV = "SELECT SanPhamID, TenSanPham FROM [Vot]";
            DataTable tbVot = ketnoi.getTable(tempV);
            for (int i = 0; i < tbVot.Rows.Count; i++)
            {
                ListMaVot.Add(tbVot.Rows[i][0].ToString());
                ListTenVot.Add(tbVot.Rows[i][1].ToString());
            }
            string tempPLV = "SELECT PhanLoaiVotID, NDphanloaivot FROM [PHANLOAIVOT]";
            DataTable tbPLV = ketnoi.getTable(tempPLV);
            for (int i = 0; i < tbPLV.Rows.Count; i++)
            {
                Listplv.Add(tbPLV.Rows[i][0].ToString());
                ListTenPLV.Add(tbPLV.Rows[i][1].ToString());
            }
            string tempPLN = "SELECT PhanLoaiNguoiID, NDphanloainguoi FROM [PHANLOAINGUOI]";
            DataTable tbPLN = ketnoi.getTable(tempPLN);
            for (int i = 0; i < tbPLN.Rows.Count; i++)
            {
                Listpln.Add(tbPLN.Rows[i][0].ToString());
                ListTenPLN.Add(tbPLN.Rows[i][1].ToString());
            }
            // Tạo danh sách câu hỏi        
            CreateQuestionList();
            // Hiển thị câu hỏi đầu tiên
            DisplayCurrentQuestion();
            // Gán sự kiện cho các nút đáp án
            foreach (var button in answerButtons)
            {
                button.Click += AnswerButton_Click;
            }
        }
        //xử lý câu hỏi
        private void DisplayCurrentQuestion()
        {
            if (currentQuestionIndex < ListCauHoi.Count)
            {
                label_CauHoi.Text = ListCauHoi[currentQuestionIndex];
                LoadButtonsForCurrentQuestion();
                guna2Button_XemKetQua.Visible = false; // Ẩn nút khi vẫn còn câu hỏi
            }
            else
            {
                // Hiện nút "Xem Kết Quả" khi đã hoàn thành tất cả các câu hỏi
                guna2Button_XemKetQua.Visible = true;
                label_CauHoi.Visible = false;
                guna2Button_DapAn1.Visible = false;
                guna2Button_DapAn2.Visible = false;
                guna2Button_DapAn3.Visible = false;
                guna2Button_DapAn4.Visible = false;
                guna2Button_DapAn5.Visible = false;
                guna2Button_DapAn6.Visible = false;
                guna2Button_DapAn7.Visible = false;
                guna2Button_DapAn8.Visible = false;
                guna2Button_DapAn9.Visible = false;
                guna2Button_DapAn10.Visible = false;
                guna2Button_DapAn11.Visible = false;
                guna2Button_DapAn12.Visible = false;
                // Tạo một instance của form iteam với thông điệp hoàn thành
                iteam thongBaoHoanThanh = new iteam("Bạn đã hoàn thành tất cả các câu hỏi.", "Success");
                thongBaoHoanThanh.Show();
            }
        }
        private void LoadButtonsForCurrentQuestion()
        {
            string query = GetQueryForCurrentQuestion();
            DataTable dataTable = ketnoi.getTable(query);
            LoadButtonsFromDataTable(dataTable);
        }
        // tạo đáp án lên button
        private string GetQueryForCurrentQuestion()
        {
            string query = string.Empty;

            switch (currentQuestionIndex)
            {
                case 0:
                    query = "SELECT MucDichID, NDmucdich FROM [MUCDICH]";
                    break;
                case 1:
                    query = "SELECT TrinhDoID, NDtrinhdo FROM [TRINHDO]";
                    break;
                case 2:
                    query = "SELECT DoCungID, NDdocung FROM [DOCUNG]";
                    break;
                case 3:
                    query = "SELECT SucCangID, NDsuccang FROM [SUCCANG]";
                    break;
                case 4:
                    query = "SELECT TrongLuongID, NDtrongluong FROM [TRONGLUONG]";
                    break;
                case 5:
                    query = "SELECT ChuViID, NDchuvi FROM [CHUVI]";
                    break;
                case 6:
                    query = "SELECT ChatLieuID, NDchatlieu FROM [CHATLIEU]";
                    break;
                case 7:
                    query = "SELECT ChieuDaiID, NDchieudai FROM [CHIEUDAI]";
                    break;
                case 8:
                    query = "SELECT TrongLuongVungID, NDtrongluongvung FROM [TRONGLUONGVUNG]";
                    break;
                case 9:
                    query = "SELECT MatVotID, NDmatvot FROM [MATVOT]";
                    break;
                case 10:
                    query = "SELECT TaiChinhID, NDtaichinh FROM [TAICHINH]";
                    break;
                case 11:
                    query = "SELECT HangVotID, NDhangvot FROM [HANGVOT]";
                    break;
                default:
                    break;
            }

            return query;
        }

        //lấy thông tin đáp án từ cơ sở dữ liệu
        private void LoadButtonsFromDataTable(DataTable dataTable)
        {
            // Ẩn tất cả các button trước khi gán
            foreach (var button in answerButtons)
            {
                button.Visible = false;
            }

            // Gán dữ liệu vào các button
            for (int i = 0; i < dataTable.Rows.Count && i < answerButtons.Length; i++)
            {
                // Gán giá trị cho button
                // Điều này giả sử bạn đang lấy cột mô tả là "NDmucdich", "NDtrinhdo", ... (tùy theo bảng)
                answerButtons[i].Text = dataTable.Rows[i][1].ToString();  // Cột mô tả như NDmucdich, NDtrinhdo, ...
                answerButtons[i].Tag = dataTable.Rows[i][0].ToString();   // Cột ID như MucDichID, TrinhDoID, ...
                answerButtons[i].Visible = true; // Hiện button nếu có dữ liệu
            }
        }

        // gắn id cho đáp án
        private void AnswerButton_Click(object sender, EventArgs e)
        {
            // Lấy nút đáp án đã được nhấn
            Guna.UI2.WinForms.Guna2Button clickedButton = sender as Guna.UI2.WinForms.Guna2Button;
            // Kiểm tra nút có phải là null không
            if (clickedButton != null)
            {
                string selectedID = clickedButton.Tag.ToString(); // Lấy ID_SuKien từ Tag
                // Lưu đáp án dựa trên chỉ số câu hỏi hiện tại
                switch (currentQuestionIndex)
                {
                    case 0:
                        mucdich = selectedID; // hoặc lưu vào danh sách nếu cần
                        break;
                    case 1:
                        trinhdo = selectedID;
                        break;
                    case 2:
                        doCung = selectedID;
                        break;
                    case 3:
                        sucCang = selectedID;
                        break;
                    case 4:
                        trongLuong = selectedID;
                        break;
                    case 5:
                        chuViCan = selectedID;
                        break;
                    case 6:
                        chatLuong = selectedID;
                        break;
                    case 7:
                        chieuDai = selectedID;
                        break;
                    case 8:
                        trongLuongVung = selectedID;
                        break;
                    case 9:
                        dangMatVot = selectedID;
                        break;
                    case 10:
                        taiChinh = selectedID;
                        break;
                    case 11:
                        hang = selectedID;
                        break;
                    default:
                        break;
                }
            }
            // Tăng chỉ số câu hỏi lên
            currentQuestionIndex++;

            // Hiển thị câu hỏi tiếp theo
            DisplayCurrentQuestion();
        }
        // Phương thức tạo danh sách câu hỏi
        private void CreateQuestionList()
        {
            ListCauHoi.AddRange(new string[]
            {
                "Câu hỏi 1: Bạn chọn vợt với mục đích gì?",
                "Câu hỏi 2: Bạn thuộc trình độ nào?",
                "Câu hỏi 3: Bạn thích vợt có độ cứng như nào?",
                "Câu hỏi 4: Bạn thích vợt có sức căng như nào?",
                "Câu hỏi 5: Bạn muốn vợt có trọng lượng như nào?",
                "Câu hỏi 6: Bạn muốn vợt có chu vi cán vợt (kích thước tay cầm) như nào?",
                "Câu hỏi 7: Bạn muốn vợt có chất lượng và độ bền như nào?",
                "Câu hỏi 8: Bạn muốn vợt có chiều dài như nào?",
                "Câu hỏi 9: Bạn muốn vợt có trọng lượng vung như nào?",
                "Câu hỏi 10: Bạn muốn vợt có dạng mặt vợt như nào?",
                "Câu hỏi 11: Bạn muốn vợt trong tài chính bao nhiêu?",
                "Câu hỏi 12: Bạn muốn vợt hãng nào?"
            });
        }

        private List<string> ListMaVot = new List<string>();
        private List<string> Listplv = new List<string>();
        private List<string> ListTenPLV = new List<string>();
        private List<string> Listpln = new List<string>();
        private List<string> ListTenPLN = new List<string>();
        private List<string> ListTenVot = new List<string>();
        private bool run = false;
        private DateTime endTime;
        private DateTime startTime;
        private string plvot;
        private string NDplvot;
        private string plnguoi;
        private string NDplnguoi;

        // Phương thức để nhận startTime
        public void SetStartTime(DateTime startTime)
        {
            this.startTime = startTime;
        }

        // thực hiện suy diễn tiến và tiến đến trang kết quả
        private async void guna2Button_XemKetQua_Click(object sender, EventArgs e)
        {
            // Lưu thời gian khi click nút "Bắt đầu"
            endTime = DateTime.Now;
            ThuatToanSuyDienTien ttSuyDienTien = new ThuatToanSuyDienTien();
            ttSuyDienTien.DocLuatTuFile(); // Đọc luật từ file
            run = true;
            List<string> GiaThiet = new List<string>
    {
                mucdich, trinhdo, doCung, sucCang,trongLuong, chuViCan, chatLuong, 
                chieuDai, trongLuongVung, dangMatVot, taiChinh, hang
    };
            int d = 0;

            // Kiểm tra số lượng vợt trước khi thiết lập giá trị cho progressBar
            if (ListMaVot.Count > 0)
            {
                progressBar1.Maximum = ListMaVot.Count - 1;
                progressBar1.Minimum = 0;
                int dem = 0;
                ketqua = ""; // Khởi tạo lại biến ketqua mỗi lần gọi
                idketqua = "";

                foreach (string s in ListMaVot)
                {
                    if (!run)
                    {
                        guna2Button_Submit.Enabled = false;
                        break;
                    }

                    // Cập nhật giá trị progress bar
                    progressBar1.Value = dem;

                    // Thêm mã trường vào KetLuan cho mỗi vòng lặp
                    List<string> KetLuan = new List<string> { s };

                    // Thực hiện suy diễn tiến trong một tác vụ nền
                    if (await Task.Run(() => ttSuyDienTien.SuyDienMain(GiaThiet, KetLuan)))
                    {
                        // Nếu suy diễn thành công, cập nhật kết quả
                        ketqua += ListTenVot.ElementAt(dem) + "\n";
                        idketqua += ListMaVot.ElementAt(dem) + "\n";
                        d++;
                    }

                    dem++;
                    await Task.Delay(100); // Tạm dừng để cập nhật giao diện
                }

                // Nếu không có kết quả, thông báo cho người dùng
                if (d == 0 && run != false)
                {
                    ketqua = "Không có vợt phù hợp với lựa chọn của bạn!!!" +
                        "\nVui lòng chọn lại." +
                        "\nChúng tôi sẽ cập nhật thêm thông tin trong thời gian tới.";
                }
            }
            else
            {
                MessageBox.Show("Danh sách mã vợt trống! Không thể hiển thị kết quả.");
            }
            // Kiểm tra số phan loai vợt trước khi thiết lập giá trị cho progressBar
            if (Listplv.Count > 0)
            {
                progressBar1.Maximum = Listplv.Count - 1;
                progressBar1.Minimum = 0;
                int demPLV = 0;
                plvot = "";

                foreach (string s in Listplv)
                {
                    if (!run)
                    {
                        guna2Button_Submit.Enabled = false;
                        break;
                    }

                    // Cập nhật giá trị progress bar
                    progressBar1.Value = demPLV;

                    // Thêm mã trường vào KetLuan cho mỗi vòng lặp
                    List<string> KetLuan = new List<string> { s };

                    // Thực hiện suy diễn tiến trong một tác vụ nền
                    if (await Task.Run(() => ttSuyDienTien.SuyDienVot(GiaThiet, KetLuan)))
                    {
                        // Nếu suy diễn thành công, cập nhật kết quả
                        plvot += Listplv.ElementAt(demPLV) + "\n";
                        NDplvot += ListTenPLV.ElementAt(demPLV) + "\n";

                        d++;
                    }

                    demPLV++;
                    await Task.Delay(100); // Tạm dừng để cập nhật giao diện
                }

                // Nếu không có kết quả, thông báo cho người dùng
                if (d == 0 && run != false)
                {
                    ketqua = "Không có vợt phù hợp với lựa chọn của bạn!!!" +
                        "\nVui lòng chọn lại." +
                        "\nChúng tôi sẽ cập nhật thêm thông tin trong thời gian tới.";
                }
            }
            else
            {
                MessageBox.Show("Danh sách mã vợt trống! Không thể hiển thị kết quả.");
            }
            // Kiểm tra số lượng vợt trước khi thiết lập giá trị cho progressBar
            if (Listpln.Count > 0)
            {
                progressBar1.Maximum = Listpln.Count - 1;
                progressBar1.Minimum = 0;
                int demPLN = 0;
                plnguoi = "";

                foreach (string s in Listpln)
                {
                    if (!run)
                    {
                        guna2Button_Submit.Enabled = false;
                        break;
                    }

                    // Cập nhật giá trị progress bar
                    progressBar1.Value = demPLN;

                    // Thêm mã trường vào KetLuan cho mỗi vòng lặp
                    List<string> KetLuan = new List<string> { s };

                    // Thực hiện suy diễn tiến trong một tác vụ nền
                    if (await Task.Run(() => ttSuyDienTien.SuyDienNguoi(GiaThiet, KetLuan)))
                    {
                        // Nếu suy diễn thành công, cập nhật kết quả
                        plnguoi += Listpln.ElementAt(demPLN) + "\n";
                        NDplnguoi += ListTenPLN.ElementAt(demPLN) + "\n";
                        d++;
                    }

                    demPLN++;
                    await Task.Delay(100); // Tạm dừng để cập nhật giao diện
                }

                // Nếu không có kết quả, thông báo cho người dùng
                if (d == 0 && run != false)
                {
                    ketqua = "Không có vợt phù hợp với lựa chọn của bạn!!!" +
                        "\nVui lòng chọn lại." +
                        "\nChúng tôi sẽ cập nhật thêm thông tin trong thời gian tới.";
                }
            }
            else
            {
                MessageBox.Show("Danh sách mã vợt trống! Không thể hiển thị kết quả.");
            }
            // Chờ cho đến khi progressBar hoàn thành
            await Task.Delay(500);

            // Ẩn form TuVan hiện tại
            this.Hide();

            // Tạo instance của FormTuVan và hiển thị nó trên panel HinhNen
            KetQua ketquafrm = new KetQua();

            // Gọi phương thức để thiết lập kết quả
            ketquafrm.SetResults(mucdich, trinhdo,doCung, sucCang, trongLuong, chuViCan, chatLuong,chieuDai, 
                                 trongLuongVung, dangMatVot, taiChinh, hang, ketqua, idketqua, endTime, startTime, plvot,plnguoi, NDplvot, NDplnguoi);
            ketquafrm.Show(); // Hiển thị form
        }

        private void guna2Button_LamMoi_Click(object sender, EventArgs e)
        {
            label_CauHoi.Visible = true;
            // Đặt lại chỉ số câu hỏi về 0
            currentQuestionIndex = 0;

            // Xóa các đáp án đã chọn (nếu cần)
            mucdich = null;
            trinhdo = null;
            doCung = null;
            sucCang = null;
            trongLuong = null;
            chuViCan = null;
            chatLuong = null;
            chieuDai = null;
            trongLuongVung = null;
            dangMatVot = null;
            taiChinh = null;
            hang = null;

            // Hiển thị lại câu hỏi đầu tiên
            DisplayCurrentQuestion();
        }
        private void guna2Button_DapAn_Click(object sender, EventArgs e)
        {
        }
        private void label_CauHoi_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button_DapAn2_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button_DapAn3_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button_DapAn4_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button_DapAn5_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button_DapAn6_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button_DapAn7_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button_DapAn8_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button_DapAn9_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button_DapAn10_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button_DapAn11_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button_DapAn12_Click(object sender, EventArgs e)
        {

        }

        
    }
}

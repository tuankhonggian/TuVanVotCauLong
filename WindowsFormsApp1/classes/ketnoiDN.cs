using System;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace WindowsFormsApp1.ketnoi.dangnhap
{
    internal class ketnoiDN
    {
        string conStr = @"Data Source=ANHTUAN\SQLEXPRESS;Initial Catalog=HCGCAULONG;Integrated Security=True;Encrypt=False";
        SqlConnection conn;
        public ketnoiDN()
        {
            conn = new SqlConnection(conStr);
        }
        public DataSet Laydulieu(string truyvan)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(truyvan, conn);
                da.Fill(ds);
                return ds;
            }
            catch
            {
                return null;
            }
        }
        public bool Thucthi(string truyvan)
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(truyvan, conn);
                int r = cmd.ExecuteNonQuery();
                conn.Close();
                return r > 0;
            }
            catch
            {
                return false;
            }
        }
        public string LayQuyenHan(string taiKhoan, string matKhau)
        {
            try
            {
                conn.Open();
                string truyvan = "SELECT Quyen FROM [Account] WHERE TaiKhoan = @TaiKhoan AND MatKhau = @MatKhau";
                SqlCommand cmd = new SqlCommand(truyvan, conn);
                cmd.Parameters.AddWithValue("@TaiKhoan", taiKhoan);
                cmd.Parameters.AddWithValue("@MatKhau", matKhau);

                object result = cmd.ExecuteScalar();
                conn.Close();
                return result != null ? result.ToString() : null;
            }
            catch
            {
                return null;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }
        public int? LayIDNguoiDung(string taiKhoan, string matKhau)
        {
            try
            {
                conn.Open();
                string truyvan = "SELECT AccountID FROM [Account] WHERE TaiKhoan = @TaiKhoan AND MatKhau = @MatKhau";
                SqlCommand cmd = new SqlCommand(truyvan, conn);
                cmd.Parameters.AddWithValue("@TaiKhoan", taiKhoan);
                cmd.Parameters.AddWithValue("@MatKhau", matKhau);

                object result = cmd.ExecuteScalar();
                conn.Close();
                return result != null ? (int?)Convert.ToInt32(result) : null;
            }
            catch
            {
                return null;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        public DataTable getTable(string sql)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(conStr)) // Use conStr here
            {
                using (SqlDataAdapter da = new SqlDataAdapter(sql, conn))
                {
                    try
                    {
                        conn.Open();
                        da.Fill(dt);
                    }
                    catch (Exception ex)
                    {
                        // Handle exceptions (log or display error)
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
            return dt;
        }

    }
}

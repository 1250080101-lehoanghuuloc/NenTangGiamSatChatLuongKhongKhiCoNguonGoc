using khonghkhi;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;

namespace khongkhi
{
    public class TaiKhoanRepository
    {
        // 1. Phục vụ chức năng Đăng nhập
        public DataTable TimTaiKhoan(string tenDN, string matKhauBam)
        {
            DataTable dt = new DataTable();
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string query = @"SELECT MaTaiKhoan, TenDangNhap, MaVaiTro, KichHoat, HoTen 
                                 FROM TaiKhoan 
                                 WHERE TenDangNhap = :username AND MatKhauHash = :pass";
                using (var cmd = new OracleCommand(query, conn))
                {
                    cmd.Parameters.Add(":username", OracleDbType.Varchar2).Value = tenDN;
                    cmd.Parameters.Add(":pass", OracleDbType.Varchar2).Value = matKhauBam;

                    using (var da = new OracleDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            return dt;
        }

        // 2. Ghi nhận log đăng nhập
        public void GhiNhanDangNhap(string thongTin, string ketQua)
        {
            System.Diagnostics.Debug.WriteLine($"[LOG] Đăng nhập {ketQua} cho: {thongTin} vào lúc {DateTime.Now}");
        }

        // 3. Phục vụ quản lý tài khoản: Lấy danh sách
        public DataTable LayDanhSachTaiKhoan()
        {
            DataTable dt = new DataTable();
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string query = @"SELECT tk.MaTaiKhoan, tk.TenDangNhap, vt.TenVaiTro, tk.MaVaiTro, tk.KichHoat 
                                 FROM TaiKhoan tk 
                                 JOIN VaiTro vt ON tk.MaVaiTro = vt.MaVaiTro";
                using (var da = new OracleDataAdapter(query, conn))
                {
                    da.Fill(dt);
                }
            }
            return dt;
        }

        // 4. Phục vụ quản lý tài khoản: Kiểm tra trùng tên đăng nhập
        public int DemTenDangNhap(string tenDN)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM TaiKhoan WHERE TenDangNhap = :tendn";
                using (var cmd = new OracleCommand(query, conn))
                {
                    cmd.Parameters.Add(":tendn", OracleDbType.Varchar2).Value = tenDN;
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        // 5. Phục vụ quản lý tài khoản: Thêm mới tài khoản
        public void ThemTaiKhoan(string tenDN, string matKhauHash, int maVaiTro)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string queryMaxId = "SELECT NVL(MAX(MaTaiKhoan), 0) + 1 FROM TaiKhoan";
                long newId = 1;
                using (var cmdMax = new OracleCommand(queryMaxId, conn))
                {
                    newId = Convert.ToInt64(cmdMax.ExecuteScalar());
                }

                string query = @"INSERT INTO TaiKhoan (MaTaiKhoan, TenDangNhap, MatKhauHash, MaVaiTro, KichHoat) 
                                 VALUES (:matk, :tendn, :matkhau, :mavaitro, 1)";
                using (var cmd = new OracleCommand(query, conn))
                {
                    cmd.Parameters.Add(":matk", OracleDbType.Int64).Value = newId;
                    cmd.Parameters.Add(":tendn", OracleDbType.Varchar2).Value = tenDN;
                    cmd.Parameters.Add(":matkhau", OracleDbType.Varchar2).Value = matKhauHash;
                    cmd.Parameters.Add(":mavaitro", OracleDbType.Int32).Value = maVaiTro;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // 6. Phục vụ quản lý tài khoản: Cập nhật phân quyền
        public void CapNhatVaiTro(long maTK, int maVaiTro)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string query = "UPDATE TaiKhoan SET MaVaiTro = :mavaitro WHERE MaTaiKhoan = :matk";
                using (var cmd = new OracleCommand(query, conn))
                {
                    cmd.Parameters.Add(":mavaitro", OracleDbType.Int32).Value = maVaiTro;
                    cmd.Parameters.Add(":matk", OracleDbType.Int64).Value = maTK;
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
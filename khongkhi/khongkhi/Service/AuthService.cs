using System;
using System.Data;
using System.Security.Cryptography;
using System.Text;

namespace khongkhi
{
    public class AuthService
    {
        private TaiKhoanRepository taiKhoanRepo = new TaiKhoanRepository();

        // bamMatKhau(matKhau)
        public string BamMatKhau(string rawPassword)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(rawPassword));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                    builder.Append(b.ToString("x2"));
                return builder.ToString();
            }
        }

        // dangNhap(tenDN, matKhau)
        public DataRow DangNhap(string tenDN, string matKhau, out string thongBaoLoi)
        {
            thongBaoLoi = "";
            string matKhauBam = BamMatKhau(matKhau);

            DataTable dt = taiKhoanRepo.TimTaiKhoan(tenDN, matKhauBam);

            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                int kichHoat = Convert.ToInt32(row["KichHoat"]);

                if (kichHoat != 1)
                {
                    thongBaoLoi = "Tài khoản của bạn đã bị khóa!";
                    taiKhoanRepo.GhiNhanDangNhap(tenDN, "ThatBai_BiKhoa");
                    return null;
                }

                // Thành công: ghi nhận và trả về thông tin tài khoản (phiên làm việc & vai trò)
                taiKhoanRepo.GhiNhanDangNhap(tenDN, "ThanhCong");
                return row;
            }
            else
            {
                // Thất bại
                taiKhoanRepo.GhiNhanDangNhap(tenDN, "ThatBai_SaiThongTin");
                thongBaoLoi = "Tên đăng nhập hoặc mật khẩu không đúng";
                return null;
            }
        }
    }
}
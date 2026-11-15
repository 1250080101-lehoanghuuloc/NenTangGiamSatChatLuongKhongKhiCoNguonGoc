using System;
using System.Data;
using System.Security.Cryptography;
using System.Text;

namespace khongkhi
{
    public class TaiKhoanService
    {
        private TaiKhoanRepository repo = new TaiKhoanRepository();

        public DataTable LayDanhSach()
        {
            return repo.LayDanhSachTaiKhoan();
        }

        // Bổ sung phương thức lấy danh sách vai trò cho ComboBox trên form
        public DataTable LayDanhSachVaiTro()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("MaVaiTro", typeof(int));
            dt.Columns.Add("TenVaiTro", typeof(string));

            // Khớp với dữ liệu bảng VaiTro trong Oracle (1: Quản lý, 2: Người dùng)
            dt.Rows.Add(1, "Quản lý");
            dt.Rows.Add(2, "Người dùng");

            return dt;
        }

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

        public string ThemTaiKhoan(string tenDN, string matKhau, int maVaiTro)
        {
            if (string.IsNullOrWhiteSpace(tenDN) || string.IsNullOrWhiteSpace(matKhau))
            {
                return "Lỗi: Thiếu dữ liệu bắt buộc!";
            }

            int count = repo.DemTenDangNhap(tenDN);
            if (count > 0)
            {
                return "Lỗi: Tên đăng nhập đã tồn tại!";
            }

            string matKhauHash = BamMatKhau(matKhau);
            repo.ThemTaiKhoan(tenDN, matKhauHash, maVaiTro);

            return "Thành công";
        }

        public void CapNhatQuyen(long maTK, int maVaiTro)
        {
            repo.CapNhatVaiTro(maTK, maVaiTro);
        }
    }
}
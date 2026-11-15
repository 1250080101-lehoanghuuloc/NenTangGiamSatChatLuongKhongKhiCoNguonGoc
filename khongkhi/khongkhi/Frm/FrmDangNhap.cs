using System;
using System.Data;
using System.Windows.Forms;

namespace khongkhi
{
    public partial class FrmDangNhap : Form
    {
        public static string TenDangNhapHienTai = "";
        public static int MaVaiTroHienTai = 0;

        private AuthService authService = new AuthService();

        public FrmDangNhap()
        {
            InitializeComponent();
        }

        // kiemTraKhongDeTrong
        private bool KiemTraKhongDeTrong(string tenDN, string matKhau)
        {
            if (string.IsNullOrWhiteSpace(tenDN) || string.IsNullOrWhiteSpace(matKhau))
            {
                MessageBox.Show("Cần nhập đủ thông tin!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            string tenDN = txtTenDangNhap.Text.Trim();
            string matKhau = txtMatKhau.Text.Trim();

            // Nhánh alt [A1: Để trống tên đăng nhập hoặc mật khẩu]
            if (!KiemTraKhongDeTrong(tenDN, matKhau))
            {
                return; // Quay lại bước 1 cho nhập lại
            }

            try
            {
                string loi = "";
                DataRow accountRow = authService.DangNhap(tenDN, matKhau, out loi);

                // Nhánh alt [Thông tin đúng: TaiKhoan != null]
                if (accountRow != null)
                {
                    // Thiết lập phiên làm việc và vai trò
                    TenDangNhapHienTai = accountRow["TenDangNhap"].ToString();
                    MaVaiTroHienTai = Convert.ToInt32(accountRow["MaVaiTro"]);

                    MessageBox.Show("Đăng nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Báo hiệu đăng nhập thành công để vòng lặp trong Program.cs mở FrmMain qua Application.Run()
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    // Nhánh alt [Thông tin sai: TaiKhoan == null]
                    lblThongBao.Text = loi;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hệ thống: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
using System;
using System.Data;
using System.Windows.Forms;

namespace khongkhi
{
    public partial class FrmQuanLyTaiKhoan : Form
    {
        private TaiKhoanService service = new TaiKhoanService();

        public FrmQuanLyTaiKhoan()
        {
            InitializeComponent();
        }

        private void FrmQuanLyTaiKhoan_Load(object sender, EventArgs e)
        {
            LoadComboboxVaiTro();
            LoadDanhSach();
        }

        private void LoadComboboxVaiTro()
        {
            try
            {
                // Nếu service của bạn dùng tên hàm khác để lấy vai trò, hãy thay đổi tương ứng ở đây
                cboVaiTro.DataSource = service.LayDanhSachVaiTro();
                cboVaiTro.DisplayMember = "TenVaiTro";
                cboVaiTro.ValueMember = "MaVaiTro";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách vai trò: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadDanhSach()
        {
            try
            {
                DataTable dt = service.LayDanhSach();
                dgvTaiKhoan.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách tài khoản: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Sự kiện nút "Thêm" tài khoản (Khớp với signature 3 tham số của service)
        private void btnThem_Click(object sender, EventArgs e)
        {
            string tenDN = txtTenDangNhap.Text.Trim();
            string matKhau = txtMatKhau.Text.Trim();

            if (string.IsNullOrEmpty(tenDN) || string.IsNullOrEmpty(matKhau))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ tên đăng nhập và mật khẩu!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cboVaiTro.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn vai trò cho tài khoản!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int maVaiTro = Convert.ToInt32(cboVaiTro.SelectedValue);

            // Gọi đúng 3 tham số mà service yêu cầu
            string ketQua = service.ThemTaiKhoan(tenDN, matKhau, maVaiTro);

            if (ketQua == "Thành công")
            {
                MessageBox.Show("Thêm tài khoản thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDanhSach();
                btnLamMoi_Click(sender, e);
            }
            else
            {
                MessageBox.Show(ketQua, "Từ chối", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Sự kiện nút "Phân quyền" / Cập nhật quyền
        private void btnPhanQuyen_Click(object sender, EventArgs e)
        {
            if (dgvTaiKhoan.SelectedRows.Count > 0)
            {
                long maTK = Convert.ToInt64(dgvTaiKhoan.SelectedRows[0].Cells["MaTaiKhoan"].Value);
                int vaiTroMoi = Convert.ToInt32(cboVaiTro.SelectedValue);

                try
                {
                    service.CapNhatQuyen(maTK, vaiTroMoi);
                    MessageBox.Show("Thành công: Quyền có hiệu lực từ lần đăng nhập kế tiếp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDanhSach();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi cập nhật quyền: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn tài khoản cần phân quyền trên bảng!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dgvTaiKhoan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvTaiKhoan.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvTaiKhoan.SelectedRows[0];
                txtTenDangNhap.Text = row.Cells["TenDangNhap"].Value?.ToString();

                // Sửa lại kiểm tra cột thông qua dgvTaiKhoan.Columns.Contains
                if (dgvTaiKhoan.Columns.Contains("HoTen") && row.Cells["HoTen"].Value != null)
                {
                    txtHoTen.Text = row.Cells["HoTen"].Value.ToString();
                }

                if (dgvTaiKhoan.Columns.Contains("MaVaiTro") && row.Cells["MaVaiTro"].Value != null)
                {
                    cboVaiTro.SelectedValue = row.Cells["MaVaiTro"].Value;
                }

                if (dgvTaiKhoan.Columns.Contains("KichHoat") && row.Cells["KichHoat"].Value != null)
                {
                    chkKichHoat.Checked = Convert.ToInt32(row.Cells["KichHoat"].Value) == 1;
                }

                txtTenDangNhap.Enabled = false; // Khóa tên đăng nhập khi chọn dòng sửa
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtTenDangNhap.Clear();
            txtMatKhau.Clear();
            if (txtHoTen != null) txtHoTen.Clear();
            if (chkKichHoat != null) chkKichHoat.Checked = false;
            txtTenDangNhap.Enabled = true;
            LoadDanhSach();
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
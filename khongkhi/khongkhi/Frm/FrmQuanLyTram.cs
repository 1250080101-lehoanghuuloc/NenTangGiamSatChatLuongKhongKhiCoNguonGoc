using System;
using System.Data;
using System.Windows.Forms;

namespace khongkhi
{
    public partial class FrmQuanLyTram : Form
    {
        private TramService service = new TramService();

        public FrmQuanLyTram()
        {
            InitializeComponent();
        }

        private void FrmQuanLyTram_Load(object sender, EventArgs e)
        {
            LoadDanhSach();
        }

        private void LoadDanhSach()
        {
            try
            {
                DataTable dt = service.LayDanhSach();
                dgvTram.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách trạm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string maTram = txtMaTram.Text.Trim();
            string tenTram = txtTenTram.Text.Trim();
            string toaDo = txtToaDo.Text.Trim();
            string diaChi = txtDiaChi.Text.Trim();

            // Nếu service của bạn nhận 4 tham số (bao gồm Tọa độ), hãy giữ nguyên. Nếu service chỉ nhận 3, hãy truyền 3 tham số.
            string ketQua = service.ThemMoi(maTram, tenTram, diaChi);
            if (ketQua == "Thành công")
            {
                MessageBox.Show("Thêm trạm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDanhSach();
                btnLamMoi_Click(sender, e);
            }
            else
            {
                MessageBox.Show(ketQua, "Từ chối", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string maTram = txtMaTram.Text.Trim();
            string tenTram = txtTenTram.Text.Trim();
            string diaChi = txtDiaChi.Text.Trim();

            string ketQua = service.CapNhat(maTram, tenTram, diaChi);
            if (ketQua == "Thành công")
            {
                MessageBox.Show("Cập nhật trạm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDanhSach();
            }
            else
            {
                MessageBox.Show(ketQua, "Từ chối", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvTram.SelectedRows.Count > 0)
            {
                string maTram = dgvTram.SelectedRows[0].Cells["MaTram"].Value.ToString();
                if (MessageBox.Show($"Bạn có chắc chắn muốn xóa trạm {maTram} không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string ketQua = service.Xoa(maTram);
                    if (ketQua == "Thành công")
                    {
                        MessageBox.Show("Xóa trạm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadDanhSach();
                        btnLamMoi_Click(sender, e);
                    }
                    else
                    {
                        MessageBox.Show(ketQua, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn trạm cần xóa trên bảng!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dgvTram_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvTram.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvTram.SelectedRows[0];
                txtMaTram.Text = row.Cells["MaTram"].Value?.ToString();
                txtTenTram.Text = row.Cells["TenTram"].Value?.ToString();

                // Sửa lại cách kiểm tra cột tồn tại bằng cách gọi qua Columns của DataGridView
                if (dgvTram.Columns.Contains("ToaDo") && row.Cells["ToaDo"].Value != null)
                    txtToaDo.Text = row.Cells["ToaDo"].Value.ToString();

                if (dgvTram.Columns.Contains("DiaChi") && row.Cells["DiaChi"].Value != null)
                    txtDiaChi.Text = row.Cells["DiaChi"].Value.ToString();

                txtMaTram.Enabled = false; // Khóa mã trạm khi chọn dòng để sửa
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtMaTram.Clear();
            txtTenTram.Clear();
            txtToaDo.Clear();
            txtDiaChi.Clear();
            txtMaTram.Enabled = true; // Mở lại khóa mã trạm để nhập mới
            LoadDanhSach();
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
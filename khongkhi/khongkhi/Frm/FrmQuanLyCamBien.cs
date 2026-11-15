using System;
using System.Data;
using System.Windows.Forms;

namespace khongkhi
{
    public partial class FrmQuanLyCamBien : Form
    {
        private CamBienService service = new CamBienService();

        public FrmQuanLyCamBien()
        {
            InitializeComponent();
        }

        private void FrmQuanLyCamBien_Load(object sender, EventArgs e)
        {
            LoadComboboxTram();
            LoadComboboxPhuongPhap();
            LoadDanhSachCamBien();
            DatTrangThaiBanDau();
        }

        private void DatTrangThaiBanDau()
        {
            txtMaCamBien.Clear();
            txtLoaiChiSo.Clear();
            txtDonViDo.Clear();
            txtMaCamBien.Enabled = true;
        }

        private void LoadComboboxTram()
        {
            cboTram.DataSource = service.LayDanhSachTram();
            cboTram.DisplayMember = "TenTram";
            cboTram.ValueMember = "MaTram";
        }

        private void LoadComboboxPhuongPhap()
        {
            cboPhuongPhap.DataSource = service.LayDanhSachPhuongPhap();
            cboPhuongPhap.DisplayMember = "TenPhuongPhap";
            cboPhuongPhap.ValueMember = "MaPhuongPhap";
        }

        private void LoadDanhSachCamBien()
        {
            try
            {
                dgvCamBien.DataSource = service.LayDanhSach();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách cảm biến: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string maCB = txtMaCamBien.Text.Trim();
            string maTram = cboTram.SelectedValue?.ToString();

            if (string.IsNullOrEmpty(maCB))
            {
                MessageBox.Show("Vui lòng nhập mã cảm biến!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaCamBien.Focus();
                return;
            }

            if (cboTram.SelectedValue == null || cboPhuongPhap.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn Trạm và Phương pháp đo hợp lệ!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            long maPP = Convert.ToInt64(cboPhuongPhap.SelectedValue);
            string loaiChiSo = txtLoaiChiSo.Text.Trim();
            string donVi = txtDonViDo.Text.Trim();

            // Gọi service lưu với trạng thái thêm mới (isUpdate = false)
            string ketQua = service.LuuCamBien(maCB, maTram, maPP, loaiChiSo, donVi, false);
            if (ketQua == "Thành công")
            {
                MessageBox.Show("Thêm cảm biến thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDanhSachCamBien();
                DatTrangThaiBanDau();
            }
            else
            {
                MessageBox.Show(ketQua, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string maCB = txtMaCamBien.Text.Trim();
            string maTram = cboTram.SelectedValue?.ToString();

            if (string.IsNullOrEmpty(maCB))
            {
                MessageBox.Show("Vui lòng chọn cảm biến cần sửa trên bảng!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cboTram.SelectedValue == null || cboPhuongPhap.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn Trạm và Phương pháp đo hợp lệ!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            long maPP = Convert.ToInt64(cboPhuongPhap.SelectedValue);
            string loaiChiSo = txtLoaiChiSo.Text.Trim();
            string donVi = txtDonViDo.Text.Trim();

            // Gọi service lưu với trạng thái cập nhật (isUpdate = true)
            string ketQua = service.LuuCamBien(maCB, maTram, maPP, loaiChiSo, donVi, true);
            if (ketQua == "Thành công")
            {
                MessageBox.Show("Cập nhật cảm biến thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDanhSachCamBien();
                DatTrangThaiBanDau();
            }
            else
            {
                MessageBox.Show(ketQua, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvCamBien.CurrentRow == null) return;
            string maCB = dgvCamBien.CurrentRow.Cells["MaCamBien"].Value.ToString();

            if (MessageBox.Show($"Bạn có chắc chắn muốn xóa cảm biến {maCB} không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string ketQua = service.XoaCamBien(maCB);
                if (ketQua == "Thành công")
                {
                    MessageBox.Show("Xóa cảm biến thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDanhSachCamBien();
                    DatTrangThaiBanDau();
                }
                else
                {
                    MessageBox.Show(ketQua, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            LoadDanhSachCamBien();
            DatTrangThaiBanDau();
        }

        private void btnDong_Click(object sender, EventArgs e) => this.Close();

        private void dgvCamBien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvCamBien.Rows[e.RowIndex];
                txtMaCamBien.Text = row.Cells["MaCamBien"].Value?.ToString();
                txtLoaiChiSo.Text = row.Cells["LoaiChiSo"].Value?.ToString();
                txtDonViDo.Text = row.Cells["DonViDo"].Value?.ToString();

                if (row.Cells["MaTram"].Value != null)
                    cboTram.SelectedValue = row.Cells["MaTram"].Value;
                if (row.Cells["MaPhuongPhap"].Value != null)
                    cboPhuongPhap.SelectedValue = row.Cells["MaPhuongPhap"].Value;

                txtMaCamBien.Enabled = false; // Khóa mã cảm biến khi chọn dòng sửa
            }
        }
    }
}
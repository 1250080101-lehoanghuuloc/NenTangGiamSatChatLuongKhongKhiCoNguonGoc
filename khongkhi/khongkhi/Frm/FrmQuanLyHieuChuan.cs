using System;
using System.Data;
using System.Windows.Forms;

namespace khongkhi
{
    public partial class FrmQuanLyHieuChuan : Form
    {
        private HieuChuanService service = new HieuChuanService();

        public FrmQuanLyHieuChuan()
        {
            InitializeComponent();
        }

        private void FrmQuanLyHieuChuan_Load(object sender, EventArgs e)
        {
            LoadComboboxCamBien();
        }

        private void LoadComboboxCamBien()
        {
            cboCamBien.DataSource = service.LayDanhSachCamBien();
            cboCamBien.DisplayMember = "TenCB";
            cboCamBien.ValueMember = "MaCamBien";
        }

        private void cboCamBien_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboCamBien.SelectedValue == null) return;
            string maCamBien = cboCamBien.SelectedValue.ToString();
            LoadThongTinHieuChuan(maCamBien);
        }

        private void LoadThongTinHieuChuan(string maCamBien)
        {
            try
            {
                // Lấy phiên bản đang hiệu lực
                DataRow r = service.LayPhienBanHienHanh(maCamBien);
                if (r != null)
                {
                    txtMaHienHanh.Text = r["MaPhienBan"].ToString();
                    txtBatDauHienHanh.Text = Convert.ToDateTime(r["ThoiDiemBatDau"]).ToString("yyyy-MM-dd HH:mm");
                }
                else
                {
                    txtMaHienHanh.Text = "Không có";
                    txtBatDauHienHanh.Text = "";
                }

                // Lấy lịch sử hiệu chuẩn
                dgvLichSu.DataSource = service.LayLichSuHieuChuan(maCamBien);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải thông tin hiệu chuẩn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (cboCamBien.SelectedValue == null) return;

            string maCamBien = cboCamBien.SelectedValue.ToString();
            string maPB = txtMaPhienBan.Text.Trim();
            DateTime dtBatDau = dtpBatDau.Value;

            string ketQua = service.TaoPhienBanMoi(maCamBien, maPB, dtBatDau);

            if (ketQua == "Thành công")
            {
                MessageBox.Show("Cập nhật phiên bản hiệu chuẩn thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaPhienBan.Clear();
                LoadThongTinHieuChuan(maCamBien);
            }
            else
            {
                MessageBox.Show(ketQua, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnDong_Click(object sender, EventArgs e) => this.Close();
    }
}
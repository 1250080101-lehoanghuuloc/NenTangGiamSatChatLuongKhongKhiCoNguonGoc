using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace khongkhi
{
    public partial class FrmXemDuLieu : Form
    {
        private PhepDoService service = new PhepDoService();
        private MqttListenerService mqttListener = new MqttListenerService(); // Dịch vụ MQTT

        public FrmXemDuLieu()
        {
            InitializeComponent();
        }

        private async void FrmXemDuLieu_Load(object sender, EventArgs e)
        {
            LoadComboboxTram();
            LoadDuLieuQuanTrac();

            try
            {
                // Khởi chạy ngầm lắng nghe dữ liệu từ Wokwi MQTT Broker
                await mqttListener.StartListeningAsync(msg =>
                {
                    // Khi có dữ liệu mới từ Wokwi đẩy lên, tự động cập nhật lại bảng và biểu đồ trên giao diện
                    this.Invoke((MethodInvoker)delegate {
                        LoadDuLieuQuanTrac();
                    });
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine("MQTT Error: " + ex.Message);
            }
        }

        private void LoadComboboxTram()
        {
            try
            {
                cboTram.DataSource = service.LayDanhSachTram();
                cboTram.DisplayMember = "TenTram";
                cboTram.ValueMember = "MaTram";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách trạm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadComboboxCamBien(string maTram)
        {
            try
            {
                cboCamBien.DataSource = service.LayDanhSachCamBien(maTram);
                cboCamBien.DisplayMember = "LoaiChiSo";
                cboCamBien.ValueMember = "MaCamBien";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách cảm biến: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadDuLieuQuanTrac()
        {
            try
            {
                DataTable dt = service.LayDuLieuQuanTrac();
                dgvData.DataSource = dt;
                VeBieuDo(dt); // Vẽ biểu đồ tự động khi tải dữ liệu
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu quan trắc: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Hàm vẽ biểu đồ giá trị theo thời gian
        private void VeBieuDo(DataTable dt)
        {
            try
            {
                chartThoiGian.Series["GiaTriDo"].Points.Clear();

                if (dt == null || dt.Rows.Count == 0) return;

                chartThoiGian.ChartAreas[0].AxisX.Title = "Thời gian đo";
                chartThoiGian.ChartAreas[0].AxisY.Title = "Giá trị";
                chartThoiGian.Series["GiaTriDo"].LegendText = "Chỉ số quan trắc";

                // Sắp xếp dữ liệu theo thời gian tăng dần để đường biểu đồ đi từ trái sang phải mượt mà
                var sortedRows = dt.AsEnumerable().OrderBy(r => r["ThoiDiemDo"]);

                foreach (var row in sortedRows)
                {
                    if (row["ThoiDiemDo"] != DBNull.Value && row["GiaTriDo"] != DBNull.Value)
                    {
                        DateTime thoiDiem = Convert.ToDateTime(row["ThoiDiemDo"]);
                        double giaTri = Convert.ToDouble(row["GiaTriDo"]);

                        chartThoiGian.Series["GiaTriDo"].Points.AddXY(thoiDiem.ToString("dd/MM HH:mm"), giaTri);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi vẽ biểu đồ: " + ex.Message);
            }
        }

        private void cboTram_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboTram.SelectedValue != null && cboTram.SelectedValue.ToString() != "System.Data.DataRowView")
            {
                string maTram = cboTram.SelectedValue.ToString();
                LoadComboboxCamBien(maTram);
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                if (cboTram.SelectedValue == null || cboCamBien.SelectedValue == null)
                {
                    MessageBox.Show("Vui lòng chọn đầy đủ Trạm và Cảm biến!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string maTram = cboTram.SelectedValue.ToString();
                string loaiChiSo = cboCamBien.Text;

                DataTable dt = service.TimKiemNangCao(maTram, loaiChiSo, dtpTuNgay.Value, dtpDenNgay.Value);
                dgvData.DataSource = dt;
                VeBieuDo(dt); // Cập nhật lại biểu đồ theo kết quả tìm kiếm
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tìm kiếm dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXemNguonGoc_Click(object sender, EventArgs e)
        {
            if (dgvData.SelectedRows.Count > 0)
            {
                long maPhepDo = Convert.ToInt64(dgvData.SelectedRows[0].Cells["MaPhepDo"].Value);
                FrmNguonGoc frm = new FrmNguonGoc(maPhepDo);
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show(
                    "Vui lòng chọn một phép đo trên bảng để xem nguồn gốc!",
                    "Cảnh báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
using System;
using System.Data;
using System.Windows.Forms;

namespace khongkhi
{
    public partial class FrmNguonGoc : Form
    {
        private long _maPhepDo;
        private PhepDoService service = new PhepDoService();

        public FrmNguonGoc(long maPhepDo)
        {
            InitializeComponent();
            _maPhepDo = maPhepDo;
        }

        private void FrmNguonGoc_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = service.LayChiTietNguonGoc(_maPhepDo);
                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow r = dt.Rows[0];

                    // Hàm hỗ trợ tìm đúng tên cột không phân biệt chữ hoa/thường
                    string GetVal(string columnName)
                    {
                        foreach (DataColumn col in dt.Columns)
                        {
                            if (col.ColumnName.Equals(columnName, StringComparison.OrdinalIgnoreCase))
                            {
                                return r[col] != DBNull.Value ? r[col].ToString() : "";
                            }
                        }
                        return "";
                    }

                    // Gán dữ liệu an toàn tránh lỗi lệch tên cột
                    txtMaPhepDo.Text = GetVal("MAPHEPDO");
                    string giaTri = GetVal("GIATRIDO");
                    string donVi = GetVal("DONVIDO");
                    txtGiaTri.Text = $"{giaTri} {donVi}";

                    string thoiDiem = GetVal("THODIEMDO");
                    if (string.IsNullOrEmpty(thoiDiem)) thoiDiem = GetVal("ThoiDiemDo"); // Dự phòng kiểu CamelCase

                    if (!string.IsNullOrEmpty(thoiDiem) && DateTime.TryParse(thoiDiem, out DateTime dtTime))
                    {
                        txtThoiDiemDo.Text = dtTime.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    else
                    {
                        txtThoiDiemDo.Text = thoiDiem;
                    }

                    txtTram.Text = GetVal("TENTRAM");
                    txtCamBien.Text = $"{GetVal("MACAMBIEN")} - {GetVal("LOAICHISO")}";
                    txtTrangThai.Text = GetVal("TRANGTHAI");

                    string lyDo = GetVal("LYDO");
                    txtLyDo.Text = !string.IsNullOrEmpty(lyDo) ? lyDo : "Không có";

                    string tenPP = GetVal("TENPHUONGPHAP");
                    txtPhuongPhap.Text = !string.IsNullOrEmpty(tenPP) ? tenPP : "Tán xạ ánh sáng (Laser Scattering)";

                    string maPB = GetVal("MAPHIENBAN");
                    txtPhienBan.Text = !string.IsNullOrEmpty(maPB) ? maPB : "Chưa hiệu chuẩn";

                    txtDoTre.Text = $"{GetVal("DOTRE")} giây";
                }
                else
                {
                    MessageBox.Show("Không tìm thấy thông tin nguồn gốc cho phép đo này!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải chi tiết nguồn gốc: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
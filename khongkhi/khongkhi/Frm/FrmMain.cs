using khonghkhi;
using MQTTnet;
using MQTTnet.Client;
using Newtonsoft.Json.Linq;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace khongkhi
{
    public partial class FrmMain : Form
    {
        private IMqttClient mqttClient;

        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            lblNguoiDungInfo.Text = $"Tài khoản: {FrmDangNhap.TenDangNhapHienTai} | Vai trò: {(FrmDangNhap.MaVaiTroHienTai == 1 ? "Quản lý" : "Người dùng")}";

            // Phân quyền người dùng: Ẩn hoàn toàn các chức năng quản trị của Admin
            if (FrmDangNhap.MaVaiTroHienTai != 1) // Nếu không phải Quản lý (tức là Người dùng)
            {
                btnQuanLyTram.Visible = false;
                btnQuanLyCamBien.Visible = false;
                btnQuanLyHieuChuan.Visible = false;
                btnQuanLyTaiKhoan.Visible = false;
            }
            else
            {
                // Đảm bảo khi là Admin thì các nút luôn hiển thị
                btnQuanLyTram.Visible = true;
                btnQuanLyCamBien.Visible = true;
                btnQuanLyHieuChuan.Visible = true;
                btnQuanLyTaiKhoan.Visible = true;
            }

            // Kích hoạt lắng nghe MQTT từ ESP32 Wokwi
            KhoiDongMqttListener();
        }

        private void btnQuanLyTram_Click(object sender, EventArgs e) => new FrmQuanLyTram().ShowDialog();
        private void btnQuanLyCamBien_Click(object sender, EventArgs e) => new FrmQuanLyCamBien().ShowDialog();
        private void btnQuanLyHieuChuan_Click(object sender, EventArgs e) => new FrmQuanLyHieuChuan().ShowDialog();
        private void btnXemDuLieu_Click(object sender, EventArgs e) => new FrmXemDuLieu().ShowDialog();
        private void btnQuanLyTaiKhoan_Click(object sender, EventArgs e) => new FrmQuanLyTaiKhoan().ShowDialog();
        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            // Hiển thị lại form đăng nhập
            FrmDangNhap frmLogin = new FrmDangNhap();
            frmLogin.Show();

            // Đóng hẳn form chính hiện tại để giải phóng tài nguyên và vòng đời ứng dụng chính xác
            this.Close();
        }

        private void btnThoat_Click(object sender, EventArgs e) => Application.Exit();

        // -----------------------------------------------------------------
        // XỬ LÝ KẾT NỐI MQTT & ĐẨY DỮ LIỆU VÀO ORACLE CSDL
        // -----------------------------------------------------------------
        private async void KhoiDongMqttListener()
        {
            var factory = new MqttFactory();
            mqttClient = factory.CreateMqttClient();

            var options = new MqttClientOptionsBuilder()
                .WithTcpServer("broker.hivemq.com", 1883) // Broker công khai khớp với Wokwi
                .WithClientId("WinForms_Client_" + Guid.NewGuid().ToString().Substring(0, 6))
                .Build();

            // Sự kiện khi nhận được bản tin từ ESP32 gửi lên
            mqttClient.ApplicationMessageReceivedAsync += async e =>
            {
                // Lấy dữ liệu Payload an toàn trong MQTTnet v4
                byte[] payloadBytes = e.ApplicationMessage.PayloadSegment.Array;
                int offset = e.ApplicationMessage.PayloadSegment.Offset;
                int count = e.ApplicationMessage.PayloadSegment.Count;
                string payload = Encoding.UTF8.GetString(payloadBytes, offset, count);
                string topic = e.ApplicationMessage.Topic;

                // Kiểm tra đúng topic của trạm
                if (topic == "envmon-demo-abc123/tram01/phepdo")
                {
                    XuLyVaLuuDuLieuPhepDo(payload);
                }

                await Task.CompletedTask;
            };

            // Sự kiện kết nối thành công tới Broker
            mqttClient.ConnectedAsync += async e =>
            {
                // Đăng ký (Subscribe) nhận tin nhắn từ topic của Wokwi
                var subscribeOptions = new MqttClientSubscribeOptionsBuilder()
                    .WithTopicFilter("envmon-demo-abc123/tram01/phepdo")
                    .Build();

                await mqttClient.SubscribeAsync(subscribeOptions);
                System.Diagnostics.Debug.WriteLine("Đã kết nối và Subscribe MQTT thành công!");
            };

            try
            {
                // Tiến hành kết nối bất đồng bộ
                await mqttClient.ConnectAsync(options);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối MQTT Broker: " + ex.Message, "Lỗi MQTT", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Hàm bóc tách JSON từ ESP32 và INSERT động vào cơ sở dữ liệu Oracle
        private void XuLyVaLuuDuLieuPhepDo(string jsonPayload)
        {
            try
            {
                // Bóc tách JSON gửi từ ESP32 Wokwi
                JObject data = JObject.Parse(jsonPayload);

                string maCamBien = data["maCamBien"]?.Value<string>() ?? "CB01";
                string maPhuongPhapStr = data["maPP"]?.Value<string>() ?? "1";
                // Chuyển đổi mã phương pháp sang số nguyên nếu DB yêu cầu kiểu số, hoặc để string nếu DB là Varchar
                int maPhuongPhap = int.TryParse(maPhuongPhapStr.Replace("PP", ""), out int pp) ? pp : 1;

                string maPhiênBan = data["phienBanHieuChuan"]?.Value<string>(); // Có thể là null
                decimal giaTriDo = data["giaTri"]?.Value<decimal>() ?? 0;
                decimal doTre = data["doTreGiay"]?.Value<decimal>() ?? 0; // Đã sửa đúng tên trường doTreGiay từ ESP32
                string trangThai = data["trangThai"]?.Value<string>() ?? "HOP_LE";
                string lyDo = data["lyDo"]?.Value<string>() ?? "";

                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string query = @"INSERT INTO PhepDo (
                                MaPhepDo, MaCamBien, MaPhuongPhap, MaPhienBan, 
                                GiaTriDo, ThoiDiemDo, ThoiDiemNhan, DoTre, TrangThai, LyDo
                             ) VALUES (
                                SEQ_PHEPDO.NEXTVAL, :macambien, :maphuongphap, :maphienban, 
                                :giatrido, SYSTIMESTAMP, SYSTIMESTAMP, :dotre, :trangthai, :lydo
                             )";

                    using (var cmd = new OracleCommand(query, conn))
                    {
                        cmd.Parameters.Add(":macambien", OracleDbType.Varchar2).Value = maCamBien;
                        cmd.Parameters.Add(":maphuongphap", OracleDbType.Int32).Value = maPhuongPhap;

                        // Xử lý truyền giá trị NULL vào Oracle nếu phiên bản hiệu chuẩn là null
                        if (string.IsNullOrEmpty(maPhiênBan))
                            cmd.Parameters.Add(":maphienban", OracleDbType.Varchar2).Value = DBNull.Value;
                        else
                            cmd.Parameters.Add(":maphienban", OracleDbType.Varchar2).Value = maPhiênBan;

                        cmd.Parameters.Add(":giatrido", OracleDbType.Decimal).Value = giaTriDo;
                        cmd.Parameters.Add(":dotre", OracleDbType.Decimal).Value = doTre;
                        cmd.Parameters.Add(":trangthai", OracleDbType.Varchar2).Value = trangThai;
                        cmd.Parameters.Add(":lydo", OracleDbType.Varchar2).Value = lyDo; // Đã sửa thành Varchar2

                        cmd.ExecuteNonQuery();
                    }
                }
                System.Diagnostics.Debug.WriteLine($"Đã lưu thành công dữ liệu từ cảm biến: {maCamBien}");
            }
            catch (Exception ex)
            {
                // Hiển thị trực tiếp lỗi ra MessageBox để thấy ngay nếu câu lệnh SQL bị lỗi ràng buộc
                MessageBox.Show("Lỗi lưu dữ liệu MQTT vào Oracle: " + ex.Message, "Lỗi Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
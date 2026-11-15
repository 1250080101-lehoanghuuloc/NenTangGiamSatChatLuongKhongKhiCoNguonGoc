using System;
using System.Data;
using Newtonsoft.Json.Linq; // Nhớ cài NuGet Package Newtonsoft.Json

namespace khongkhi
{
    public class ThuNhanService
    {
        private PhepDoRepository phepDoRepo = new PhepDoRepository();

        // 1. Hàm nhận chuỗi JSON từ MQTT (Wokwi) để parse và xử lý
        public string XuLyBanTinTuWokwi(string payloadJson)
        {
            try
            {
                JObject data = JObject.Parse(payloadJson);

                // Trích xuất dữ liệu từ JSON Wokwi gửi lên
                string maCamBien = data["maCamBien"]?.ToString();
                double giaTri = Convert.ToDouble(data["giaTri"]);
                DateTime tgDo = Convert.ToDateTime(data["tgDo"]);
                DateTime tgNhan = DateTime.Now; // Thời điểm hệ thống C# nhận được tin nhắn

                // Lấy độ trễ dưới dạng số thực (double)
                double doTre = 0;
                if (data["doTreGiay"] != null)
                {
                    double.TryParse(data["doTreGiay"].ToString(), out doTre);
                }

                string trangThaiWokwi = data["trangThai"]?.ToString();
                string lyDoWokwi = data["lyDo"]?.ToString();

                // Gọi lại logic nghiệp vụ kiểm tra hiệu chuẩn và lưu vào DB (Đã bỏ tham số maPhepDo thừa)
                return XuLyThuNhanPhepDo(maCamBien, giaTri, tgDo, tgNhan, doTre);
            }
            catch (Exception ex)
            {
                return "Lỗi parse JSON Wokwi: " + ex.Message;
            }
        }

        // 2. Hàm nghiệp vụ xử lý và gọi Repository (Khớp 100% với định nghĩa mới của LuuPhepDoTransaction)
        public string XuLyThuNhanPhepDo(string maCamBien, double giaTri, DateTime tgDo, DateTime tgNhan, double doTre)
        {
            // Kiểm tra cảm biến tồn tại
            DataTable dtCB = phepDoRepo.LayThongTinCamBien(maCamBien);
            if (dtCB.Rows.Count == 0)
            {
                return "Từ chối: Mã cảm biến không tồn tại trong hệ thống!";
            }
            int maPP = Convert.ToInt32(dtCB.Rows[0]["MaPP"]);

            // Lấy phiên bản hiệu chuẩn phù hợp tại thời điểm đo (TgDo)
            DataTable dtHC = phepDoRepo.LayPhiênBảnHieuChuanTheoThoiDiem(maCamBien, tgDo);
            if (dtHC.Rows.Count == 0)
            {
                return "Từ chối: Không tìm thấy phiên bản hiệu chuẩn hợp lệ tại thời điểm đo!";
            }
            string maPhienBan = dtHC.Rows[0]["MaPhiênBản"].ToString();

            // Đánh giá chất lượng phép đo
            string trangThai = "Đạt";
            string lyDo = "Chỉ số nằm trong ngưỡng cho phép";

            // Lưu phép đo thông qua Transaction (Đúng chuẩn 10 tham số)
            bool success = phepDoRepo.LuuPhepDoTransaction(
                maCamBien,
                maPP,
                maPhienBan,
                giaTri,
                tgDo,
                tgNhan,
                doTre,
                trangThai,
                lyDo,
                out string error
            );

            if (success)
            {
                return "Thành công";
            }
            return "Lỗi giao dịch: " + error;
        }
    }
}
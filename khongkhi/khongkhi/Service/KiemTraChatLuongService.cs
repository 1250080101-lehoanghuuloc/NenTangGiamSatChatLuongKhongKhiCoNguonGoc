using System;
using System.Data;

namespace khongkhi
{
    public class KiemTraChatLuongService
    {
        private PhepDoRepository repo = new PhepDoRepository();

        public void KiemTraChấtLuong(string maCamBien, double giaTriDo, DateTime tgDo, DateTime tgNhan, out string trangThai, out string lyDo)
        {
            trangThai = "Hợp lệ";
            lyDo = "Đạt mọi quy tắc";

            // 1. QuyTacNguongVatLy: Kiểm tra giá trị có nằm ngoài ngưỡng cho phép không (Ví dụ: < 0 hoặc > 500)
            if (giaTriDo < 0 || giaTriDo > 500)
            {
                trangThai = "Không hợp lệ";
                lyDo = "Giá trị vượt ngưỡng vật lý cho phép";
                return; // Vi phạm nghiêm trọng -> Dừng luôn
            }

            // 2. QuyTacDoTre: Kiểm tra độ trễ thời gian nhận so với thời điểm đo (Ví dụ: trễ quá 3600 giây)
            double doTreGiay = (tgNhan - tgDo).TotalSeconds;
            if (doTreGiay > 3600)
            {
                trangThai = "Nghi ngờ";
                lyDo = "Độ trễ thời gian đo vượt quá giới hạn cho phép";
                return;
            }

            // 3. QuyTacDotBien: Kiểm tra biến động đột biến (Có thể so sánh với giá trị kỳ trước nếu cần)
            // Tạm thời giả lập logic kiểm tra đột biến cơ bản
            if (Math.Abs(giaTriDo) > 450)
            {
                trangThai = "Nghi ngờ";
                lyDo = "Phát hiện chỉ số biến động đột biến so với ngưỡng";
                return;
            }

            // 4. QuyTacPhienBanHieuChuan: Kiểm tra xem tại thời điểm đo có phiên bản hiệu chuẩn hiệu lực không
            DataTable dtHC = repo.LayPhiênBảnHieuChuanTheoThoiDiem(maCamBien, tgDo);
            if (dtHC.Rows.Count == 0)
            {
                trangThai = "Nghi ngờ";
                lyDo = "Chưa có phiên bản hiệu chuẩn hiệu lực";
                return;
            }
        }
    }
}
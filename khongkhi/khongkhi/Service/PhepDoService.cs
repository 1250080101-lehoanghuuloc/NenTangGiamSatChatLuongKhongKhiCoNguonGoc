using System;
using System.Data;

namespace khongkhi
{
    public class PhepDoService
    {
        private PhepDoRepository repo = new PhepDoRepository();

        public DataTable LayDanhSachTram()
        {
            return repo.LayDanhSachTram();
        }

        public DataTable LayDanhSachCamBien(string maTram)
        {
            return repo.LayDanhSachCamBienTheoTram(maTram);
        }
        public DataTable LayChiTietNguonGoc(long maPhepDo)
        {
            return repo.LayChiTietNguonGoc(maPhepDo);
        }

        public DataTable LayDuLieuQuanTrac()
        {
            return repo.LayDuLieuQuanTrac();
        }

        public DataTable TimKiem(DateTime tuNgay, DateTime denNgay)
        {
            DateTime tu = tuNgay.Date;
            DateTime den = denNgay.Date.AddDays(1).AddSeconds(-1);
            return repo.TimKiemTheoThoiGian(tu, den);
        }
        public DataTable TimKiemNangCao(string maTram, string loaiChiSo, DateTime tuNgay, DateTime denNgay)
        {
            if (denNgay < tuNgay)
            {
                throw new Exception("Khoảng thời gian không hợp lệ (Đến ngày phải lớn hơn hoặc bằng Từ ngày).");
            }

            DateTime tu = tuNgay.Date;
            DateTime den = denNgay.Date.AddDays(1).AddSeconds(-1);

            return repo.LayDuLieuTheoDieuKien(maTram, loaiChiSo, tu, den);
        }
    }
}
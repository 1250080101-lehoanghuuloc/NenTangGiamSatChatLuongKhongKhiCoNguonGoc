using System;
using System.Data;

namespace khongkhi
{
    public class HieuChuanService
    {
        private HieuChuanRepository repo = new HieuChuanRepository();

        public DataTable LayDanhSachCamBien()
        {
            return repo.LayDanhSachCamBien();
        }

        public DataRow LayPhienBanHienHanh(string maCamBien)
        {
            return repo.LayPhienBanHienHanh(maCamBien);
        }

        public DataTable LayLichSuHieuChuan(string maCamBien)
        {
            return repo.LayLichSuHieuChuan(maCamBien);
        }

        public string TaoPhienBanMoi(string maCamBien, string maPB, DateTime dtBatDau)
        {
            if (string.IsNullOrWhiteSpace(maCamBien))
            {
                return "Vui lòng chọn cảm biến!";
            }

            if (string.IsNullOrWhiteSpace(maPB))
            {
                return "Vui lòng nhập mã phiên bản mới!";
            }

            if (repo.CapNhatPhienBanTransaction(maCamBien, maPB, dtBatDau, out string error))
            {
                return "Thành công";
            }
            return "Lỗi Transaction: " + error;
        }
    }
}
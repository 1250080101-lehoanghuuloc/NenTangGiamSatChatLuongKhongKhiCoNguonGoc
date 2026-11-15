using System.Data;

namespace khongkhi
{
    public class TramService
    {
        private TramRepository repo = new TramRepository();

        public DataTable LayDanhSach()
        {
            return repo.LayDanhSachTram();
        }

        public string ThemMoi(string maTram, string tenTram, string diaChi)
        {
            if (string.IsNullOrWhiteSpace(maTram) || string.IsNullOrWhiteSpace(tenTram))
            {
                return "Lỗi: Mã trạm và tên trạm không được để trống!";
            }

            if (repo.DemTram(maTram) > 0)
            {
                return "Lỗi: Mã trạm đã tồn tại trong hệ thống!";
            }

            repo.ThemMoiTram(maTram, tenTram, diaChi);
            return "Thành công";
        }

        public string CapNhat(string maTram, string tenTram, string diaChi)
        {
            if (string.IsNullOrWhiteSpace(maTram) || string.IsNullOrWhiteSpace(tenTram))
            {
                return "Lỗi: Dữ liệu bắt buộc không được để trống!";
            }

            repo.CapNhatTram(maTram, tenTram, diaChi);
            return "Thành công";
        }

        public string Xoa(string maTram)
        {
            if (string.IsNullOrWhiteSpace(maTram))
            {
                return "Lỗi: Vui lòng chọn trạm cần xóa!";
            }

            repo.XoaTram(maTram);
            return "Thành công";
        }
    }
}
using System.Data;

namespace khongkhi
{
    public class CamBienService
    {
        private CamBienRepository repo = new CamBienRepository();

        public DataTable LayDanhSach()
        {
            return repo.LayDanhSachCamBien();
        }

        public DataTable LayDanhSachTram()
        {
            return repo.LayDanhSachTram();
        }

        // Đảm bảo tên hàm khớp với bên FrmQuanLyCamBien gọi
        public DataTable LayDanhSachPhuongPhap()
        {
            return repo.LayDanhSachPhuongPhapDo();
        }

        public string LuuCamBien(string maCB, string maTram, long maPP, string loaiChiSo, string donVi, bool isUpdate)
        {
            if (string.IsNullOrWhiteSpace(maCB) || string.IsNullOrWhiteSpace(loaiChiSo))
            {
                return "Vui lòng nhập đầy đủ Mã cảm biến và Loại chỉ số!";
            }

            int count = repo.DemCamBien(maCB);

            if (isUpdate)
            {
                repo.CapNhat(maCB, maTram, maPP, loaiChiSo, donVi);
            }
            else
            {
                if (count > 0)
                {
                    return "Mã cảm biến đã tồn tại!";
                }
                repo.ThemMoi(maCB, maTram, maPP, loaiChiSo, donVi);
            }
            return "Thành công";
        }

        public string XoaCamBien(string maCB)
        {
            if (string.IsNullOrWhiteSpace(maCB))
            {
                return "Vui lòng chọn cảm biến cần xóa!";
            }

            try
            {
                repo.Xoa(maCB);
                return "Thành công";
            }
            catch (System.Exception ex)
            {
                return "Không thể xóa cảm biến vì đã phát sinh dữ liệu phép đo liên quan: " + ex.Message;
            }
        }
    }
}
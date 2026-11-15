using khonghkhi;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;

namespace khongkhi
{
    public class PhepDoRepository
    {
        // 1. Lấy danh sách trạm phục vụ ComboBox
        public DataTable LayDanhSachTram()
        {
            DataTable dt = new DataTable();
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string query = "SELECT MaTram, TenTram FROM Tram";
                using (var da = new OracleDataAdapter(query, conn))
                {
                    da.Fill(dt);
                }
            }
            return dt;
        }

        // 2. Lấy danh sách cảm biến theo mã trạm
        public DataTable LayDanhSachCamBienTheoTram(string maTram)
        {
            DataTable dt = new DataTable();
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string query = "SELECT MaCamBien, LoaiChiSo FROM CamBien WHERE MaTram = :matram";
                using (var cmd = new OracleCommand(query, conn))
                {
                    cmd.BindByName = true;
                    cmd.Parameters.Add("matram", OracleDbType.Varchar2).Value = maTram;
                    using (var da = new OracleDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            return dt;
        }

        // 3. Lấy toàn bộ dữ liệu quan trắc (Tận dụng View V_ChiTietPhepDo)
        public DataTable LayDuLieuQuanTrac()
        {
            DataTable dt = new DataTable();
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string query = "SELECT * FROM V_ChiTietPhepDo ORDER BY ThoiDiemDo DESC";
                using (var da = new OracleDataAdapter(query, conn))
                {
                    da.Fill(dt);
                }
            }
            return dt;
        }

        // 4. Tìm kiếm dữ liệu quan trắc theo khoảng thời gian
        public DataTable TimKiemTheoThoiGian(DateTime tuNgay, DateTime denNgay)
        {
            DataTable dt = new DataTable();
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string query = @"SELECT * FROM V_ChiTietPhepDo 
                                 WHERE ThoiDiemDo BETWEEN :tungay AND :denngay
                                 ORDER BY ThoiDiemDo DESC";
                using (var cmd = new OracleCommand(query, conn))
                {
                    cmd.BindByName = true;
                    cmd.Parameters.Add("tungay", OracleDbType.Date).Value = tuNgay;
                    cmd.Parameters.Add("denngay", OracleDbType.Date).Value = denNgay;
                    using (var da = new OracleDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            return dt;
        }

        // 5. Lấy thông tin cảm biến và phương pháp đo
        public DataTable LayThongTinCamBien(string maCamBien)
        {
            DataTable dt = new DataTable();
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string query = "SELECT cb.*, pp.MaPP FROM CAM_BIEN cb JOIN PHUONG_PHAP_DO pp ON cb.MaPP = pp.MaPP WHERE cb.MaCamBien = :macambien";
                using (var cmd = new OracleCommand(query, conn))
                {
                    cmd.BindByName = true;
                    cmd.Parameters.Add("macambien", OracleDbType.Varchar2).Value = maCamBien;
                    using (var da = new OracleDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            return dt;
        }

        // 6. Lấy phiên bản hiệu chuẩn phù hợp với thời điểm đo (TgDo)
        public DataTable LayPhiênBảnHieuChuanTheoThoiDiem(string maCamBien, DateTime tgDo)
        {
            DataTable dt = new DataTable();
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string query = @"SELECT * FROM PHIEN_BAN_HIEU_CHUAN 
                                 WHERE MaCamBien = :macambien 
                                   AND ThoiDiemBatDau <= :tgdo1 
                                   AND (ThoiDiemHetHieuLuc IS NULL OR ThoiDiemHetHieuLuc > :tgdo2)";
                using (var cmd = new OracleCommand(query, conn))
                {
                    cmd.BindByName = true;
                    cmd.Parameters.Add("macambien", OracleDbType.Varchar2).Value = maCamBien;
                    cmd.Parameters.Add("tgdo1", OracleDbType.TimeStamp).Value = tgDo;
                    cmd.Parameters.Add("tgdo2", OracleDbType.TimeStamp).Value = tgDo;
                    using (var da = new OracleDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            return dt;
        }

        // 7. Lấy chi tiết nguồn gốc
        public DataTable LayChiTietNguonGoc(long maPhepDo)
        {
            DataTable dt = new DataTable();
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string query = "SELECT * FROM V_ChiTietPhepDo WHERE MaPhepDo = :id";
                using (var cmd = new OracleCommand(query, conn))
                {
                    cmd.BindByName = true;
                    cmd.Parameters.Add("id", OracleDbType.Int64).Value = maPhepDo;
                    using (var da = new OracleDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            return dt;
        }

        // 8. Lấy dữ liệu theo điều kiện (Nâng cao)
        public DataTable LayDuLieuTheoDieuKien(string maTram, string loaiChiSo, DateTime tuNgay, DateTime denNgay)
        {
            DataTable dt = new DataTable();
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string query = @"SELECT * FROM V_ChiTietPhepDo 
                                 WHERE MaTram = :matram 
                                   AND LoaiChiSo = :loaichiso 
                                   AND ThoiDiemDo BETWEEN :tungay AND :denngay
                                 ORDER BY ThoiDiemDo DESC";
                using (var cmd = new OracleCommand(query, conn))
                {
                    cmd.BindByName = true;
                    cmd.Parameters.Add("matram", OracleDbType.Varchar2).Value = maTram;
                    cmd.Parameters.Add("loaichiso", OracleDbType.Varchar2).Value = loaiChiSo;
                    cmd.Parameters.Add("tungay", OracleDbType.Date).Value = tuNgay;
                    cmd.Parameters.Add("denngay", OracleDbType.Date).Value = denNgay;
                    using (var da = new OracleDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            return dt;
        }

        // 9. Lưu phép đo sử dụng Stored Procedure (Transaction tự động xử lý bên CSDL)
        public bool LuuPhepDoTransaction(string maCamBien, int maPP, string maPhienBan, double giaTri, DateTime tgDo, DateTime tgNhan, double doTre, string trangThai, string lyDon, out string errorMsg)
        {
            errorMsg = "";
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                try
                {
                    using (var cmd = new OracleCommand("SP_ThemPhepDo", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.BindByName = true;

                        cmd.Parameters.Add("p_MaCamBien", OracleDbType.Varchar2).Value = maCamBien;
                        cmd.Parameters.Add("p_MaPhuongPhap", OracleDbType.Int32).Value = maPP;
                        cmd.Parameters.Add("p_MaPhienBan", OracleDbType.Varchar2).Value = (object)maPhienBan ?? DBNull.Value;
                        cmd.Parameters.Add("p_GiaTriDo", OracleDbType.Decimal).Value = giaTri;
                        cmd.Parameters.Add("p_ThoiDiemDo", OracleDbType.TimeStamp).Value = tgDo;
                        cmd.Parameters.Add("p_ThoiDiemNhan", OracleDbType.TimeStamp).Value = tgNhan;
                        cmd.Parameters.Add("p_DoTre", OracleDbType.Decimal).Value = doTre;
                        cmd.Parameters.Add("p_TrangThai", OracleDbType.Varchar2).Value = trangThai;
                        cmd.Parameters.Add("p_LyDo", OracleDbType.NVarchar2).Value = (object)lyDon ?? DBNull.Value;

                        cmd.ExecuteNonQuery();
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    errorMsg = ex.Message;
                    return false;
                }
            }
        }
    }
}
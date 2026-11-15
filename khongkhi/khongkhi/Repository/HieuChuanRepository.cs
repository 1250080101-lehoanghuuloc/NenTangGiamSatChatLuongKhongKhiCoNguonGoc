using khonghkhi;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;

namespace khongkhi
{
    public class HieuChuanRepository
    {
        // 1. Lấy danh sách Cảm biến để đổ vào ComboBox
        public DataTable LayDanhSachCamBien()
        {
            DataTable dt = new DataTable();
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string query = "SELECT MaCamBien, (MaCamBien || ' - ' || LoaiChiSo || ' - ' || MaTram) AS TenCB FROM CamBien";
                using (var da = new OracleDataAdapter(query, conn))
                {
                    da.Fill(dt);
                }
            }
            return dt;
        }

        // 2. Lấy phiên bản hiệu chuẩn đang có hiệu lực
        public DataRow LayPhienBanHienHanh(string maCamBien)
        {
            DataTable dt = new DataTable();
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string query = @"SELECT MaPhienBan, ThoiDiemBatDau 
                                FROM PhienBanHieuChuan 
                                WHERE MaCamBien = :cb AND ThoiDiemHetHieuLuc IS NULL";
                using (var cmd = new OracleCommand(query, conn))
                {
                    cmd.BindByName = true; // Bật bind theo tên
                    cmd.Parameters.Add("cb", OracleDbType.Varchar2).Value = maCamBien; // Đã bỏ dấu :
                    using (var da = new OracleDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            return dt.Rows.Count > 0 ? dt.Rows[0] : null;
        }

        // 3. Lấy lịch sử tất cả phiên bản hiệu chuẩn của Cảm biến
        public DataTable LayLichSuHieuChuan(string maCamBien)
        {
            DataTable dt = new DataTable();
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string query = @"SELECT MaPhienBan, ThoiDiemBatDau, ThoiDiemHetHieuLuc,
                                        CASE WHEN ThoiDiemHetHieuLuc IS NULL THEN 'Đang hiệu lực' ELSE 'Hết hiệu lực' END AS TrangThai
                                 FROM PhienBanHieuChuan
                                 WHERE MaCamBien = :cb
                                 ORDER BY ThoiDiemBatDau DESC";
                using (var cmd = new OracleCommand(query, conn))
                {
                    cmd.BindByName = true; // Bật bind theo tên
                    cmd.Parameters.Add("cb", OracleDbType.Varchar2).Value = maCamBien; // Đã bỏ dấu :
                    using (var da = new OracleDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            return dt;
        }

        // 4. Thực thi Transaction đóng phiên bản cũ và thêm phiên bản mới
        public bool CapNhatPhienBanTransaction(string maCamBien, string maPB, DateTime dtBatDau, out string errorMsg)
        {
            errorMsg = "";
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                try
                {
                    using (var cmd = new OracleCommand("SP_TaoPhienBanHieuChuan", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("p_MaPhienBan", OracleDbType.Varchar2).Value = maPB;
                        cmd.Parameters.Add("p_MaCamBien", OracleDbType.Varchar2).Value = maCamBien;
                        cmd.Parameters.Add("p_ThoiDiemMoi", OracleDbType.TimeStamp).Value = dtBatDau;
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
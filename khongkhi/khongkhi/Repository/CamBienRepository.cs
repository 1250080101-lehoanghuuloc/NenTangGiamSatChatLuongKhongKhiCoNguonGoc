using khonghkhi;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;

namespace khongkhi
{
    public class CamBienRepository
    {
        public DataTable LayDanhSachCamBien()
        {
            DataTable dt = new DataTable();
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string query = @"SELECT cb.MaCamBien, t.TenTram, cb.LoaiChiSo, cb.DonViDo, pp.TenPhuongPhap, cb.MaTram, cb.MaPhuongPhap
                                 FROM CamBien cb
                                 JOIN Tram t ON cb.MaTram = t.MaTram
                                 JOIN PhuongPhapDo pp ON cb.MaPhuongPhap = pp.MaPhuongPhap
                                 ORDER BY cb.MaCamBien";
                using (var da = new OracleDataAdapter(query, conn))
                {
                    da.Fill(dt);
                }
            }
            return dt;
        }

        public DataTable LayDanhSachTram()
        {
            DataTable dt = new DataTable();
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var da = new OracleDataAdapter("SELECT MaTram, TenTram FROM Tram", conn))
                {
                    da.Fill(dt);
                }
            }
            return dt;
        }

        public DataTable LayDanhSachPhuongPhapDo()
        {
            DataTable dt = new DataTable();
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var da = new OracleDataAdapter("SELECT MaPhuongPhap, TenPhuongPhap FROM PhuongPhapDo", conn))
                {
                    da.Fill(dt);
                }
            }
            return dt;
        }

        public int DemCamBien(string maCB)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = new OracleCommand("SELECT COUNT(*) FROM CamBien WHERE MaCamBien = :id", conn))
                {
                    cmd.Parameters.Add(":id", OracleDbType.Varchar2).Value = maCB;
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        public void ThemMoi(string maCB, string maTram, long maPP, string loaiChiSo, string donVi)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string query = "INSERT INTO CamBien (MaCamBien, MaTram, MaPhuongPhap, LoaiChiSo, DonViDo) VALUES (:id, :tram, :pp, :loai, :dv)";
                using (var cmd = new OracleCommand(query, conn))
                {
                    cmd.Parameters.Add(":id", OracleDbType.Varchar2).Value = maCB;
                    cmd.Parameters.Add(":tram", OracleDbType.Varchar2).Value = maTram;
                    cmd.Parameters.Add(":pp", OracleDbType.Int64).Value = maPP;
                    cmd.Parameters.Add(":loai", OracleDbType.Varchar2).Value = loaiChiSo;
                    cmd.Parameters.Add(":dv", OracleDbType.Varchar2).Value = donVi;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void CapNhat(string maCB, string maTram, long maPP, string loaiChiSo, string donVi)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string query = "UPDATE CamBien SET MaTram = :tram, MaPhuongPhap = :pp, LoaiChiSo = :loai, DonViDo = :dv WHERE MaCamBien = :id";
                using (var cmd = new OracleCommand(query, conn))
                {
                    cmd.Parameters.Add(":tram", OracleDbType.Varchar2).Value = maTram;
                    cmd.Parameters.Add(":pp", OracleDbType.Int64).Value = maPP;
                    cmd.Parameters.Add(":loai", OracleDbType.Varchar2).Value = loaiChiSo;
                    cmd.Parameters.Add(":dv", OracleDbType.Varchar2).Value = donVi;
                    cmd.Parameters.Add(":id", OracleDbType.Varchar2).Value = maCB;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Xoa(string maCB)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = new OracleCommand("DELETE FROM CamBien WHERE MaCamBien = :id", conn))
                {
                    cmd.Parameters.Add(":id", OracleDbType.Varchar2).Value = maCB;
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
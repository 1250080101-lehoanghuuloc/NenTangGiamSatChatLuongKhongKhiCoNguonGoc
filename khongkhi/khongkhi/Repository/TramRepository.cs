using khonghkhi;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;

namespace khongkhi
{
    public class TramRepository
    {
        // 1. Lấy danh sách trạm
        public DataTable LayDanhSachTram()
        {
            DataTable dt = new DataTable();
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string query = "SELECT MaTram, TenTram, DiaChi FROM Tram";
                using (var da = new OracleDataAdapter(query, conn))
                {
                    da.Fill(dt);
                }
            }
            return dt;
        }

        // 2. Kiểm tra trùng mã trạm
        public int DemTram(string maTram)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM Tram WHERE MaTram = :matram";
                using (var cmd = new OracleCommand(query, conn))
                {
                    cmd.Parameters.Add(":matram", OracleDbType.Varchar2).Value = maTram;
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        // 3. Thêm trạm mới (chỉ gồm MaTram, TenTram, DiaChi)
        public void ThemMoiTram(string maTram, string tenTram, string diaChi)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string query = "INSERT INTO Tram (MaTram, TenTram, DiaChi) VALUES (:matram, :tentram, :diachi)";
                using (var cmd = new OracleCommand(query, conn))
                {
                    cmd.Parameters.Add(":matram", OracleDbType.Varchar2).Value = maTram;
                    cmd.Parameters.Add(":tentram", OracleDbType.Varchar2).Value = tenTram;
                    cmd.Parameters.Add(":diachi", OracleDbType.Varchar2).Value = diaChi;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // 4. Sửa thông tin trạm
        public void CapNhatTram(string maTram, string tenTram, string diaChi)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string query = "UPDATE Tram SET TenTram = :tentram, DiaChi = :diachi WHERE MaTram = :matram";
                using (var cmd = new OracleCommand(query, conn))
                {
                    cmd.Parameters.Add(":tentram", OracleDbType.Varchar2).Value = tenTram;
                    cmd.Parameters.Add(":diachi", OracleDbType.Varchar2).Value = diaChi;
                    cmd.Parameters.Add(":matram", OracleDbType.Varchar2).Value = maTram;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // 5. Xóa trạm
        public void XoaTram(string maTram)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string query = "DELETE FROM Tram WHERE MaTram = :matram";
                using (var cmd = new OracleCommand(query, conn))
                {
                    cmd.Parameters.Add(":matram", OracleDbType.Varchar2).Value = maTram;
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
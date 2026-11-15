using System;
using System.Data;
using Oracle.ManagedDataAccess.Client;

namespace khonghkhi
{
    public static class DatabaseHelper
    {
        // Thay đổi thông tin DataSource/User Id/Password cho khớp với Oracle của bạn
        // Nếu dùng localhost và PDB (như XEPDB1), chuỗi kết nối thường có dạng:
        private static string connectionString = "User Id=system;Password=12345;Data Source=localhost:1521/orcl21;";

        // Hoặc nếu kết nối cơ bản:
        // private static string connectionString = "User Id=system;Password=your_password;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=ORCL)));";

        public static OracleConnection GetConnection()
        {
            OracleConnection conn = new OracleConnection(connectionString);
            return conn;
        }

        // Hàm kiểm tra kết nối nhanh
        public static bool TestConnection()
        {
            try
            {
                using (var conn = GetConnection())
                {
                    conn.Open();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi kết nối: " + ex.Message);
                return false;
            }
        }
    }
}
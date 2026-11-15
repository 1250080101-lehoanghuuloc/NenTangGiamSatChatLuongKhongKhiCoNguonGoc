using khonghkhi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace khongkhi
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            while (true)
            {
                FrmDangNhap loginForm = new FrmDangNhap();

                // Mở form đăng nhập dưới dạng hộp thoại chờ kết quả trả về từ DialogResult.OK
                if (loginForm.ShowDialog() == DialogResult.OK)
                {
                    // Nếu đăng nhập đúng, mở giao diện chính và giữ vòng đời ứng dụng tại đây
                    FrmMain mainForm = new FrmMain();
                    Application.Run(mainForm);
                }
                else
                {
                    // Nếu tắt form đăng nhập hoặc bấm thoát, kết thúc hoàn toàn chương trình
                    break;
                }
            }

        }
    }
}

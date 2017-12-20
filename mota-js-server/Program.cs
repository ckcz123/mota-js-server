using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace mota_js_server
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool createdNew;
            System.Threading.Mutex instance = new System.Threading.Mutex(true, Application.ProductName, out createdNew);
            if (createdNew)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
                instance.ReleaseMutex();
            }
            else
            {
                MessageBox.Show("您已启动了一个本地服务器！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

        }
    }
}

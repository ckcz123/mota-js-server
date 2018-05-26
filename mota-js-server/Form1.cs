using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Diagnostics;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading;
using SimpleHttpServer;
using SimpleHttpServer.Models;
using SimpleHttpServer.RouteHandlers;


namespace mota_js_server
{
    public partial class Form1 : Form
    {
        private Thread thread;

        public Form1()
        {
            InitializeComponent();

            if (portInUse(1055))
            {
                label1.Text = "服务启动失败：1055端口已被占用！";
            }
            else
            {
                // 启动
                HttpServer httpServer = new HttpServer(1055, new List<Route>()
                {
                    new Route()
                    {
                        Callable = new FileSystemRouteHandler() {BasePath = ".", ShowDirectories = true}.Handle,
                        UrlRegex = "^/(.*)$",
                        Method = "GET"
                    },
                    new Route()
                    {
                        Callable = MyRoute.route,
                        UrlRegex = "^/(.*)$",
                        Method = "POST"
                    },
                });
                thread = new Thread(new ThreadStart(httpServer.Listen));
                thread.Start();
                label1.Text = "已启动服务：http://127.0.0.1:1055/";
            }
        }

        private bool portInUse(int port)
        {
            IPGlobalProperties ipGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();
            IPEndPoint[] ipEndPoints = ipGlobalProperties.GetActiveTcpListeners();
            foreach (IPEndPoint ipEndPoint in ipEndPoints)
            {
                if (ipEndPoint.Port == port) return true;
            }
            return false;
        }


        private bool checkChrome()
        {
            RegistryKey browserKeys = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Clients\StartMenuInternet");
            if (browserKeys == null)
                browserKeys = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Clients\StartMenuInternet");
            string[] names = browserKeys.GetSubKeyNames();
            foreach (string name in names)
            {
                if (name.ToLower().Contains("chrome"))
                    return true;
            }
            return false;
        }

        private void openUrl(string url)
        {
            
            if (checkChrome())
            {
                try
                {
                    Process.Start("chrome.exe", url);
                    return;
                }
                catch (Exception)
                {
                }
            }

            MessageBox.Show("你当前没有安装Chrome浏览器，使用其他浏览器可能会导致本程序闪退或无法正常工作，强烈推荐下载Chrome浏览器后再进行操作。",
                "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            Process.Start(url);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openUrl("http://127.0.0.1:1055/index.html");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            openUrl("http://127.0.0.1:1055/editor.html");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!File.Exists("常用工具\\便捷PS工具.exe"))
            {
                MessageBox.Show("找不到常用工具目录下的便捷PS工具！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Process.Start("常用工具\\便捷PS工具.exe");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (!File.Exists("常用工具\\地图生成器.exe"))
            {
                MessageBox.Show("找不到常用工具目录下的地图生成器！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Process.Start("常用工具\\地图生成器.exe");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (!File.Exists("常用工具\\JS代码压缩工具.exe"))
            {
                MessageBox.Show("找不到常用工具目录下的JS代码压缩工具！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Process.Start("常用工具\\JS代码压缩工具.exe");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (!File.Exists("常用工具\\伤害和临界值计算器.exe"))
            {
                MessageBox.Show("找不到常用工具目录下的伤害和临界值计算器！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Process.Start("常用工具\\伤害和临界值计算器.exe");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (!File.Exists("常用工具\\RM动画导出器.exe"))
            {
                MessageBox.Show("找不到常用工具目录下的RM动画导出器！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Process.Start("常用工具\\RM动画导出器.exe");
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                Environment.Exit(System.Environment.ExitCode);
            }
            catch (Exception)
            {

            }
        }
    }
}

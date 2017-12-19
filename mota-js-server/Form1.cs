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


namespace mota_js_server
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
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
            Process.Start(url);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openUrl("http://127.0.0.1:1055/index.html");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            openUrl("http://127.0.0.1:1055/drawMapGUI.html");
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
    }
}

using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace BankAdviser.BLL.Infrastructure
{
    public static class WebDriverManager
    {
        private const int SW_SHOWMINIMIZED = 2;

        [DllImport("user32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);

        public static Task MinimizeChromeDriverWndAsync()
        {
            return Task.Run(() =>
            {
                IntPtr chromeDrvWnd = new IntPtr();
                string chromeDrvWndTitle = AppDomain.CurrentDomain.BaseDirectory + @"chromedriver.exe";
                Process[] pros = Process.GetProcesses(".");
                foreach (Process p in pros)
                    if (p.MainWindowTitle.ToUpper().Contains(chromeDrvWndTitle.ToUpper()))
                        chromeDrvWnd = p.MainWindowHandle;
                if (!chromeDrvWnd.Equals(IntPtr.Zero))
                {
                    ShowWindowAsync(chromeDrvWnd, SW_SHOWMINIMIZED);
                }
            });            
        }
    }
}
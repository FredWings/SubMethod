using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;

namespace NoWindowWndProc
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool PostThreadMessage(int threadId, uint msg, IntPtr wParam, IntPtr lParam);

        public const uint WM_APP = 0x9112;

        public static Process GetCurrentExeProcess()
        {
            Process targetProcess = null;

            Process currentProcess = Process.GetCurrentProcess();
            string exeName = string.Format("{0}.exe", currentProcess.ProcessName);

            try
            {
                Process[] aryProcess = Process.GetProcessesByName(currentProcess.ProcessName);

                foreach (Process process in aryProcess)
                {
                    if (process.Id != currentProcess.Id && process.ProcessName == currentProcess.ProcessName)
                    {
                        targetProcess = process;
                        break;
                    }
                }
            }
            catch (System.PlatformNotSupportedException pEx)
            {

            }
            catch (System.InvalidOperationException iEx) { }
            catch (System.ComponentModel.Win32Exception win32Ex) { }
            catch (Exception ex) { }

            return targetProcess;
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            Process targetProcess = GetCurrentExeProcess();

            if (targetProcess != null)
            {
                if (targetProcess.Threads.Count > 0)
                {
                    int targetMainThreadID = targetProcess.Threads[0].Id;

                    bool result = PostThreadMessage(targetMainThreadID, WM_APP, IntPtr.Zero, IntPtr.Zero);
                }

                Environment.Exit(0);
            }
            else
            {
                ComponentDispatcher.ThreadPreprocessMessage += ComponentDispatcher_ThreadPreprocessMessage;
            }

        }

        private void ComponentDispatcher_ThreadPreprocessMessage(ref MSG msg, ref bool handled)
        {
            int m = (int)App.WM_APP;
            if (msg.message == m)
                MessageBox.Show("收到消息了!");
        }
    }
}

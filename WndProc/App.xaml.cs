using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;

namespace WndProc
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        #region Dll Imports
        [DllImport("user32.dll")]
        static extern IntPtr PostMessage(IntPtr hwnd, uint msg, IntPtr wParam, IntPtr lParam);

        public const uint WM_APP = 0x9122;
        #endregion Dll Imports

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
                PostMessage(targetProcess.MainWindowHandle, WM_APP, IntPtr.Zero, IntPtr.Zero);
            }
        }
    }


}

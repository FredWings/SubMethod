using System;
using System.Windows;
using System.Windows.Interop;

namespace WndProc
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            ((HwndSource)PresentationSource.FromVisual(this)).AddHook(myHook);
        }

        private IntPtr myHook(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            int m = (int)App.WM_APP;
            if (msg == m)
            {
                MessageBox.Show("Haha");
            }
            return IntPtr.Zero;
        }
    }
}

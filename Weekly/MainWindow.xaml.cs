using MahApps.Metro.Controls;
using System.Windows;
using Weekly.ViewModel;

namespace Weekly
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        #region 私有变量
        
        #endregion

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainViewModel();
        }


        private void Import_Click(object sender, RoutedEventArgs e)
        {

        }
    }



}

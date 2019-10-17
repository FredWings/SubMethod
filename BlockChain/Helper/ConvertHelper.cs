using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace BlockChain.Helper
{
    /// <summary>
    /// 盈亏颜色转换
    /// </summary>
    public class ProfitConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double money = 0.0;
            SolidColorBrush scb = new SolidColorBrush(Colors.LawnGreen);
            if (double.TryParse(value.ToString(), out money) & money < 0)
                scb = new SolidColorBrush(Colors.Red);
            return scb;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class MoneyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((decimal)value).ToString("#,##0.00");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}

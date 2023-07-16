using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace ElasticView.UI.Pages
{
    public class IndexPageHealthForegroundConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var status = values[0] as string;
            var health = values[1] as string;
            if (status == "open")
            {
                if (health.ToLower() == "green")
                {
                    return Brushes.Green;
                }
                else if (health.ToLower() == "yellow")
                {
                    return Brushes.Orange;
                }
                else
                {
                    return Brushes.Red;
                }
                //new object[] {}
            }

            return Brushes.Black;

        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class IndexPageHealthTextConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var status = values[0] as string;
            var health = values[1] as string;
            if (status == "open")
            {
                return health;
                //if (health.ToLower() == "green")
                //{
                //    return new object[] { Brushes.Green, health };
                //}
                //else if (health.ToLower() == "yellow")
                //{
                //    return new object[] { Brushes.Yellow, health };
                //}
                //else
                //{
                //    return new object[] { Brushes.Red, health };
                //}
                //new object[] {}
            }

            return string.Empty;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class IndexPageOpenButtonVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string status = value as string;
            return status.ToLower() == "close" ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class IndexPageCloseButtonVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string status = value as string;
            return status.ToLower() == "open" ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

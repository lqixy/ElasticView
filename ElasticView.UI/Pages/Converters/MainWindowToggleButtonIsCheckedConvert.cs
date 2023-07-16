using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ElasticView.UI.Pages
{
    public class MainWindowToggleButtonIsCheckedConvert : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            int selecedIndex = (int)values[0];
            int pageIndex = (int)values[1];

            return selecedIndex == pageIndex;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            bool isChecked = (bool)value;

            if(isChecked)
            {
                return new object[] { true, "Checked" };
            }
            else
            {
                return new object[] { false, "Unchecked" }; // 返回多个值
            }

            //throw new NotImplementedException();
            //throw new NotSupportedException();
        }
    }
}

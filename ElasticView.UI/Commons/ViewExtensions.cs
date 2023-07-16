using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElasticView.UI.Commons
{
    public static class ViewExtensions
    {
        public static string BytesToString(this long value)
        {
            double max = value / 1024 / 1024;
            var unit = "M";
            if (max >= 1024)
            {
                max = Math.Round((double)max / 1024, 2);
                unit = "G";
            }
            return $"{max}{unit}";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dividend"></param>
        /// <param name="divisor"></param>
        /// <param name="percent">默认百分比</param>
        /// <param name="digits">保留位数</param>
        /// <returns></returns>
        public static double GetPercent(long dividend, long divisor, int percent = 100, int digits = 2)
        {
            if (divisor == 0) return 0;
            return Math.Round((double)dividend / divisor * percent, digits);
        }
    }
}

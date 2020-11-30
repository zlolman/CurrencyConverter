using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;
using System.Text.RegularExpressions;

namespace CurrencyConverter 
{
    public class Converter : IValueConverter
    {
        public object Convert(object value, Type targetType, object paremeter, string language)
        {
            if (!String.IsNullOrEmpty(value.ToString()))
            {
                value = value.ToString().Replace(".", ",");
                double source = System.Convert.ToDouble(value.ToString());
                return Math.Round(source / CalculatePage.koef, 2).ToString().Replace(",", ".");
            }
            else 
            {
                return "";
            }            
        }

        public object ConvertBack(object value, Type targetType, object paremeter, string language)
        {
            if (!String.IsNullOrEmpty(value.ToString()))
            {
                value = value.ToString().Replace(".", ",");
                double source = System.Convert.ToDouble(value.ToString());
                double target = Math.Round(source / CalculatePage.koef, 2);
                return Math.Round(source * CalculatePage.koef, 2).ToString().Replace(",", ".");
            }
            else
            {
                return "";
            }
        }
    }
}

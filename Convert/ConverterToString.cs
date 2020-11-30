using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;
using System.Text.RegularExpressions;

namespace CurrencyConverter 
{
    public class ConverterToString : IValueConverter
    {
        public object Convert(object value, Type targetType, object paremeter, string language)
        {
            if (value != null)
            {
                if (value.ToString() == "") 
                {
                    value = "0";
                }
                value = value.ToString().Replace(".", ",");
                double source = System.Convert.ToDouble(value.ToString());
                double target = Math.Round(source *CalculatePage.koef, 2);
                return target.ToString();
            }
            else 
            {
                return "0";
            }
        }

        public object ConvertBack(object value, Type targetType, object paremeter, string language)
        {
            if (value != null)
            {
                if (value.ToString() == "")
                {
                    value = "0";
                }
                value = value.ToString().Replace(".", ",");
                double source = System.Convert.ToDouble(value.ToString());
                double target = Math.Round(source / CalculatePage.koef, 2);
                return target.ToString();
            }
            else
            {
                return "0";
            }
        }
    }
}

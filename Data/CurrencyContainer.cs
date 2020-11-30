using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;
using System.Collections.Generic;
namespace CurrencyConverter.Data
{
    public static class CurrencyContainer
    {
        static public List<Currency> Currencies { get; set; }
        public delegate void Change();
        public static Change del;

    }
}

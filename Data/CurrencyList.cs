using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;
namespace CurrencyConverter.Data
{
    public static class CurrencyList
    {
        static public ObservableCollection<Currency> Currencies { get; set; }
        public delegate void Change();
        public static Change del;
    }
}

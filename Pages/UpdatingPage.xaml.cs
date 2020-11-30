using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using CurrencyConverter.Data;
using System.Collections.Specialized;
using System.Collections.ObjectModel;

namespace CurrencyConverter
{
    public sealed partial class UpdatingPage : Page
    {
        delegate void Change(object sender, NotifyCollectionChangedEventArgs e);
        public UpdatingPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e) //загрузка данных
        {
            CurrencyContainer.del += Downloaded;
            IDataSource jsonSource = new JsonSource(); 
            jsonSource.GetCurrencyList(CurrencyContainer.del); //передача в качестве параметра делегата перехода окна
        }

        private void Downloaded()
        {
            Frame.Navigate(typeof(CalculatePage));
        }

    }
}

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

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace CurrencyConverter
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class UpdatingPage : Page
    {
        delegate void Change(object sender, NotifyCollectionChangedEventArgs e);
        public UpdatingPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e) 
        {
            CurrencyList.del += Downloaded;
            IDataSource jsonSource = new JsonSource();
            jsonSource.GetCurrencyList(CurrencyList.del);
        }

        private async void Downloaded()
        {
            Frame.Navigate(typeof(MainPage));
        }

    }
}

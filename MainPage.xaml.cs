using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
using System.Net;
using CurrencyConverter.Data;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x419

namespace CurrencyConverter
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        static Currency leftCurrency, rightCurrency;
        static public double koef;
        public MainPage()
        {
            this.InitializeComponent();
            if ((leftCurrency == null) && (rightCurrency == null))
            {
                leftCurrency = CurrencyList.Currencies.ToList().Find(value => value.CharCode == "RUB");
                rightCurrency = CurrencyList.Currencies.ToList().Find(value => value.CharCode == "USD");
                leftCurrencyName.Text = leftCurrency.CharCode + "\n" + leftCurrency.Name;
                rightCurrencyName.Text = rightCurrency.CharCode + "\n" + rightCurrency.Name;
                NewKoef();
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {
                ValueTuple<string, Currency> pair = (ValueTuple<string, Currency>)e.Parameter;
                if (pair.Item1 == "left")
                {
                    leftCurrency = pair.Item2;
                    leftCurrencyName.Text = leftCurrency.CharCode + "\n" + leftCurrency.Name;
                    rightCurrencyName.Text = rightCurrency.CharCode + "\n" + rightCurrency.Name;
                    NewKoef();
                };
                if (pair.Item1 == "right")
                {
                    rightCurrency = pair.Item2;
                    rightCurrencyName.Text = rightCurrency.CharCode + "\n" + rightCurrency.Name;
                    leftCurrencyName.Text = leftCurrency.CharCode + "\n" + leftCurrency.Name;
                    NewKoef();
                }
            }
        }
        private void Left_valute_button(object sender, RoutedEventArgs e)
        {
            var pair = ("left", leftCurrency);
            Frame.Navigate(typeof(ListOfCurrencyPage), pair);
        }

        private void Right_valute_button(object sender, RoutedEventArgs e)
        {
            var pair = ("right", rightCurrency);
            Frame.Navigate(typeof(ListOfCurrencyPage), pair);
        }

        void NewKoef() 
        {
            koef = (leftCurrency.Value * (double)rightCurrency.Nominal) / ((double)leftCurrency.Nominal * rightCurrency.Value);
        }
    }
}

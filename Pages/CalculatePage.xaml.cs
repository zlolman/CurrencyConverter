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

namespace CurrencyConverter
{
    
    public sealed partial class CalculatePage : Page
    {
        const string DEFAULTLEFTCURRENCY = "RUB";
        const string DEFAULTRIGHTCURRENCY = "USD";
        static Currency leftCurrency, rightCurrency;
        static public double koef;
        public CalculatePage()
        {
            this.InitializeComponent();
            if ((leftCurrency == null) && (rightCurrency == null))
            {
                leftCurrency = CurrencyList.Currencies.Find(value => value.CharCode == DEFAULTLEFTCURRENCY);
                rightCurrency = CurrencyList.Currencies.Find(value => value.CharCode == DEFAULTRIGHTCURRENCY);
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
            var pair = ("left", leftCurrency); //выбор валюты
            Frame.Navigate(typeof(ListOfCurrencyPage), pair);
        }

        private void Right_valute_button(object sender, RoutedEventArgs e)
        {
            var pair = ("right", rightCurrency); //выбор валюты
            Frame.Navigate(typeof(ListOfCurrencyPage), pair);
        }

        private void TextBox_BeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            args.Cancel = args.NewText.Any(c => (!char.IsDigit(c)) && ((char)c != '.')); //исключении всех символов, кроме "," "." и цифр
            if (!String.IsNullOrEmpty(args.NewText)) 
            {
                char firstSymbol = args.NewText.First();
                if (firstSymbol == '.') //проверка на корректность первого символа
                {
                    args.Cancel = true;
                }
                if (args.NewText.Last() == '.') //проверка на повторение "."
                {
                    if (args.NewText.IndexOf('.', 0) != (args.NewText.Length - 1))
                    {
                        args.Cancel = true;
                    }                    
                }
            } 
        }

        private void NewKoef() //вычисления коофицента перевода валюты
        {
            koef = (leftCurrency.Value * (double)rightCurrency.Nominal) / ((double)leftCurrency.Nominal * rightCurrency.Value); 
        }
    }
}

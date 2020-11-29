using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Text.Json;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;


namespace CurrencyConverter.Data
{
    class JsonSource: IDataSource
    {
        private static readonly HttpClient client = new HttpClient();
        //private string jsonString = "";

        private async Task<string> GetFromWeb() {
            try
            {
                HttpResponseMessage response = await client.GetAsync("https://www.cbr-xml-daily.ru/daily_json.js");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();                
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
                return "";
            }
        }

        private string JsonStringHandling(string jsonString)
        {
            jsonString = jsonString.Substring(jsonString.IndexOf("Valute") + 11);
            jsonString = jsonString.Substring(0, jsonString.Length - 6);
            jsonString = Regex.Replace(jsonString, @"[^0-9a-zA-Z,а-яА-Я]\w\w\w[^0-9a-zA-Z,а-яА-Я][^0-9a-zA-Z,а-яА-Я]\s", "");
            jsonString = "[\n" + jsonString + "\n]";
            return jsonString;
        }

        private List<Currency> Deserialization(string jsonString)  
        {
            return JsonSerializer.Deserialize<List<Currency>>(jsonString); 
        }

        public async void GetCurrencyList(CurrencyList.Change change) 
        {
            CurrencyList.Currencies = new ObservableCollection<Currency> (Deserialization(JsonStringHandling(await GetFromWeb())));
            Currency RUB = new Currency() { ID = "01", CharCode = "RUB", Name = "Российских рублей", Nominal = 1, NumCode = "643", Value = 1.0, Previous = 1.0 };
            CurrencyList.Currencies.Add(RUB);
            change.Invoke();
        }
    }
}

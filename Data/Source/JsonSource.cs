using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Text.Json;
using System.Collections;
using System.Linq;

namespace CurrencyConverter.Data
{
    class JsonSource: IDataSource
    {
        private static readonly HttpClient client = new HttpClient();
        async Task<string> GetFromWeb() //получение данных https://www.cbr-xml-daily.ru/daily_json.js
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync("https://www.cbr-xml-daily.ru/daily_json.js");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception e)
            {
                return "";
            }
        }

        private string JsonStringHandling(string jsonString) //подготовка json 
        { 
            try
            {
                jsonString = jsonString.Substring(jsonString.IndexOf("Valute") + 11); //удаление лишних частей из json
                jsonString = jsonString.Substring(0, jsonString.Length - 6);
                jsonString = Regex.Replace(jsonString, @"[^0-9a-zA-Z,а-яА-Я]\w\w\w[^0-9a-zA-Z,а-яА-Я][^0-9a-zA-Z,а-яА-Я]\s", "");
                jsonString = "[\n" + jsonString + "\n]";
                return jsonString;
            }
            catch (Exception e)
            {
                return "";
            }           
        }
        public async void GetCurrencyList(CurrencyContainer.Change change) //десериализация
        {
            try
            {
                CurrencyContainer.Currencies = JsonSerializer.Deserialize<List<Currency>>(JsonStringHandling(await GetFromWeb()));
                Currency RUB = new Currency() { ID = "01", CharCode = "RUB", Name = "Российских рублей", Nominal = 1, NumCode = "643", Value = 1.0, Previous = 1.0 };
                CurrencyContainer.Currencies.Add(RUB);
                CurrencyContainer.Currencies = CurrencyContainer.Currencies.OrderBy(u => u.Name).ToList();
                change.Invoke(); //вызов делегата для переключения окна
            }
            catch 
            {
                return;
            }
            
        }
    }
}

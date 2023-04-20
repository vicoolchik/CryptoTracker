using CryptoTracker.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace CryptoTracker.Services
{
    public class CurrencyService : ICurrencyService
    {
        private readonly HttpClient _httpClient;

        public CurrencyService()
        {
            _httpClient = new HttpClient { BaseAddress = new Uri("https://api.coingecko.com/api/v3/") };
        }

        public async Task<List<Currency>> GetTopNCurrenciesAsync(int n)
        {
            var response = await _httpClient.GetAsync($"search/trending");
            response.EnsureSuccessStatusCode();
            var jsonString = await response.Content.ReadAsStringAsync();
            var responseObject = JsonConvert.DeserializeObject<Dictionary<string, List<Dictionary<string, object>>>>(jsonString);
            var currencies = new List<Currency>();

            foreach (var item in responseObject["coins"])
            {
                if (currencies.Count >= n)
                {
                    break;
                }

                var itemDictionary = ((JObject)item["item"]).ToObject<Dictionary<string, JToken>>();

                currencies.Add(new Currency
                {
                    Id = itemDictionary["id"].ToString(),
                    Name = itemDictionary["name"].ToString(),
                    Symbol = itemDictionary["symbol"].ToString(),
                });
            }

            return currencies;
        }
        public async Task<CurrencyDetail> GetCurrencyDetailAsync(string id)
        {
            var response = await _httpClient.GetAsync($"coins/{id}");
            response.EnsureSuccessStatusCode();
            var jsonString = await response.Content.ReadAsStringAsync();
            var responseObject = JObject.Parse(jsonString);

            var currencyDetail = new CurrencyDetail
            {
                Id = responseObject["id"].ToString(),
                Name = responseObject["name"].ToString(),
                Symbol = responseObject["symbol"].ToString(),
                Price = Convert.ToDecimal(responseObject["market_data"]["current_price"]["usd"]),
                Volume = Convert.ToDecimal(responseObject["market_data"]["total_volume"]["usd"]),
                PriceChange = Convert.ToDecimal(responseObject["market_data"]["price_change_percentage_24h"]),
                Markets = new List<Market>()
            };

            var tickers = responseObject["tickers"] as JArray;

            foreach (var item in tickers)
            {
                var market = item["market"].ToObject<JObject>();

                currencyDetail.Markets.Add(new Market
                {
                    Id = market["identifier"].ToString(),
                    Name = market["name"].ToString(),
                    Price = Convert.ToDecimal(item["converted_last"]["usd"])
                });
            }


            return currencyDetail;
        }


        public async Task<List<Currency>> SearchCurrenciesAsync(string searchTerm)
        {
            var response = await _httpClient.GetAsync($"search?query={searchTerm}");
            response.EnsureSuccessStatusCode();
            var jsonString = await response.Content.ReadAsStringAsync();
            var responseObject = JsonConvert.DeserializeObject<Dictionary<string, List<Dictionary<string, object>>>>(jsonString);
            var currencies = new List<Currency>();

            foreach (var item in responseObject["coins"])
            {
                currencies.Add(new Currency
                {
                    Id = item["id"].ToString(),
                    Name = item["name"].ToString(),
                    Symbol = item["symbol"].ToString(),
                });
            }

            return currencies;
        }
    }
}

using Application.Common.Interfaces;
using System.Net.Http.Json;

namespace Infrastructure.Services.CurrencyConverters
{
    internal sealed class CurrencyConverter : ICurrencyConverter
    {
        private readonly HttpClient _httpClient;

        public CurrencyConverter(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<decimal> GetConversionRate(decimal amount, string from, string to)
        {
            if (from == to) return amount;

            var uriString = 
                string.Format($"https://api.frankfurter.app/latest?amount={@amount}&from={@from}&to={@to}", amount, from, to);

            var response = await _httpClient.GetAsync(uriString);
            response.EnsureSuccessStatusCode();

            var serializedResponse = await response.Content.ReadFromJsonAsync<ConversionRate>();

            return serializedResponse.Rates.Values.First();
        }
    }
}

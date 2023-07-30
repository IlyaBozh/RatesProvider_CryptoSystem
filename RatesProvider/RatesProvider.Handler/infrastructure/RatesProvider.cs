
using CryptoSystem_NuGetPackage.Enums;
using Newtonsoft.Json.Linq;

namespace RatesProvider.Handler;

public static class RatesProvider 
{
    public static async Task<Dictionary<string, decimal>> GetCryptoRatesAsync(List<string> cryptoPairs)
    {
        var httpClient = new HttpClient();
        var url = $"https://min-api.cryptocompare.com/data/pricemulti?fsyms={string.Join(",", cryptoPairs)}&tsyms=USD";
        var response = await httpClient.GetAsync(url);
        var content = await response.Content.ReadAsStringAsync();
        var json = JObject.Parse(content);

        var rates = new Dictionary<string, decimal>();

        foreach (var pair in cryptoPairs)
        {
            var price = json[pair]["USD"].Value<decimal>();
            rates.Add($"{pair}/USD", price);
        }

        return rates;
    }
}

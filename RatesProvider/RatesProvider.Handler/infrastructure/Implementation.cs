using RatesProvider.Handler.Interfaces;
using Microsoft.Extensions.Logging;

namespace RatesProvider.Handler;
public class Implementation : IImplementation
{
    ICurrencyHandler _currencyHandle;
    ILogger _logger;

    public Implementation(ICurrencyHandler currencyHandle, ILogger<Implementation> logger)
    {
        _logger = logger;
        _currencyHandle = currencyHandle;
    }

    public async Task Run()
    {
        var cryptoPairs = new List<string> { "BTC", "ETH", "LTC" };

        while (true)
        {
            await _currencyHandle.HandleRatesAsync(cryptoPairs);

            await Task.Delay(TimeSpan.FromSeconds(5));
        }
    }
}

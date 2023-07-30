using System.Timers;

namespace RatesProvider.Handler.Interfaces;

public interface ICurrencyHandler
{
    Task HandleRatesAsync(List<string> cryptoPairs);
}

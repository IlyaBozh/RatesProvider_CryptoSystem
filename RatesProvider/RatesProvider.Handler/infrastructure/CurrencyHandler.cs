
using Microsoft.Extensions.Logging;
using RatesProvider.Handler.Interfaces;
using IncredibleBackend.Messaging.Interfaces;
using CryptoSystem_NuGetPackage.Events;

namespace RatesProvider.Handler;

public class CurrencyHandler : ICurrencyHandler
{
    private readonly IMessageProducer _messageProducer;
    private readonly ILogger _logger;
    private NewRatesEvent _result;

    public CurrencyHandler(ILogger<CurrencyHandler> logger,
        IMessageProducer messageProducer)
    {
        _messageProducer = messageProducer;
        _logger = logger;
        _result = new NewRatesEvent();
    }

    public async Task HandleRatesAsync(List<string> cryptoPairs)
    {
        var rates = await RatesProvider.GetCryptoRatesAsync(cryptoPairs);

        foreach (var rate in rates)
        {
            Console.WriteLine($"{rate.Key}: {rate.Value}");
        }

        _result.Rates = rates;

        await _messageProducer.ProduceMessage(_result, "Send rates to queue");
    }
}
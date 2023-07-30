using CryptoSystem_NuGetPackage.Events;
using IncredibleBackend.Messaging;
using IncredibleBackend.Messaging.Extentions;
using IncredibleBackend.Messaging.Interfaces;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using RatesProvider.Handler.Interfaces;

namespace RatesProvider.Handler.Extensions;

public static class ServiceCollectionExtension
{
    public static void ConfigureDependencies(this IServiceCollection services)
    {
        services.AddScoped<IImplementation, Implementation>();
        services.AddScoped<ICurrencyHandler, CurrencyHandler>();
        services.AddScoped<IMessageProducer, MessageProducer>();
    }

    public static void ConfigureMessaging(this IServiceCollection services)
    {
        services.RegisterConsumersAndProducers(
               null,
               null,
                (cfg) => { 
                    cfg.RegisterProducer<NewRatesEvent>("cryptoRates");
                });
    }
}

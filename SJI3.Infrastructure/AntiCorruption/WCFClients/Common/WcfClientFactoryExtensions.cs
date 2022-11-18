using System;
using Microsoft.Extensions.DependencyInjection;

namespace SJI3.Infrastructure.AntiCorruption.WCFClients.Common;

public static class WcfClientFactoryExtensions
{
    public static void AddWcfClient<T>(this IServiceCollection services, Func<IServiceProvider, WcfServiceClientOptions> settingsAction) where T : class
    { 
        services.AddSingleton(provider => new WcfServiceClientFactory<T>(settingsAction?.Invoke(provider)).CreateChannel());
    }
}
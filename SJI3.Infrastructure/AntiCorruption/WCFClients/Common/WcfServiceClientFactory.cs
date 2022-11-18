using System;
using System.ServiceModel;

namespace SJI3.Infrastructure.AntiCorruption.WCFClients.Common;

public class WcfServiceClientFactory<T>
{
    private readonly WcfServiceClientOptions _options;

    public WcfServiceClientFactory(WcfServiceClientOptions options)
    {
        _options = options;
    }
    
    public T CreateChannel()
    {
        var endpointAttribute = (ServiceEndpointAttribute) Attribute.GetCustomAttribute(typeof(T), typeof(ServiceEndpointAttribute));

        if (endpointAttribute is null)
            throw new ArgumentException($"Decorate {typeof(T)} with ServiceEndpoint attribute");
        
        return new ChannelFactory<T>(new BasicHttpBinding(), new EndpointAddress($"{_options.ServiceUrl}{endpointAttribute.Endpoint}"))
            .CreateChannel();
    }
}
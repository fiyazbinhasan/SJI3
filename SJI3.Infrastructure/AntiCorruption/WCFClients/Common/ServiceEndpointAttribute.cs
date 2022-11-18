using System;

namespace SJI3.Infrastructure.AntiCorruption.WCFClients.Common;

[AttributeUsage(AttributeTargets.Interface)]
public class ServiceEndpointAttribute : Attribute
{
    public ServiceEndpointAttribute(string endpoint)
    {
        Endpoint = endpoint;
    }

    public string Endpoint
    {
        get;
    }
}
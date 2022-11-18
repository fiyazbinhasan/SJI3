using System.ServiceModel;
using SJI3.Infrastructure.AntiCorruption.WCFClients.Common;

namespace SJI3.Infrastructure.AntiCorruption.WCFClients.Services;

[ServiceEndpoint("/Design_Time_Addresses/WcfServiceLibrary/Service/")]
[ServiceContract]
public interface IService
{
    [OperationContract]
    string GetData(int value);
}
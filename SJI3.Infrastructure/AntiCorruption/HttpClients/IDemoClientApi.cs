using System.Net.Http;
using System.Threading.Tasks;
using Refit;
using SJI3.Infrastructure.AntiCorruption.HttpClients.Requests;

namespace SJI3.Infrastructure.AntiCorruption.HttpClients;

public interface IDemoClientApi
{
    [Get("/api/demo")]
    Task<HttpResponseMessage> Get();

    [Post("/api/demo")]
    Task<HttpResponseMessage> Post([Body] DemoPostBody frBody);
}
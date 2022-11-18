using System.Net.Http;
using Polly;
using Polly.Contrib.WaitAndRetry;
using Polly.Extensions.Http;

namespace SJI3.Infrastructure.Extensions;

public static class PollyPolicies
{
    public static IAsyncPolicy<HttpResponseMessage> RetryWithJitter(PolicyOption option)
    {
        var delay = Backoff.DecorrelatedJitterBackoffV2(medianFirstRetryDelay: option.RetryDelay, retryCount: option.RetryCount);

        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
            .WaitAndRetryAsync(delay);
    }
}
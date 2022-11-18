using System;

namespace SJI3.Infrastructure.Extensions;

public class PolicyOption
{
    public TimeSpan RetryDelay { get; }
    public int RetryCount { get; }

    public PolicyOption(TimeSpan retryDelay, int retryCount)
    {
        RetryDelay = retryDelay;
        RetryCount = retryCount;
    }
}
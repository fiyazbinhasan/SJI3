using System;
using System.Threading;
using System.Threading.Tasks;

namespace SJI3.Core.Common.Infra;

public interface ITaskQueue<T>
{
    ValueTask QueueWorkItemAsync(Func<CancellationToken, ValueTask<T>> workItem);

    ValueTask<Func<CancellationToken, ValueTask<T>>> DequeueAsync(
        CancellationToken cancellationToken);
}
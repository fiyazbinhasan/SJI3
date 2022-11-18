using System;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using SJI3.Core.Common.Infra;

namespace SJI3.Infrastructure.Services.Queue;

public class BackgroundTaskQueue : ITaskQueue<Guid>
{
    private readonly Channel<Func<CancellationToken, ValueTask<Guid>>> _queue;

    public BackgroundTaskQueue(int capacity)
    {
        var options = new BoundedChannelOptions(capacity)
        {
            FullMode = BoundedChannelFullMode.Wait
        };

        _queue = Channel.CreateBounded<Func<CancellationToken, ValueTask<Guid>>>(options);
    }

    public ValueTask QueueWorkItemAsync(Func<CancellationToken, ValueTask<Guid>> workItem)
    {
        if (workItem == null)
        {
            throw new ArgumentNullException(nameof(workItem));
        }

        return WriteInternalAsync(workItem);
    }

    private async ValueTask WriteInternalAsync(Func<CancellationToken, ValueTask<Guid>> workItem)
    {
        await _queue.Writer.WriteAsync(workItem);
    }

    public async ValueTask<Func<CancellationToken, ValueTask<Guid>>> DequeueAsync(CancellationToken cancellationToken)
    {
        var workItem = await _queue.Reader.ReadAsync(cancellationToken);

        return workItem;
    }
}
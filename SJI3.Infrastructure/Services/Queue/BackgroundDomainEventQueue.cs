using SJI3.Core.Common.Domain;
using SJI3.Core.Common.Infra;
using System;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace SJI3.Infrastructure.Services.Queue;

public class BackgroundDomainEventQueue : ITaskQueue<IDomainEvent>
{
    private readonly Channel<Func<CancellationToken, ValueTask<IDomainEvent>>> _queue;

    public BackgroundDomainEventQueue(int capacity)
    {
        var options = new BoundedChannelOptions(capacity)
        {
            FullMode = BoundedChannelFullMode.Wait
        };

        _queue = Channel.CreateBounded<Func<CancellationToken, ValueTask<IDomainEvent>>>(options);
    }

    public async ValueTask<Func<CancellationToken, ValueTask<IDomainEvent>>> DequeueAsync(CancellationToken cancellationToken)
    {
        var workItem = await _queue.Reader.ReadAsync(cancellationToken);

        return workItem;
    }

    public ValueTask QueueWorkItemAsync(Func<CancellationToken, ValueTask<IDomainEvent>> workItem)
    {
        if (workItem == null)
        {
            throw new ArgumentNullException(nameof(workItem));
        }

        return WriteInternalAsync(workItem);
    }

    private async ValueTask WriteInternalAsync(Func<CancellationToken, ValueTask<IDomainEvent>> workItem)
    {
        await _queue.Writer.WriteAsync(workItem);
    }
}

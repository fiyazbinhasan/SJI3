using SJI3.Core.Common.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace SJI3.Core.Services.Domain;

public interface IDomainEventPublishingService
{
    Task Publish(IDomainEvent domainEvent, CancellationToken cancellationToken);
}

using SJI3.Core.Common.Domain;
using SJI3.Core.Common.Infra;
using SJI3.Core.Services.Domain;
using SJI3.Infrastructure.Consumers.InMemory.Infra;
using System.Threading;
using System.Threading.Tasks;

namespace SJI3.Infrastructure.AntiCorruption.Domain;

public class DomainEventPublishingService : IDomainEventPublishingService
{
    private readonly IAppLogger<DomainEventPublishingService> _logger;
    private readonly IMemoryBus _bus;

    public DomainEventPublishingService(IAppLogger<DomainEventPublishingService> logger, IMemoryBus bus)
    {
        _logger = logger;
        _bus = bus;
    }

    public async Task Publish(IDomainEvent domainEvent, CancellationToken stoppingToken)
    {
        _logger.LogInformation("Publishing domain event {DomainEvent}", domainEvent.GetType());
        await _bus.Publish((object) domainEvent, stoppingToken);
        await Task.CompletedTask;
    }
}
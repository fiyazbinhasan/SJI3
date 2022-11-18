using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SJI3.Core.Common.Domain;
using SJI3.Core.Common.Infra;
using SJI3.Core.Services.Domain;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SJI3.Infrastructure.AntiCorruption.HostedServices;

public class DomainEventsPublishingBackgroundService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ITaskQueue<IDomainEvent> _taskQueue;
    private readonly IAppLogger<DomainEventsPublishingBackgroundService> _logger;

    public DomainEventsPublishingBackgroundService(IServiceProvider serviceProvider, ITaskQueue<IDomainEvent> taskQueue,
        IAppLogger<DomainEventsPublishingBackgroundService> logger)
    {
        _serviceProvider = serviceProvider;
        _taskQueue = taskQueue;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Domain Event Publishing Service is running");

        await BackgroundProcessing(stoppingToken);
    }

    private async Task BackgroundProcessing(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            var workItem =
                await _taskQueue.DequeueAsync(cancellationToken);

            try
            {
                using var scope = _serviceProvider.CreateScope();
                var publishingService = scope.ServiceProvider.GetRequiredService<IDomainEventPublishingService>();
                await publishingService.Publish(await workItem(cancellationToken), cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Error occurred executing {WorkItem}", nameof(workItem));
            }
        }
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Domain Event Publishing Service is stopping");

        await base.StopAsync(cancellationToken);
    }
}

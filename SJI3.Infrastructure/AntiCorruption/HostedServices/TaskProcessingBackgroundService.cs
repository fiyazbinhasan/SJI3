using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SJI3.Core.Common.Infra;
using SJI3.Core.Services.Domain;

namespace SJI3.Infrastructure.AntiCorruption.HostedServices;

public class TaskProcessingBackgroundService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ITaskQueue<Guid> _taskQueue;
    private readonly IAppLogger<TaskProcessingBackgroundService> _logger;

    public TaskProcessingBackgroundService(IServiceProvider serviceProvider, ITaskQueue<Guid> taskQueue,
        IAppLogger<TaskProcessingBackgroundService> logger)
    {
        _serviceProvider = serviceProvider;
        _taskQueue = taskQueue;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Queued Hosted Service is running");

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
                var processingService = scope.ServiceProvider.GetRequiredService<ITaskProcessingService>();
                await processingService.ProcessTask(await workItem(cancellationToken), cancellationToken);
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
        _logger.LogInformation("Queued Hosted Service is stopping");

        await base.StopAsync(cancellationToken);
    }
}
using System;
using System.Threading;
using System.Threading.Tasks;
using SJI3.Core.Common.Infra;
using SJI3.Core.Services.Domain;

namespace SJI3.Infrastructure.AntiCorruption.Domain;

public class TaskProcessingService : ITaskProcessingService
{
    private readonly IAppLogger<TaskProcessingService> _logger;

    public TaskProcessingService(IAppLogger<TaskProcessingService> logger)
    {
        _logger = logger;
    }

    public Task ProcessTask(Guid taskId, CancellationToken stoppingToken)
    {
        _logger.LogInformation("New task added {TaskId}", taskId);
        return Task.CompletedTask;
    }
}
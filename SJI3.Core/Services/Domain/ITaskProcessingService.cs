using System;
using System.Threading;
using System.Threading.Tasks;

namespace SJI3.Core.Services.Domain;

public interface ITaskProcessingService
{
    Task ProcessTask(Guid taskId, CancellationToken stoppingToken);
}
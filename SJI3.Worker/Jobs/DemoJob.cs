using Quartz;

namespace SJI3.Worker.Jobs;

[DisallowConcurrentExecution]
public class DemoJob : IJob
{
    private readonly ILogger<DemoJob> _logger;
    public DemoJob(ILogger<DemoJob> logger)
    {
        _logger = logger;
    }

    public Task Execute(IJobExecutionContext context)
    {
        _logger.LogInformation("Demo Job Running!");
        return Task.CompletedTask;
    }
}
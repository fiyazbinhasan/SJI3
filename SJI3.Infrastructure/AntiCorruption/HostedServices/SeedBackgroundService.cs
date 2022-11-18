using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SJI3.Core.Common.Infra;
using SJI3.Core.Entities;
using SJI3.Infrastructure.Data;

namespace SJI3.Infrastructure.AntiCorruption.HostedServices;

public class SeedBackgroundService : BackgroundService
{
    private readonly IAppLogger<SeedBackgroundService> _logger;
    private readonly IServiceProvider _provider;

    public SeedBackgroundService(IAppLogger<SeedBackgroundService> logger, IServiceProvider provider)
    {
        _logger = logger;
        _provider = provider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Seed Hosted Service is running");

        await BackgroundProcessing(stoppingToken);
    }

    private async Task BackgroundProcessing(CancellationToken cancellationToken)
    {
        using var scope = _provider.CreateScope();
        var context = scope.ServiceProvider.GetService<AppDbContext>();

        if (context != null)
        {
            await context.Database.EnsureCreatedAsync(cancellationToken);

            if(!context.Set<ApplicationUser>().Any())
            {
                context.Set<ApplicationUser>().Add(new ApplicationUser(new Guid("a1c4b914-c059-46f5-84bf-0415c118c39e"), "admin@sji3.local", "admin@sji3.local"));
            }
            
            if (!context.Set<TaskUnitStatus>().Any())
            {
                context.Set<TaskUnitStatus>().Add(new TaskUnitStatus(1, nameof(TaskUnitStatus.TaskStatusOne)));
                context.Set<TaskUnitStatus>().Add(new TaskUnitStatus(2, nameof(TaskUnitStatus.TaskStatusTwo)));
                context.Set<TaskUnitStatus>().Add(new TaskUnitStatus(3, nameof(TaskUnitStatus.TaskStatusThree)));
            }

            if (!context.Set<TaskUnitType>().Any())
            {
                context.Set<TaskUnitType>().Add(new TaskUnitType(1, nameof(TaskUnitType.TypeOne)));
                context.Set<TaskUnitType>().Add(new TaskUnitType(2, nameof(TaskUnitType.TypeTwo)));
            }

            await context.SaveChangesAsync(cancellationToken);
        }
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Seed Hosted Service is stopping");

        await base.StopAsync(cancellationToken);
    }
}
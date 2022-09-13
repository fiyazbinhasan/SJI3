using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using SJI3.Core.Common.Infra;
using SJI3.Infrastructure.Consumers.Messages;
using SJI3.Infrastructure.Services.Notification;

namespace SJI3.Infrastructure.Consumers.Handlers;

public class TaskStatusChangedConsumer : IConsumer<TaskStatusChanged>
{
    private readonly IHubContext<NotificationsHub> _hubContext;
    private readonly IAppLogger<TaskStatusChangedConsumer> _logger;

    public TaskStatusChangedConsumer(
        IHubContext<NotificationsHub> hubContext,
        IAppLogger<TaskStatusChangedConsumer> logger)
    {
        _hubContext = hubContext ?? throw new ArgumentNullException(nameof(hubContext));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task Consume(ConsumeContext<TaskStatusChanged> context)
    {
        _logger.LogInformation("----- Handling integration event: {IntegrationEventId} - ({IntegrationEvent})", context.Message.Id, context.Message);

        await _hubContext.Clients
            .Group(context.Message.ApplicationUserId)
            .SendAsync("UpdatedTaskState", new { context.Message.TaskId, context.Message.TaskStatus });
    }
}
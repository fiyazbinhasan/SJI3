using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.SignalR;
using SJI3.Core.Common.Infra;
using SJI3.Infrastructure.Services.Notification;

namespace SJI3.Infrastructure.Consumers.InMemory.TaskUnit.TaskStatusChanged;

public class TaskStatusChangedConsumer : IConsumer<TaskStatusChangedMessage>
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

    public async Task Consume(ConsumeContext<TaskStatusChangedMessage> context)
    {
        _logger.LogInformation("----- Handling integration event: {IntegrationEventId} - ({IntegrationEvent})", context.Message.Id, context.Message);

        await _hubContext.Clients
            .Group(context.Message.ApplicationUserId.ToString())
            .SendAsync("UpdatedTaskState", new { context.Message.TaskId, context.Message.TaskStatus });
        
        await Task.CompletedTask;
    }
}
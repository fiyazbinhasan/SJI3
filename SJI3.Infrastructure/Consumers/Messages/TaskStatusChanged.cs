using System;

namespace SJI3.Infrastructure.Consumers.Messages;

public record TaskStatusChanged(Guid TaskId, int TaskStatus, string ApplicationUserId) : IntegrationEvent;
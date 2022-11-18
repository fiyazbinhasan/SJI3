using System;
using SJI3.Core.Common.Infra;

namespace SJI3.Infrastructure.Consumers.InMemory.TaskUnit.TaskStatusChanged;

public record TaskStatusChangedMessage(Guid TaskId, int TaskStatus, Guid ApplicationUserId) : IntegrationEvent;
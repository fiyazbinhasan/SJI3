using SJI3.Core.Common.Domain;
using System;
using SJI3.Core.Common.Infra;

namespace SJI3.Core.DomainEvents;

public record TaskUnitAddedDomainEvent(Guid TaskUnitId, Guid ApplicationUserId) : IntegrationEvent, IDomainEvent;


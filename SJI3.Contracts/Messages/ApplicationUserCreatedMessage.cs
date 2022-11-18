using SJI3.Contracts.Common;

namespace SJI3.Contracts.Messages
{
    public record ApplicationUserCreatedMessage(Guid UserId, string Name, string Email) : IntegrationEvent;
}
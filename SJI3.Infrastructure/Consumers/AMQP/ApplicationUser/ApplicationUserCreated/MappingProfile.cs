using Mapster;
using SJI3.Contracts.Messages;

namespace SJI3.Infrastructure.Consumers.AMQP.ApplicationUser.ApplicationUserCreated;

public class MappingProfile : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<ApplicationUserCreatedMessage, Core.Entities.ApplicationUser>()
            .IgnoreNonMapped(true)
            .ConstructUsing(src => new Core.Entities.ApplicationUser(src.UserId, src.Name, src.Email));
    }
}
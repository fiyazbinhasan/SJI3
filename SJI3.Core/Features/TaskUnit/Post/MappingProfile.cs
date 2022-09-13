using Mapster;
using SJI3.Core.Entities;

namespace SJI3.Core.Features.TaskUnit.Post;

public class MappingProfile : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<PostTaskUnitCommand, Entities.TaskUnit>()
            .IgnoreNonMapped(true)
            .ConstructUsing(src => new Entities.TaskUnit(
                    src.Id,
                    src.Moniker,
                    src.FromDateTime,
                    src.ToDateTime,
                    src.TaskUnitTypeId,
                    TaskUnitStatus.TaskStatusOne.Id,
                    src.ApplicationUserId
                )
            );
    }
}
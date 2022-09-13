using Mapster;
using SJI3.Core.Common.Domain;
using SJI3.Core.Entities;

namespace SJI3.Core.Features.TaskUnit.Put;

public record MappingProfile : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<PutTaskUnitCommand, Entities.TaskUnit>()
            .Map(src => src.TaskUnitStatusId, dest => dest.TaskUnitStatusId)
            .IgnoreNonMapped(true);
    }
}
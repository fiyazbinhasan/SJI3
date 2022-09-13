using Mapster;
using SJI3.Core.Common.Domain;
using SJI3.Core.Entities;

namespace SJI3.Core.Features.TaskUnit.Get;

public record MappingProfile : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Entities.TaskUnit, TaskUnitModel>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Moniker, src => src.Moniker)
            .Map(dest => dest.FromDateTime, src => src.FromDateTime)
            .Map(dest => dest.ToDateTime, src => src.ToDateTime)
            .Map(dest => dest.TaskUnitType, src => Enumeration.FromValue<TaskUnitType>(src.TaskUnitTypeId))
            .Map(dest => dest.TaskUnitStatus, src => Enumeration.FromValue<TaskUnitStatus>(src.TaskUnitStatusId));
    }
}
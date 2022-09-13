using Mapster;
using SJI3.Core.Common.Domain;

namespace SJI3.Core.Features.Common;

public record MappingProfile : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig(typeof(PagedList<>), typeof(PaginationMetadata));
    }
}
using Mapster;

namespace SJI3.Core.Features.Menu.Get;

public record MappingProfile : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Entities.Menu, MenuModel>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Title, src => src.Title)
            .Map(dest => dest.Url, src => src.Url)
            .Map(dest => dest.Icon, src => src.Icon)
            .Map(dest => dest.IsParent, src => src.IsParent)
            .Map(dest => dest.ParentId, src => src.ParentId.Value)
            .Map(dest => dest.IsActive, src => src.IsActive);
    }
}
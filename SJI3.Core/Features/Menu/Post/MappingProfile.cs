using Mapster;

namespace SJI3.Core.Features.Menu.Post;

public class MappingProfile : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<PostMenuCommand, Entities.Menu>()
            .IgnoreNonMapped(true)
            .ConstructUsing(src => new Entities.Menu(
                    src.Id,
                    src.Title,
                    src.Url,
                    src.Icon,
                    src.ParentId,
                    src.IsParent,
                    src.IsActive
                )
            );
    }
}
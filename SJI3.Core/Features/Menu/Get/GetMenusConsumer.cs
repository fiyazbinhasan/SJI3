using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MapsterMapper;
using MassTransit;
using SJI3.Core.Common.Infra;

namespace SJI3.Core.Features.Menu.Get;

public class GetMenusConsumer : IConsumer<IGetMenusQuery>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetMenusConsumer(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public async Task Consume(ConsumeContext<IGetMenusQuery> context)
    {
        var menus = await _unitOfWork.Repository<Entities.Menu, Guid>().ListAllAsync();
        
        await context.RespondAsync(new GetMenusResponse
        {
            Menus = _mapper.Map<IEnumerable<MenuModel>>(menus)
        });
    }
}
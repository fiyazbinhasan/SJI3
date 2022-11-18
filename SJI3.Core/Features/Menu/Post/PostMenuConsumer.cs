using System;
using System.Threading.Tasks;
using MapsterMapper;
using MassTransit;
using SJI3.Core.Common.Infra;

namespace SJI3.Core.Features.Menu.Post;

public class PostMenuConsumer : IConsumer<PostMenuCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public PostMenuConsumer(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public async Task Consume(ConsumeContext<PostMenuCommand> context)
    {
        var menu = _mapper.Map<PostMenuCommand, Entities.Menu>(context.Message);
        await _unitOfWork.Repository<Entities.Menu, Guid>().AddAsync(menu);

        if (await _unitOfWork.CommitAsync() > 0)
        {
            await context.RespondAsync<IPostMenuResponse>(new
            {
                IsAdded = true
            });
        }
        else
        {
            await context.RespondAsync<IPostMenuResponse>(new { IsAdded = false });
        }
    }
}
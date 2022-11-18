using System;
using System.Threading.Tasks;
using MassTransit;
using SJI3.Core.Common.Infra;
using SJI3.Core.Entities;

namespace SJI3.Core.Features.Menu.AddUserToMenu;

public class AddUserToMenuConsumer : IConsumer<AddUserToMenuCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public AddUserToMenuConsumer(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task Consume(ConsumeContext<AddUserToMenuCommand> context)
    {
        await _unitOfWork.Repository<UserMenu, Guid>()
            .AddAsync(new UserMenu(context.Message.ApplicationUserId, context.Message.MenuId, true));
        
        if (await _unitOfWork.CommitAsync() > 0)
        {
            await context.RespondAsync<IAddUserToMenuResponse>(new
            {
                IsAdded = true
            });
        }
        else
        {
            await context.RespondAsync<IAddUserToMenuResponse>(new { IsAdded = false });
        }
    }
}
using MassTransit;
using SJI3.Core.Common.Infra;
using SJI3.Core.DomainEvents;
using SJI3.Core.Entities;
using System;
using System.Threading.Tasks;

namespace SJI3.Infrastructure.Consumers.InMemory.TaskUnit.TaskUnitAdded;

public class TaskUnitAddedConsumer : IConsumer<TaskUnitAddedDomainEvent>
{
    private readonly IUnitOfWork _unitOfWork;

    public TaskUnitAddedConsumer(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Consume(ConsumeContext<TaskUnitAddedDomainEvent> context)
    {
        var applicationUser = await _unitOfWork.Repository<ApplicationUser, Guid>()
            .GetByIdAsync(context.Message.ApplicationUserId);

        applicationUser.AddTaskUnit(context.Message.TaskUnitId);

        await _unitOfWork.CommitAsync();
    }
}

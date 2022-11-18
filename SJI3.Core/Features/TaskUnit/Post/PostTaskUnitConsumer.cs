using MapsterMapper;
using MassTransit;
using SJI3.Core.Common.Domain;
using SJI3.Core.Common.Infra;
using SJI3.Core.DomainEvents;
using System;
using System.Threading.Tasks;

namespace SJI3.Core.Features.TaskUnit.Post;

public class PostTaskUnitConsumer : IConsumer<PostTaskUnitCommand>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITaskQueue<Guid> _taskQueue;
    private readonly ITaskQueue<IDomainEvent> _domainEventQueue;

    public PostTaskUnitConsumer(IUnitOfWork unitOfWork, IMapper mapper, ITaskQueue<Guid> taskQueue, ITaskQueue<IDomainEvent> domainEventQueue)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _taskQueue = taskQueue;
        _domainEventQueue = domainEventQueue;
    }

    public async Task Consume(ConsumeContext<PostTaskUnitCommand> context)
    {
        var taskUnit = _mapper.Map<PostTaskUnitCommand, Entities.TaskUnit>(context.Message);
        await _unitOfWork.Repository<Entities.TaskUnit, Guid>().AddAsync(taskUnit);

        if (await _unitOfWork.CommitAsync() > 0)
        {
            await context.RespondAsync<IPostTaskUnitResponse>(new
            {
                IsAdded = true
            });

            await _taskQueue.QueueWorkItemAsync(_ => new ValueTask<Guid>(taskUnit.Id));
            await _domainEventQueue.QueueWorkItemAsync(_ => new ValueTask<IDomainEvent>(new TaskUnitAddedDomainEvent(taskUnit.Id, taskUnit.ApplicationUserId)));
        }
        else
        {
            await context.RespondAsync<IPostTaskUnitResponse>(new { IsAdded = false });
        }
    }
}
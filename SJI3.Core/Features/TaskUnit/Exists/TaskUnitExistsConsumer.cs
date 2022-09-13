using MassTransit;
using System.Threading.Tasks;

namespace SJI3.Core.Features.TaskUnit.Exists;

public class TaskUnitExistsConsumer : IConsumer<ITaskUnitExistsQuery>
{
    private readonly IRepository _repository;

    public TaskUnitExistsConsumer(IRepository repository)
    {
        _repository = repository;
    }

    public async Task Consume(ConsumeContext<ITaskUnitExistsQuery> context)
    {
        await context.RespondAsync(new TaskUnitExistsResponse
        {
            Exists = await _repository.Exists(context.Message.Id)
        });
    }
}
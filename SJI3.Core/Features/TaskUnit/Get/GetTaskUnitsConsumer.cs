using MapsterMapper;
using MassTransit;
using SJI3.Core.Features.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SJI3.Core.Features.TaskUnit.Get;

public class GetTaskUnitsConsumer : IConsumer<IGetTaskUnitsQuery>
{
    private readonly IRepository _repository;
    private readonly IMapper _mapper;

    public GetTaskUnitsConsumer(IRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task Consume(ConsumeContext<IGetTaskUnitsQuery> context)
    {
        var pagedList = _repository.Get(context.Message.ResourceParameters);

        await context.RespondAsync(new GetTaskUnitsResponse
        {
            TaskUnits = _mapper.Map<IEnumerable<TaskUnitModel>>(pagedList),
            PaginationMetadata = _mapper.Map<PaginationMetadata>(pagedList)
        });
    }
}
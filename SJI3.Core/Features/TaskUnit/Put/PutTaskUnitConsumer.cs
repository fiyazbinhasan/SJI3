using MapsterMapper;
using MassTransit;
using SJI3.Core.Common.Infra;
using System;
using System.Threading.Tasks;

namespace SJI3.Core.Features.TaskUnit.Put
{
    public class PutTaskUnitConsumer : IConsumer<PutTaskUnitCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public PutTaskUnitConsumer(IUnitOfWork unitOfWork, IRepository repository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Consume(ConsumeContext<PutTaskUnitCommand> context)
        {
            var taskUnit = await _unitOfWork.Repository<Entities.TaskUnit, Guid>().GetByIdAsync(context.Message.Id);
            _mapper.Map(context.Message, taskUnit);

            await context.RespondAsync<IPutTaskUnitResponse>(new
            {
                IsUpdated = await _repository.UpdateTaskStatus(taskUnit)
            });
        }
    }
}
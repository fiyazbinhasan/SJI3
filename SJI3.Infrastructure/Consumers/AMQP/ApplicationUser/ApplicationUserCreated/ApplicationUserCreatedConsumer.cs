using System.Threading.Tasks;
using MapsterMapper;
using MassTransit;
using SJI3.Contracts.Messages;

namespace SJI3.Infrastructure.Consumers.AMQP.ApplicationUser.ApplicationUserCreated;

public class ApplicationUserCreatedConsumer : IConsumer<ApplicationUserCreatedMessage>
{
    private readonly IRepository _repository;
    private readonly IMapper _mapper;

    public ApplicationUserCreatedConsumer(IRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task Consume(ConsumeContext<ApplicationUserCreatedMessage> context)
    {
        var applicationUser = _mapper.Map<Core.Entities.ApplicationUser>(context.Message);
        await _repository.AddApplicationUser(applicationUser);
        await Task.CompletedTask;
    }
}
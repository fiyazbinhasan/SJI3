using MassTransit;

namespace SJI3.Infrastructure.Consumers.AMQP.ApplicationUser.ApplicationUserCreated;

public class ApplicationUserCreatedConsumerDefinition : ConsumerDefinition<ApplicationUserCreatedConsumer>
{
    protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<ApplicationUserCreatedConsumer> consumerConfigurator)
    {
        endpointConfigurator.UseMessageRetry(r => r.Intervals(500, 1000));
    }
}
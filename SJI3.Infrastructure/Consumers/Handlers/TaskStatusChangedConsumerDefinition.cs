using MassTransit;

namespace SJI3.Infrastructure.Consumers.Handlers
{
    public class TaskStatusChangedConsumerDefinition : ConsumerDefinition<TaskStatusChangedConsumer>
    {
        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<TaskStatusChangedConsumer> consumerConfigurator)
        {
            endpointConfigurator.UseMessageRetry(r => r.Intervals(500, 1000));
        }
    }
}
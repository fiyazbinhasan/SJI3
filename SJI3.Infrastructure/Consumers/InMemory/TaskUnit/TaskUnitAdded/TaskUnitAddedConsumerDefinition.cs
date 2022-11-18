using MassTransit;

namespace SJI3.Infrastructure.Consumers.InMemory.TaskUnit.TaskUnitAdded;

public class TaskUnitAddedConsumerDefinition : ConsumerDefinition<TaskUnitAddedConsumer>
{
    protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<TaskUnitAddedConsumer> consumerConfigurator)
    {
        endpointConfigurator.UseMessageRetry(r => r.Intervals(500, 1000));
    }
}
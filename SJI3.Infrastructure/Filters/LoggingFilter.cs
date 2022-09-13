using System;
using System.Threading.Tasks;
using MassTransit;
using SJI3.Core.Common.Infra;

namespace SJI3.Infrastructure.Filters;

public class LoggingFilter<T> :
    IFilter<ConsumeContext<T>>
    where T : class
{
    private readonly IAppLogger<LoggingFilter<T>> _logger;

    public LoggingFilter(IAppLogger<LoggingFilter<T>> logger)
    {
        _logger = logger;
    }

    public void Probe(ProbeContext context)
    {
        throw new NotSupportedException();
    }

    public Task Send(ConsumeContext<T> context, IPipe<ConsumeContext<T>> next)
    {
        _logger.LogInformation($"Handling {typeof(T).Name}");
        var response = next.Send(context);
        _logger.LogInformation($"Handled {typeof(T).Name}");
        return response;
    }
}
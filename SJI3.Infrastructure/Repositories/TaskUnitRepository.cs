using Microsoft.EntityFrameworkCore;
using NodaTime;
using SJI3.Core.Entities;
using SJI3.Infrastructure.Data;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using SJI3.Core.Common.Domain;
using SJI3.Core.Common.Infra;
using SJI3.Infrastructure.Consumers.InMemory.TaskUnit.TaskStatusChanged;
using Exists = SJI3.Core.Features.TaskUnit.Exists;
using Get = SJI3.Core.Features.TaskUnit.Get;
using Put = SJI3.Core.Features.TaskUnit.Put;
using SJI3.Infrastructure.Consumers.InMemory.Infra;

namespace SJI3.Infrastructure.Repositories;

public class TaskUnitRepository : GenericRepository<TaskUnit, Guid>, Get.IRepository, Exists.IRepository, Put.IRepository
{
    private readonly AppDbContext _context;
    private readonly ITaskQueue<Guid> _taskQueue;
    private readonly IMemoryBus _bus;

    public TaskUnitRepository(AppDbContext context, ITaskQueue<Guid> taskQueue, IMemoryBus bus) : base(context)
    {
        _context = context;
        _taskQueue = taskQueue;
        _bus = bus;
    }

    public Task<bool> Exists(Guid id)
    {
        return _context.Set<TaskUnit>().AnyAsync(f => f.Id.Equals(id));
    }

    public PagedList<TaskUnit> Get(Get.ResourceParameters parameters)
    {
        IQueryable<TaskUnit> query = _context.Set<TaskUnit>();

        if (parameters.Start.HasValue && parameters.End.HasValue)
        {
            var interval = new DateInterval(parameters.Start.Value, parameters.End.Value);

            query = query.Where(t =>
                t.ToDateTime.HasValue && t.FromDateTime.HasValue &&
                (interval.Contains(t.FromDateTime.Value.InUtc().Date) ||
                 interval.Contains(t.ToDateTime.Value.InUtc().Date)));
        }

        if (string.IsNullOrEmpty(parameters.OrderBy))
            return PagedList<TaskUnit>.Create(query, parameters.PageNumber,
                parameters.PageSize);

        return PagedList<TaskUnit>.Create(query.OrderBy(parameters.OrderBy), parameters.PageNumber,
            parameters.PageSize);
    }

    public async Task<bool> UpdateTaskStatus(TaskUnit taskUnit)
    {
        _context.Set<TaskUnit>().Update(taskUnit);

        if (await _context.SaveChangesAsync() > 0)
        {
            await _taskQueue.QueueWorkItemAsync(_ => new ValueTask<Guid>(taskUnit.Id));

            await _bus.Publish(new TaskStatusChangedMessage(taskUnit.Id, taskUnit.TaskUnitStatusId, taskUnit.ApplicationUserId));

            return await Task.FromResult(true);
        }

        return await Task.FromResult(false);
    }
}
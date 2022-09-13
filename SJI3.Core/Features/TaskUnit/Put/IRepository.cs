using SJI3.Core.Common.Infra;
using System;
using System.Threading.Tasks;

namespace SJI3.Core.Features.TaskUnit.Put;

public interface IRepository : IGenericRepository<Entities.TaskUnit, Guid>
{
    Task<bool> UpdateTaskStatus(Entities.TaskUnit taskUnit);
}
using SJI3.Core.Common.Infra;
using System;
using System.Threading.Tasks;

namespace SJI3.Core.Features.TaskUnit.Exists;

public interface IRepository : IGenericRepository<Entities.TaskUnit, Guid>
{
    Task<bool> Exists(Guid id);
}
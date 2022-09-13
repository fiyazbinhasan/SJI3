using SJI3.Core.Common.Domain;
using SJI3.Core.Common.Infra;
using System;

namespace SJI3.Core.Features.TaskUnit.Get;

public interface IRepository : IGenericRepository<Entities.TaskUnit, Guid>
{
    PagedList<Entities.TaskUnit> Get(ResourceParameters parameters);
}
using System;
using System.Threading.Tasks;
using SJI3.Core.Common.Infra;

namespace SJI3.Infrastructure.Consumers.AMQP.ApplicationUser.ApplicationUserCreated;

public interface IRepository : IGenericRepository<Core.Entities.ApplicationUser, Guid>
{
    Task AddApplicationUser(Core.Entities.ApplicationUser applicationUser);
}
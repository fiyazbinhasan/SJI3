using System;
using System.Threading.Tasks;
using SJI3.Core.Entities;
using SJI3.Infrastructure.Data;
using AddApplicationUser = SJI3.Infrastructure.Consumers.AMQP.ApplicationUser.ApplicationUserCreated;

namespace SJI3.Infrastructure.Repositories;

public class ApplicationUserRepository : GenericRepository<ApplicationUser, Guid>, AddApplicationUser.IRepository
{
    private readonly AppDbContext _context;

    public ApplicationUserRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task AddApplicationUser(ApplicationUser applicationUser)
    {
        await _context.ApplicationUsers.AddAsync(applicationUser);
        await _context.SaveChangesAsync();
    }
}
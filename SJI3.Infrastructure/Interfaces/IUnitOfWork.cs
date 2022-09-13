using Microsoft.EntityFrameworkCore;
using SJI3.Core.Common.Infra;

namespace SJI3.Infrastructure.Interfaces;

public interface IUnitOfWork<out TContext> : IUnitOfWork where TContext : DbContext
{
    TContext Context { get; }
}
using CSharpFunctionalExtensions;
using System.Threading;
using System.Threading.Tasks;

namespace SJI3.Core.Common.Infra;

public interface IUnitOfWork
{
    IGenericRepository<TEntity, TKey> Repository<TEntity, TKey>() where TEntity : Entity<TKey>;

    Task<int> CommitAsync(CancellationToken cancellationToken = default);
}
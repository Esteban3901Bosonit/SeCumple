using SeCumple.Infrastructure.Persistence.Interfaces;

namespace SeCumple.Infrastructure.Persistence.Context;

public interface IUnitOfWork : IDisposable
{
    IBaseRepository<TEntity> Repository<TEntity>() where TEntity : class;
    Task<int> Complete();
}
namespace SeCumple.Infrastructure.Persistence.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IBaseRepository<TEntity> Repository<TEntity>() where TEntity : class;
    Task<int> Complete();
}
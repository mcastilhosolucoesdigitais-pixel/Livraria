using Livraria.TJRJ.API.Domain.Common;

namespace Livraria.TJRJ.API.Domain.Interfaces;

public interface IRepository<T> where T : Entity
{
    IUnitOfWork UnitOfWork { get; }

    Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);
    Task AddAsync(T entity, CancellationToken cancellationToken = default);
    void Update(T entity);
    void Delete(T entity);
    Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default);
}

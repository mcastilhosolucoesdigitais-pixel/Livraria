using Livraria.TJRJ.API.Domain.Entities;

namespace Livraria.TJRJ.API.Domain.Interfaces;

public interface IAutorRepository : IRepository<Autor>
{
    Task<Autor?> GetByIdWithLivrosAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Autor>> GetByNomeAsync(string nome, CancellationToken cancellationToken = default);
    Task<bool> ExistsByNomeAsync(string nome, CancellationToken cancellationToken = default);
    Task<bool> ExistsByNomeExcludingIdAsync(string nome, Guid id, CancellationToken cancellationToken = default);
}

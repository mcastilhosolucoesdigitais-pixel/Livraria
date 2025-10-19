using Livraria.TJRJ.API.Domain.Entities;

namespace Livraria.TJRJ.API.Domain.Interfaces;

public interface ILivroRepository : IRepository<Livro>
{
    Task<Livro?> GetByIdWithDetailsAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Livro>> GetAllWithDetailsAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<Livro>> GetByTituloAsync(string titulo, CancellationToken cancellationToken = default);
    Task<IEnumerable<Livro>> GetByAutorIdAsync(Guid autorId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Livro>> GetByAssuntoIdAsync(Guid assuntoId, CancellationToken cancellationToken = default);
    Task<bool> ExistsByTituloAsync(string titulo, CancellationToken cancellationToken = default);
}

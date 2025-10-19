using Livraria.TJRJ.API.Domain.Entities;

namespace Livraria.TJRJ.API.Domain.Interfaces;

public interface ILivroRepository : IRepository<Livro>
{
    Task<Livro?> GetByIdWithDetailsAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Livro>> GetAllWithDetailsAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<Livro>> GetByTituloAsync(string titulo, CancellationToken cancellationToken = default);
    Task<IEnumerable<Livro>> GetByAutorIdAsync(int autorId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Livro>> GetByAssuntoIdAsync(int assuntoId, CancellationToken cancellationToken = default);
    Task<bool> ExistsByTituloAsync(string titulo, CancellationToken cancellationToken = default);
}

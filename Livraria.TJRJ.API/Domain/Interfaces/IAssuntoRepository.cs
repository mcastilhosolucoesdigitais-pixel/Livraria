using Livraria.TJRJ.API.Domain.Entities;

namespace Livraria.TJRJ.API.Domain.Interfaces;

public interface IAssuntoRepository : IRepository<Assunto>
{
    Task<Assunto?> GetByIdWithLivrosAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Assunto>> GetByDescricaoAsync(string descricao, CancellationToken cancellationToken = default);
    Task<bool> ExistsByDescricaoAsync(string descricao, CancellationToken cancellationToken = default);
    Task<bool> ExistsByDescricaoExcludingIdAsync(string descricao, Guid id, CancellationToken cancellationToken = default);
}

using Livraria.TJRJ.API.Domain.DTOs;

namespace Livraria.TJRJ.API.Domain.Interfaces;

public interface IRelatorioRepository
{
    Task<IEnumerable<RelatorioLivroAutorDto>> GetRelatorioLivrosPorAutorAsync(
        Guid? autorId = null,
        CancellationToken cancellationToken = default);
}

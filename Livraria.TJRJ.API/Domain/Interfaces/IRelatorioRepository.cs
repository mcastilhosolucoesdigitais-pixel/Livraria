using Livraria.TJRJ.API.Application.DTOs;

namespace Livraria.TJRJ.API.Domain.Interfaces;

public interface IRelatorioRepository
{
    Task<IEnumerable<RelatorioLivroAutorDto>> GetRelatorioLivrosPorAutorAsync(
        int? autorId = null,
        CancellationToken cancellationToken = default);
}

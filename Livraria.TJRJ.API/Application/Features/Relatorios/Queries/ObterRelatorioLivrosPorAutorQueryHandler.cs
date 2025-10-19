using Livraria.TJRJ.API.Application.Common;
using Livraria.TJRJ.API.Application.DTOs;
using Livraria.TJRJ.API.Domain.Interfaces;
using MediatR;

namespace Livraria.TJRJ.API.Application.Features.Relatorios.Queries;

public class ObterRelatorioLivrosPorAutorQueryHandler : IRequestHandler<ObterRelatorioLivrosPorAutorQuery, Result<IEnumerable<RelatorioLivroAutorDto>>>
{
    private readonly IRelatorioRepository _relatorioRepository;

    public ObterRelatorioLivrosPorAutorQueryHandler(IRelatorioRepository relatorioRepository)
    {
        _relatorioRepository = relatorioRepository;
    }

    public async Task<Result<IEnumerable<RelatorioLivroAutorDto>>> Handle(
        ObterRelatorioLivrosPorAutorQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            var relatorio = await _relatorioRepository.GetRelatorioLivrosPorAutorAsync(
                request.AutorId,
                cancellationToken);

            return Result<IEnumerable<RelatorioLivroAutorDto>>.Success(relatorio);
        }
        catch (Exception ex)
        {
            return Result<IEnumerable<RelatorioLivroAutorDto>>.Failure($"Erro ao gerar relatório: {ex.Message}");
        }
    }
}

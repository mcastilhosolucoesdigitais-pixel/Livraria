using Livraria.TJRJ.API.Application.Common;
using Livraria.TJRJ.API.Application.DTOs;
using Livraria.TJRJ.API.Domain.Interfaces;
using MediatR;

namespace Livraria.TJRJ.API.Application.Features.Assuntos.Queries;

public class ObterTodosAssuntosQueryHandler : IRequestHandler<ObterTodosAssuntosQuery, Result<IEnumerable<AssuntoDto>>>
{
    private readonly IAssuntoRepository _assuntoRepository;

    public ObterTodosAssuntosQueryHandler(IAssuntoRepository assuntoRepository)
    {
        _assuntoRepository = assuntoRepository;
    }

    public async Task<Result<IEnumerable<AssuntoDto>>> Handle(ObterTodosAssuntosQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var assuntos = await _assuntoRepository.GetAllAsync(cancellationToken);

            var assuntosDto = assuntos.Select(a => new AssuntoDto
            {
                Id = a.Id,
                Descricao = a.Descricao
            }).ToList();

            return Result<IEnumerable<AssuntoDto>>.Success(assuntosDto);
        }
        catch (Exception ex)
        {
            return Result<IEnumerable<AssuntoDto>>.Failure($"Erro ao buscar assuntos: {ex.Message}");
        }
    }
}

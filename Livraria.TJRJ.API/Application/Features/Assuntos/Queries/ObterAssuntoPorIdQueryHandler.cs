using Livraria.TJRJ.API.Application.Common;
using Livraria.TJRJ.API.Application.DTOs;
using Livraria.TJRJ.API.Domain.Interfaces;
using MediatR;

namespace Livraria.TJRJ.API.Application.Features.Assuntos.Queries;

public class ObterAssuntoPorIdQueryHandler : IRequestHandler<ObterAssuntoPorIdQuery, Result<AssuntoDto>>
{
    private readonly IAssuntoRepository _assuntoRepository;

    public ObterAssuntoPorIdQueryHandler(IAssuntoRepository assuntoRepository)
    {
        _assuntoRepository = assuntoRepository;
    }

    public async Task<Result<AssuntoDto>> Handle(ObterAssuntoPorIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var assunto = await _assuntoRepository.GetByIdAsync(request.Id, cancellationToken);

            if (assunto == null)
            {
                return Result<AssuntoDto>.Failure("Assunto não encontrado.");
            }

            var assuntoDto = new AssuntoDto
            {
                Id = assunto.Id,
                Descricao = assunto.Descricao
            };

            return Result<AssuntoDto>.Success(assuntoDto);
        }
        catch (Exception ex)
        {
            return Result<AssuntoDto>.Failure($"Erro ao buscar assunto: {ex.Message}");
        }
    }
}

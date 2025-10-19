using Livraria.TJRJ.API.Application.Common;
using Livraria.TJRJ.API.Application.DTOs;
using Livraria.TJRJ.API.Domain.Interfaces;
using MediatR;

namespace Livraria.TJRJ.API.Application.Features.Autores.Queries;

public class ObterTodosAutoresQueryHandler : IRequestHandler<ObterTodosAutoresQuery, Result<IEnumerable<AutorDto>>>
{
    private readonly IAutorRepository _autorRepository;

    public ObterTodosAutoresQueryHandler(IAutorRepository autorRepository)
    {
        _autorRepository = autorRepository;
    }

    public async Task<Result<IEnumerable<AutorDto>>> Handle(ObterTodosAutoresQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var autores = await _autorRepository.GetAllAsync(cancellationToken);

            var autoresDto = autores.Select(a => new AutorDto
            {
                Id = a.Id,
                Nome = a.Nome
            }).ToList();

            return Result<IEnumerable<AutorDto>>.Success(autoresDto);
        }
        catch (Exception ex)
        {
            return Result<IEnumerable<AutorDto>>.Failure($"Erro ao buscar autores: {ex.Message}");
        }
    }
}

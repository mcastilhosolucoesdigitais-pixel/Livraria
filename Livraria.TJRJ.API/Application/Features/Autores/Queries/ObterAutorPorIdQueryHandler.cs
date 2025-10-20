using Livraria.TJRJ.API.Application.Common;
using Livraria.TJRJ.API.Application.DTOs;
using Livraria.TJRJ.API.Domain.Exceptions;
using Livraria.TJRJ.API.Domain.Interfaces;
using MediatR;

namespace Livraria.TJRJ.API.Application.Features.Autores.Queries;

public class ObterAutorPorIdQueryHandler : IRequestHandler<ObterAutorPorIdQuery, Result<AutorDto>>
{
    private readonly IAutorRepository _autorRepository;

    public ObterAutorPorIdQueryHandler(IAutorRepository autorRepository)
    {
        _autorRepository = autorRepository;
    }

    public async Task<Result<AutorDto>> Handle(ObterAutorPorIdQuery request, CancellationToken cancellationToken)
    {
        var autor = await _autorRepository.GetByIdAsync(request.Id, cancellationToken);

        if (autor == null)
        {
            throw new AutorNaoEncontradoException(request.Id);
        }

        var autorDto = new AutorDto
        {
            Id = autor.Id,
            Nome = autor.Nome
        };

        return Result<AutorDto>.Success(autorDto);
    }
}

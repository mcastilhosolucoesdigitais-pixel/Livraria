using Livraria.TJRJ.API.Application.Common;
using Livraria.TJRJ.API.Application.DTOs;
using MediatR;

namespace Livraria.TJRJ.API.Application.Features.Autores.Queries;

public class ObterAutorPorIdQuery : IRequest<Result<AutorDto>>
{
    public Guid Id { get; set; }
}

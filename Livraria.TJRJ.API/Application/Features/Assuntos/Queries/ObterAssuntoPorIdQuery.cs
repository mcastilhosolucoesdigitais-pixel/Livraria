using Livraria.TJRJ.API.Application.Common;
using Livraria.TJRJ.API.Application.DTOs;
using MediatR;

namespace Livraria.TJRJ.API.Application.Features.Assuntos.Queries;

public class ObterAssuntoPorIdQuery : IRequest<Result<AssuntoDto>>
{
    public Guid Id { get; set; }
}

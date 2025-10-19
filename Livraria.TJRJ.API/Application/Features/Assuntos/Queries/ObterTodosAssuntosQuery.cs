using Livraria.TJRJ.API.Application.Common;
using Livraria.TJRJ.API.Application.DTOs;
using MediatR;

namespace Livraria.TJRJ.API.Application.Features.Assuntos.Queries;

public class ObterTodosAssuntosQuery : IRequest<Result<IEnumerable<AssuntoDto>>>
{
}

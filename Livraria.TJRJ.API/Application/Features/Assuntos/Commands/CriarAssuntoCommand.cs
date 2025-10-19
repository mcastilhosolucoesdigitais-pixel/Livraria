using Livraria.TJRJ.API.Application.Common;
using MediatR;

namespace Livraria.TJRJ.API.Application.Features.Assuntos.Commands;

public class CriarAssuntoCommand : IRequest<Result<Guid>>
{
    public string Descricao { get; set; } = string.Empty;
}

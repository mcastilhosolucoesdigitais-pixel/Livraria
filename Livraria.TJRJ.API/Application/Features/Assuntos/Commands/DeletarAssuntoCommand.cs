using Livraria.TJRJ.API.Application.Common;
using MediatR;

namespace Livraria.TJRJ.API.Application.Features.Assuntos.Commands;

public class DeletarAssuntoCommand : IRequest<Result>
{
    public Guid Id { get; set; }
}

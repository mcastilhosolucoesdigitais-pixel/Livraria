using Livraria.TJRJ.API.Application.Common;
using MediatR;

namespace Livraria.TJRJ.API.Application.Features.Autores.Commands;

public class DeletarAutorCommand : IRequest<Result>
{
    public Guid Id { get; set; }
}

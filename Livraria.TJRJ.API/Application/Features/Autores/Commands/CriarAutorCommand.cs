using Livraria.TJRJ.API.Application.Common;
using MediatR;

namespace Livraria.TJRJ.API.Application.Features.Autores.Commands;

public class CriarAutorCommand : IRequest<Result<Guid>>
{
    public string Nome { get; set; } = string.Empty;
}

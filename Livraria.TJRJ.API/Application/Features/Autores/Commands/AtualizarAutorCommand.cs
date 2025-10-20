using Livraria.TJRJ.API.Application.Common;
using MediatR;

namespace Livraria.TJRJ.API.Application.Features.Autores.Commands;

public class AtualizarAutorCommand : IRequest<Result>
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
}

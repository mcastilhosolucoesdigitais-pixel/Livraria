using Livraria.TJRJ.API.Application.Common;
using MediatR;

namespace Livraria.TJRJ.API.Application.Features.Assuntos.Commands;

public class AtualizarAssuntoCommand : IRequest<Result>
{
    public int Id { get; set; }
    public string Descricao { get; set; } = string.Empty;
}

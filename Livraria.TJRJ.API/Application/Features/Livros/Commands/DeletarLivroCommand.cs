using Livraria.TJRJ.API.Application.Common;
using MediatR;

namespace Livraria.TJRJ.API.Application.Features.Livros.Commands;

public class DeletarLivroCommand : IRequest<Result>
{
    public Guid Id { get; set; }
}

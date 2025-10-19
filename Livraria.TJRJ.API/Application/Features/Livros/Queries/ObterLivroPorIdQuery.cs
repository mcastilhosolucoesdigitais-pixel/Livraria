using Livraria.TJRJ.API.Application.Common;
using Livraria.TJRJ.API.Application.DTOs;
using MediatR;

namespace Livraria.TJRJ.API.Application.Features.Livros.Queries;

public class ObterLivroPorIdQuery : IRequest<Result<LivroDto>>
{
    public Guid Id { get; set; }
}

using Livraria.TJRJ.API.Application.Common;
using Livraria.TJRJ.API.Application.DTOs;
using MediatR;

namespace Livraria.TJRJ.API.Application.Features.Relatorios.Queries;

public class ObterRelatorioLivrosPorAutorQuery : IRequest<Result<IEnumerable<RelatorioLivroAutorDto>>>
{
    public int? AutorId { get; set; }
}

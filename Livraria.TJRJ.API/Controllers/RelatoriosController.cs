using Livraria.TJRJ.API.Application.DTOs;
using Livraria.TJRJ.API.Application.Features.Relatorios.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Livraria.TJRJ.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RelatoriosController : ControllerBase
{
    private readonly IMediator _mediator;

    public RelatoriosController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Obtém relatório de livros agrupados por autor
    /// </summary>
    /// <param name="autorId">ID do autor para filtrar (opcional)</param>
    [HttpGet("livros-por-autor")]
    [ProducesResponseType(typeof(List<RelatorioLivroAutorAgrupadoDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetLivrosPorAutor([FromQuery] int? autorId = null)
    {
        var query = new ObterRelatorioLivrosPorAutorQuery { AutorId = autorId };
        var result = await _mediator.Send(query);

        if (!result.IsSuccess)
        {
            return BadRequest(new { errors = result.Errors.Any() ? result.Errors : new List<string> { result.Error ?? "Erro ao obter relatório" } });
        }

        var grouped = result.Value!
            .GroupBy(r => new { r.AutorId, r.AutorNome })
            .Select(g => new RelatorioLivroAutorAgrupadoDTO
            {
                AutorId = g.Key.AutorId,
                AutorNome = g.Key.AutorNome,
                Livros = g.Select(l => l).ToList()
            })
            .OrderBy(x=>x.AutorNome);

        return Ok(grouped);
    }
}

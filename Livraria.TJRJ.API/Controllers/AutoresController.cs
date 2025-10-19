using Livraria.TJRJ.API.Application.DTOs;
using Livraria.TJRJ.API.Application.Features.Autores.Commands;
using Livraria.TJRJ.API.Application.Features.Autores.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Livraria.TJRJ.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AutoresController : ControllerBase
{
    private readonly IMediator _mediator;

    public AutoresController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Cria um novo autor
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Post([FromBody] CriarAutorCommand command)
    {
        var result = await _mediator.Send(command);

        if (!result.IsSuccess)
        {
            return BadRequest(new { errors = result.Errors.Any() ? result.Errors : new List<string> { result.Error ?? "Erro ao criar autor" } });
        }

        return Created($"/api/autores/{result.Value}", result.Value);
    }

    /// <summary>
    /// Obtém todos os autores
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<AutorDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get()
    {
        var query = new ObterTodosAutoresQuery();
        var result = await _mediator.Send(query);

        if (!result.IsSuccess)
        {
            return BadRequest(new { errors = result.Errors.Any() ? result.Errors : new List<string> { result.Error ?? "Erro ao obter autores" } });
        }

        return Ok(result.Value);
    }

    /// <summary>
    /// Obtém um autor por ID
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(AutorDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        var query = new ObterAutorPorIdQuery { Id = id };
        var result = await _mediator.Send(query);

        if (!result.IsSuccess)
        {
            return NotFound(new { error = result.Error ?? "Autor não encontrado" });
        }

        return Ok(result.Value);
    }

    /// <summary>
    /// Atualiza um autor existente
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Put(int id, [FromBody] AtualizarAutorCommand command)
    {
        command.Id = id;
        var result = await _mediator.Send(command);

        if (!result.IsSuccess)
        {
            if (result.Error?.Contains("não encontrado") == true || result.Error?.Contains("não existe") == true)
            {
                return NotFound(new { error = result.Error });
            }

            return BadRequest(new { errors = result.Errors.Any() ? result.Errors : new List<string> { result.Error ?? "Erro ao atualizar autor" } });
        }

        return NoContent();
    }

    /// <summary>
    /// Remove um autor
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var command = new DeletarAutorCommand { Id = id };
        var result = await _mediator.Send(command);

        if (!result.IsSuccess)
        {
            return NotFound(new { error = result.Error ?? "Autor não encontrado" });
        }

        return NoContent();
    }
}

using Livraria.TJRJ.API.Application.DTOs;
using Livraria.TJRJ.API.Application.Features.Assuntos.Commands;
using Livraria.TJRJ.API.Application.Features.Assuntos.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Livraria.TJRJ.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AssuntosController : ControllerBase
{
    private readonly IMediator _mediator;

    public AssuntosController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Cria um novo assunto
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Post([FromBody] CriarAssuntoCommand command)
    {
        var result = await _mediator.Send(command);
        return Created($"/api/assuntos/{result.Value}", result.Value);
    }

    /// <summary>
    /// Obtém todos os assuntos
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<AssuntoDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get()
    {
        var query = new ObterTodosAssuntosQuery();
        var result = await _mediator.Send(query);
        return Ok(result.Value);
    }

    /// <summary>
    /// Obtém um assunto por ID
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(AssuntoDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        var query = new ObterAssuntoPorIdQuery { Id = id };
        var result = await _mediator.Send(query);
        return Ok(result.Value);
    }

    /// <summary>
    /// Atualiza um assunto existente
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Put(int id, [FromBody] AtualizarAssuntoCommand command)
    {
        command.Id = id;
        await _mediator.Send(command);
        return NoContent();
    }

    /// <summary>
    /// Remove um assunto
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var command = new DeletarAssuntoCommand { Id = id };
        await _mediator.Send(command);
        return NoContent();
    }
}

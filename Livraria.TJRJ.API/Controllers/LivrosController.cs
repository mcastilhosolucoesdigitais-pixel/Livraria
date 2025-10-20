using Livraria.TJRJ.API.Application.DTOs;
using Livraria.TJRJ.API.Application.Features.Livros.Commands;
using Livraria.TJRJ.API.Application.Features.Livros.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Livraria.TJRJ.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LivrosController : ControllerBase
{
    private readonly IMediator _mediator;

    public LivrosController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Cria um novo livro
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Post([FromBody] CriarLivroCommand command)
    {
        var result = await _mediator.Send(command);
        return Created($"/api/livros/{result.Value}", result.Value);
    }

    /// <summary>
    /// Obtém todos os livros
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<LivroDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get()
    {
        var query = new ObterTodosLivrosQuery();
        var result = await _mediator.Send(query);
        return Ok(result.Value);
    }

    /// <summary>
    /// Obtém um livro por ID
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(LivroDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        var query = new ObterLivroPorIdQuery { Id = id };
        var result = await _mediator.Send(query);
        return Ok(result.Value);
    }

    /// <summary>
    /// Atualiza um livro existente
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Put(int id, [FromBody] AtualizarLivroCommand command)
    {
        command.Id = id;
        await _mediator.Send(command);
        return NoContent();
    }

    /// <summary>
    /// Remove um livro
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var command = new DeletarLivroCommand { Id = id };
        await _mediator.Send(command);
        return NoContent();
    }
}

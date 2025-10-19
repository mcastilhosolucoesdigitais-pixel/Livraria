using Livraria.TJRJ.API.Application.Common;
using Livraria.TJRJ.API.Application.DTOs;
using Livraria.TJRJ.API.Domain.Interfaces;
using MediatR;

namespace Livraria.TJRJ.API.Application.Features.Livros.Queries;

public class ObterTodosLivrosQueryHandler : IRequestHandler<ObterTodosLivrosQuery, Result<IEnumerable<LivroDto>>>
{
    private readonly ILivroRepository _livroRepository;

    public ObterTodosLivrosQueryHandler(ILivroRepository livroRepository)
    {
        _livroRepository = livroRepository;
    }

    public async Task<Result<IEnumerable<LivroDto>>> Handle(ObterTodosLivrosQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var livros = await _livroRepository.GetAllWithDetailsAsync(cancellationToken);

            var livrosDto = livros.Select(livro => new LivroDto
            {
                Id = livro.Id,
                Titulo = livro.Titulo,
                Editora = livro.Editora,
                Edicao = livro.Edicao,
                AnoPublicacao = livro.AnoPublicacao,
                Autores = livro.Autores.Select(a => new AutorDto
                {
                    Id = a.Id,
                    Nome = a.Nome
                }).ToList(),
                Assuntos = livro.Assuntos.Select(a => new AssuntoDto
                {
                    Id = a.Id,
                    Descricao = a.Descricao
                }).ToList(),
                Precos = livro.Precos.Select(p => new PrecoLivroDto
                {
                    Valor = p.Valor,
                    FormaDeCompra = p.FormaDeCompra.ToString()
                }).ToList()
            }).ToList();

            return Result<IEnumerable<LivroDto>>.Success(livrosDto);
        }
        catch (Exception ex)
        {
            return Result<IEnumerable<LivroDto>>.Failure($"Erro ao buscar livros: {ex.Message}");
        }
    }
}

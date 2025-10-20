using Livraria.TJRJ.API.Application.Common;
using Livraria.TJRJ.API.Application.DTOs;
using Livraria.TJRJ.API.Domain.Exceptions;
using Livraria.TJRJ.API.Domain.Interfaces;
using MediatR;

namespace Livraria.TJRJ.API.Application.Features.Livros.Queries;

public class ObterLivroPorIdQueryHandler : IRequestHandler<ObterLivroPorIdQuery, Result<LivroDto>>
{
    private readonly ILivroRepository _livroRepository;

    public ObterLivroPorIdQueryHandler(ILivroRepository livroRepository)
    {
        _livroRepository = livroRepository;
    }

    public async Task<Result<LivroDto>> Handle(ObterLivroPorIdQuery request, CancellationToken cancellationToken)
    {
        var livro = await _livroRepository.GetByIdWithDetailsAsync(request.Id, cancellationToken);

        if (livro == null)
        {
            throw new LivroNaoEncontradoException(int.Parse(request.Id.ToString()));
        }

        var livroDto = new LivroDto
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
        };

        return Result<LivroDto>.Success(livroDto);
    }
}

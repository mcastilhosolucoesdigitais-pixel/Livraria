using Livraria.TJRJ.API.Application.Common;
using Livraria.TJRJ.API.Domain.Entities;
using Livraria.TJRJ.API.Domain.Enums;
using Livraria.TJRJ.API.Domain.Exceptions;
using Livraria.TJRJ.API.Domain.Interfaces;
using MediatR;

namespace Livraria.TJRJ.API.Application.Features.Livros.Commands;

public class CriarLivroCommandHandler : IRequestHandler<CriarLivroCommand, Result<int>>
{
    private readonly ILivroRepository _livroRepository;
    private readonly IAutorRepository _autorRepository;
    private readonly IAssuntoRepository _assuntoRepository;

    public CriarLivroCommandHandler(
        ILivroRepository livroRepository,
        IAutorRepository autorRepository,
        IAssuntoRepository assuntoRepository)
    {
        _livroRepository = livroRepository;
        _autorRepository = autorRepository;
        _assuntoRepository = assuntoRepository;
    }

    public async Task<Result<int>> Handle(CriarLivroCommand request, CancellationToken cancellationToken)
    {
        // Cria o livro
        var livro = new Livro(
            request.Titulo,
            request.Editora,
            request.Edicao,
            request.AnoPublicacao);

        // Adiciona autores
        if (request.Autores.Any())
        {
            foreach (var autorId in request.Autores)
            {
                var autor = await _autorRepository.GetByIdAsync(autorId, cancellationToken);
                if (autor == null)
                {
                    throw new AutorNaoEncontradoException(autorId);
                }
                livro.AdicionarAutor(autor);
            }
        }

        // Adiciona assuntos
        if (request.Assuntos.Any())
        {
            foreach (var assuntoId in request.Assuntos)
            {
                var assunto = await _assuntoRepository.GetByIdAsync(assuntoId, cancellationToken);
                if (assunto == null)
                {
                    throw new AssuntoNaoEncontradoException(assuntoId);
                }
                livro.AdicionarAssunto(assunto);
            }
        }

        // Adiciona preços
        foreach (var precoInput in request.Precos)
        {
            if (Enum.TryParse<FormaDeCompra>(precoInput.FormaDeCompra, true, out var formaDeCompra))
            {
                livro.DefinirPreco(precoInput.Valor, formaDeCompra);
            }
            else
            {
                throw new ArgumentException($"Forma de compra '{precoInput.FormaDeCompra}' é inválida.");
            }
        }

        // Adiciona ao repositório
        await _livroRepository.AddAsync(livro, cancellationToken);

        // Salva as alterações
        await _livroRepository.UnitOfWork.CommitAsync(cancellationToken);

        return Result<int>.Success(livro.Id);
    }
}

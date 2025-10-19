using Livraria.TJRJ.API.Application.Common;
using Livraria.TJRJ.API.Domain.Entities;
using Livraria.TJRJ.API.Domain.Enums;
using Livraria.TJRJ.API.Domain.Interfaces;
using MediatR;

namespace Livraria.TJRJ.API.Application.Features.Livros.Commands;

public class CriarLivroCommandHandler : IRequestHandler<CriarLivroCommand, Result<Guid>>
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

    public async Task<Result<Guid>> Handle(CriarLivroCommand request, CancellationToken cancellationToken)
    {
        try
        {
            // Cria o livro
            var livro = new Livro(
                request.Titulo,
                request.Editora,
                request.Edicao,
                request.AnoPublicacao);

            // Adiciona autores
            if (request.AutoresIds.Any())
            {
                foreach (var autorId in request.AutoresIds)
                {
                    var autor = await _autorRepository.GetByIdAsync(autorId, cancellationToken);
                    if (autor == null)
                    {
                        return Result<Guid>.Failure($"Autor com ID {autorId} não encontrado.");
                    }
                    livro.AdicionarAutor(autor);
                }
            }

            // Adiciona assuntos
            if (request.AssuntosIds.Any())
            {
                foreach (var assuntoId in request.AssuntosIds)
                {
                    var assunto = await _assuntoRepository.GetByIdAsync(assuntoId, cancellationToken);
                    if (assunto == null)
                    {
                        return Result<Guid>.Failure($"Assunto com ID {assuntoId} não encontrado.");
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
                    return Result<Guid>.Failure($"Forma de compra '{precoInput.FormaDeCompra}' é inválida.");
                }
            }

            // Adiciona ao repositório
            await _livroRepository.AddAsync(livro, cancellationToken);

            // Salva as alterações
            await _livroRepository.UnitOfWork.CommitAsync(cancellationToken);

            return Result<Guid>.Success(livro.Id);
        }
        catch (ArgumentException ex)
        {
            return Result<Guid>.Failure(ex.Message);
        }
        catch (Exception ex)
        {
            return Result<Guid>.Failure($"Erro ao criar livro: {ex.Message}");
        }
    }
}

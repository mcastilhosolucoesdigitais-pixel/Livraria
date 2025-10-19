using Livraria.TJRJ.API.Application.Common;
using Livraria.TJRJ.API.Domain.Enums;
using Livraria.TJRJ.API.Domain.Interfaces;
using MediatR;

namespace Livraria.TJRJ.API.Application.Features.Livros.Commands;

public class AtualizarLivroCommandHandler : IRequestHandler<AtualizarLivroCommand, Result>
{
    private readonly ILivroRepository _livroRepository;
    private readonly IAutorRepository _autorRepository;
    private readonly IAssuntoRepository _assuntoRepository;

    public AtualizarLivroCommandHandler(
        ILivroRepository livroRepository,
        IAutorRepository autorRepository,
        IAssuntoRepository assuntoRepository)
    {
        _livroRepository = livroRepository;
        _autorRepository = autorRepository;
        _assuntoRepository = assuntoRepository;
    }

    public async Task<Result> Handle(AtualizarLivroCommand request, CancellationToken cancellationToken)
    {
        try
        {
            // Busca o livro com detalhes
            var livro = await _livroRepository.GetByIdWithDetailsAsync(request.Id, cancellationToken);

            if (livro == null)
            {
                return Result.Failure("Livro não encontrado.");
            }

            // Atualiza os dados básicos
            livro.AtualizarDados(
                request.Titulo,
                request.Editora,
                request.Edicao,
                request.AnoPublicacao);

            // Atualiza autores
            livro.LimparAutores();
            foreach (var autorId in request.AutoresIds)
            {
                var autor = await _autorRepository.GetByIdAsync(autorId, cancellationToken);
                if (autor == null)
                {
                    return Result.Failure($"Autor com ID {autorId} não encontrado.");
                }
                livro.AdicionarAutor(autor);
            }

            // Atualiza assuntos
            livro.LimparAssuntos();
            foreach (var assuntoId in request.AssuntosIds)
            {
                var assunto = await _assuntoRepository.GetByIdAsync(assuntoId, cancellationToken);
                if (assunto == null)
                {
                    return Result.Failure($"Assunto com ID {assuntoId} não encontrado.");
                }
                livro.AdicionarAssunto(assunto);
            }

            // Atualiza preços
            // Remove todos os preços existentes
            foreach (FormaDeCompra forma in Enum.GetValues(typeof(FormaDeCompra)))
            {
                livro.RemoverPreco(forma);
            }

            // Adiciona novos preços
            foreach (var precoInput in request.Precos)
            {
                if (Enum.TryParse<FormaDeCompra>(precoInput.FormaDeCompra, true, out var formaDeCompra))
                {
                    livro.DefinirPreco(precoInput.Valor, formaDeCompra);
                }
                else
                {
                    return Result.Failure($"Forma de compra '{precoInput.FormaDeCompra}' é inválida.");
                }
            }

            // Atualiza o repositório
            _livroRepository.Update(livro);

            // Salva as alterações
            await _livroRepository.UnitOfWork.CommitAsync(cancellationToken);

            return Result.Success();
        }
        catch (ArgumentException ex)
        {
            return Result.Failure(ex.Message);
        }
        catch (Exception ex)
        {
            return Result.Failure($"Erro ao atualizar livro: {ex.Message}");
        }
    }
}

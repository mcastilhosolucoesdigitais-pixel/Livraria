using Livraria.TJRJ.API.Application.Common;
using Livraria.TJRJ.API.Domain.Interfaces;
using MediatR;

namespace Livraria.TJRJ.API.Application.Features.Livros.Commands;

public class DeletarLivroCommandHandler : IRequestHandler<DeletarLivroCommand, Result>
{
    private readonly ILivroRepository _livroRepository;

    public DeletarLivroCommandHandler(ILivroRepository livroRepository)
    {
        _livroRepository = livroRepository;
    }

    public async Task<Result> Handle(DeletarLivroCommand request, CancellationToken cancellationToken)
    {
        try
        {
            // Busca o livro com detalhes para limpar relacionamentos
            var livro = await _livroRepository.GetByIdWithDetailsAsync(request.Id, cancellationToken);

            if (livro == null)
            {
                return Result.Failure("Livro não encontrado.");
            }

            // Limpa relacionamentos antes de deletar
            livro.LimparAutores();
            livro.LimparAssuntos();

            // Deleta o livro
            _livroRepository.Delete(livro);

            // Salva as alterações
            await _livroRepository.UnitOfWork.CommitAsync(cancellationToken);

            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure($"Erro ao deletar livro: {ex.Message}");
        }
    }
}

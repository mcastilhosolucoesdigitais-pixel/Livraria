using Livraria.TJRJ.API.Application.Common;
using Livraria.TJRJ.API.Domain.Exceptions;
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
        // Busca o livro com detalhes para limpar relacionamentos
        var livro = await _livroRepository.GetByIdWithDetailsAsync(request.Id, cancellationToken);

        if (livro == null)
        {
            throw new LivroNaoEncontradoException(int.Parse(request.Id.ToString()));
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
}

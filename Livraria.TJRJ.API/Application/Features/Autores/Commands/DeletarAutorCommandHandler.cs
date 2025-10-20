using Livraria.TJRJ.API.Application.Common;
using Livraria.TJRJ.API.Domain.Exceptions;
using Livraria.TJRJ.API.Domain.Interfaces;
using MediatR;

namespace Livraria.TJRJ.API.Application.Features.Autores.Commands;

public class DeletarAutorCommandHandler : IRequestHandler<DeletarAutorCommand, Result>
{
    private readonly IAutorRepository _autorRepository;

    public DeletarAutorCommandHandler(IAutorRepository autorRepository)
    {
        _autorRepository = autorRepository;
    }

    public async Task<Result> Handle(DeletarAutorCommand request, CancellationToken cancellationToken)
    {
        // Busca o autor com seus livros
        var autor = await _autorRepository.GetByIdWithLivrosAsync(request.Id, cancellationToken);

        if (autor == null)
        {
            throw new AutorNaoEncontradoException(request.Id);
        }

        // Verifica se o autor possui livros associados
        if (autor.Livros.Any())
        {
            throw new ValidationException("Não é possível deletar um autor que possui livros associados.");
        }

        // Deleta o autor
        _autorRepository.Delete(autor);

        // Salva as alterações
        await _autorRepository.UnitOfWork.CommitAsync(cancellationToken);

        return Result.Success();
    }
}

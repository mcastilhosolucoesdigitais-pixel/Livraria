using Livraria.TJRJ.API.Application.Common;
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
        try
        {
            // Busca o autor com seus livros
            var autor = await _autorRepository.GetByIdWithLivrosAsync(request.Id, cancellationToken);

            if (autor == null)
            {
                return Result.Failure("Autor não encontrado.");
            }

            // Verifica se o autor possui livros associados
            if (autor.Livros.Any())
            {
                return Result.Failure("Não é possível deletar um autor que possui livros associados.");
            }

            // Deleta o autor
            _autorRepository.Delete(autor);

            // Salva as alterações
            await _autorRepository.UnitOfWork.CommitAsync(cancellationToken);

            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure($"Erro ao deletar autor: {ex.Message}");
        }
    }
}

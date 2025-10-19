using Livraria.TJRJ.API.Application.Common;
using Livraria.TJRJ.API.Domain.Interfaces;
using MediatR;

namespace Livraria.TJRJ.API.Application.Features.Autores.Commands;

public class AtualizarAutorCommandHandler : IRequestHandler<AtualizarAutorCommand, Result>
{
    private readonly IAutorRepository _autorRepository;

    public AtualizarAutorCommandHandler(IAutorRepository autorRepository)
    {
        _autorRepository = autorRepository;
    }

    public async Task<Result> Handle(AtualizarAutorCommand request, CancellationToken cancellationToken)
    {
        try
        {
            // Busca o autor existente
            var autor = await _autorRepository.GetByIdAsync(request.Id, cancellationToken);

            if (autor == null)
            {
                return Result.Failure("Autor não encontrado.");
            }

            // Verifica se já existe outro autor com o mesmo nome
            var autorExistente = await _autorRepository.ExistsByNomeExcludingIdAsync(
                request.Nome,
                request.Id,
                cancellationToken);

            if (autorExistente)
            {
                return Result.Failure("Já existe outro autor com este nome.");
            }

            // Atualiza o nome
            autor.AtualizarNome(request.Nome);

            // Atualiza o repositório
            _autorRepository.Update(autor);

            // Salva as alterações
            await _autorRepository.UnitOfWork.CommitAsync(cancellationToken);

            return Result.Success();
        }
        catch (ArgumentException ex)
        {
            return Result.Failure(ex.Message);
        }
        catch (Exception ex)
        {
            return Result.Failure($"Erro ao atualizar autor: {ex.Message}");
        }
    }
}

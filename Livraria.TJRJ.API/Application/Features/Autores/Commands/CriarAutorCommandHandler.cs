using Livraria.TJRJ.API.Application.Common;
using Livraria.TJRJ.API.Domain.Entities;
using Livraria.TJRJ.API.Domain.Interfaces;
using MediatR;

namespace Livraria.TJRJ.API.Application.Features.Autores.Commands;

public class CriarAutorCommandHandler : IRequestHandler<CriarAutorCommand, Result<int>>
{
    private readonly IAutorRepository _autorRepository;

    public CriarAutorCommandHandler(IAutorRepository autorRepository)
    {
        _autorRepository = autorRepository;
    }

    public async Task<Result<int>> Handle(CriarAutorCommand request, CancellationToken cancellationToken)
    {
        try
        {
            // Verifica se já existe um autor com o mesmo nome
            var autorExistente = await _autorRepository.ExistsByNomeAsync(
                request.Nome,
                cancellationToken);

            if (autorExistente)
            {
                return Result<int>.Failure("Já existe um autor com este nome.");
            }

            // Cria o novo autor
            var autor = new Autor(request.Nome);

            // Adiciona ao repositório
            await _autorRepository.AddAsync(autor, cancellationToken);

            // Salva as alterações
            await _autorRepository.UnitOfWork.CommitAsync(cancellationToken);

            return Result<int>.Success(autor.Id);
        }
        catch (ArgumentException ex)
        {
            return Result<int>.Failure(ex.Message);
        }
        catch (Exception ex)
        {
            return Result<int>.Failure($"Erro ao criar autor: {ex.Message}");
        }
    }
}

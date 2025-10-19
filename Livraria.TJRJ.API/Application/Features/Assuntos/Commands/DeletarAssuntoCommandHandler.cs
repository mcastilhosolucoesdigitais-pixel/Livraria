using Livraria.TJRJ.API.Application.Common;
using Livraria.TJRJ.API.Domain.Interfaces;
using MediatR;

namespace Livraria.TJRJ.API.Application.Features.Assuntos.Commands;

public class DeletarAssuntoCommandHandler : IRequestHandler<DeletarAssuntoCommand, Result>
{
    private readonly IAssuntoRepository _assuntoRepository;

    public DeletarAssuntoCommandHandler(IAssuntoRepository assuntoRepository)
    {
        _assuntoRepository = assuntoRepository;
    }

    public async Task<Result> Handle(DeletarAssuntoCommand request, CancellationToken cancellationToken)
    {
        try
        {
            // Busca o assunto com seus livros
            var assunto = await _assuntoRepository.GetByIdWithLivrosAsync(request.Id, cancellationToken);

            if (assunto == null)
            {
                return Result.Failure("Assunto não encontrado.");
            }

            // Verifica se o assunto possui livros associados
            if (assunto.Livros.Any())
            {
                return Result.Failure("Não é possível deletar um assunto que possui livros associados.");
            }

            // Deleta o assunto
            _assuntoRepository.Delete(assunto);

            // Salva as alterações
            await _assuntoRepository.UnitOfWork.CommitAsync(cancellationToken);

            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure($"Erro ao deletar assunto: {ex.Message}");
        }
    }
}

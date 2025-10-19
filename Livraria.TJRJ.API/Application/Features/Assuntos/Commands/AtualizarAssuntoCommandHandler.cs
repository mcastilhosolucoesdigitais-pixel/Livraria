using Livraria.TJRJ.API.Application.Common;
using Livraria.TJRJ.API.Domain.Interfaces;
using MediatR;

namespace Livraria.TJRJ.API.Application.Features.Assuntos.Commands;

public class AtualizarAssuntoCommandHandler : IRequestHandler<AtualizarAssuntoCommand, Result>
{
    private readonly IAssuntoRepository _assuntoRepository;

    public AtualizarAssuntoCommandHandler(IAssuntoRepository assuntoRepository)
    {
        _assuntoRepository = assuntoRepository;
    }

    public async Task<Result> Handle(AtualizarAssuntoCommand request, CancellationToken cancellationToken)
    {
        try
        {
            // Busca o assunto existente
            var assunto = await _assuntoRepository.GetByIdAsync(request.Id, cancellationToken);

            if (assunto == null)
            {
                return Result.Failure("Assunto não encontrado.");
            }

            // Verifica se já existe outro assunto com a mesma descrição
            var assuntoExistente = await _assuntoRepository.ExistsByDescricaoExcludingIdAsync(
                request.Descricao,
                request.Id,
                cancellationToken);

            if (assuntoExistente)
            {
                return Result.Failure("Já existe outro assunto com esta descrição.");
            }

            // Atualiza a descrição
            assunto.AtualizarDescricao(request.Descricao);

            // Atualiza o repositório
            _assuntoRepository.Update(assunto);

            // Salva as alterações
            await _assuntoRepository.UnitOfWork.CommitAsync(cancellationToken);

            return Result.Success();
        }
        catch (ArgumentException ex)
        {
            return Result.Failure(ex.Message);
        }
        catch (Exception ex)
        {
            return Result.Failure($"Erro ao atualizar assunto: {ex.Message}");
        }
    }
}

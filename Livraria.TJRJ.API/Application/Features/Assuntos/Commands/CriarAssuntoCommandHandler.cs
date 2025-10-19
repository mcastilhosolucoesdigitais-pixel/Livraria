using Livraria.TJRJ.API.Application.Common;
using Livraria.TJRJ.API.Domain.Entities;
using Livraria.TJRJ.API.Domain.Interfaces;
using MediatR;

namespace Livraria.TJRJ.API.Application.Features.Assuntos.Commands;

public class CriarAssuntoCommandHandler : IRequestHandler<CriarAssuntoCommand, Result<Guid>>
{
    private readonly IAssuntoRepository _assuntoRepository;

    public CriarAssuntoCommandHandler(IAssuntoRepository assuntoRepository)
    {
        _assuntoRepository = assuntoRepository;
    }

    public async Task<Result<Guid>> Handle(CriarAssuntoCommand request, CancellationToken cancellationToken)
    {
        try
        {
            // Verifica se já existe um assunto com a mesma descrição
            var assuntoExistente = await _assuntoRepository.ExistsByDescricaoAsync(
                request.Descricao,
                cancellationToken);

            if (assuntoExistente)
            {
                return Result<Guid>.Failure("Já existe um assunto com esta descrição.");
            }

            // Cria o novo assunto
            var assunto = new Assunto(request.Descricao);

            // Adiciona ao repositório
            await _assuntoRepository.AddAsync(assunto, cancellationToken);

            // Salva as alterações
            await _assuntoRepository.UnitOfWork.CommitAsync(cancellationToken);

            return Result<Guid>.Success(assunto.Id);
        }
        catch (ArgumentException ex)
        {
            return Result<Guid>.Failure(ex.Message);
        }
        catch (Exception ex)
        {
            return Result<Guid>.Failure($"Erro ao criar assunto: {ex.Message}");
        }
    }
}

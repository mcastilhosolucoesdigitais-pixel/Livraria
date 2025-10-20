using Livraria.TJRJ.API.Application.Common;
using Livraria.TJRJ.API.Domain.Entities;
using Livraria.TJRJ.API.Domain.Exceptions;
using Livraria.TJRJ.API.Domain.Interfaces;
using MediatR;

namespace Livraria.TJRJ.API.Application.Features.Assuntos.Commands;

public class CriarAssuntoCommandHandler : IRequestHandler<CriarAssuntoCommand, Result<int>>
{
    private readonly IAssuntoRepository _assuntoRepository;

    public CriarAssuntoCommandHandler(IAssuntoRepository assuntoRepository)
    {
        _assuntoRepository = assuntoRepository;
    }

    public async Task<Result<int>> Handle(CriarAssuntoCommand request, CancellationToken cancellationToken)
    {
        // Verifica se já existe um assunto com a mesma descrição
        var assuntoExistente = await _assuntoRepository.ExistsByDescricaoAsync(
            request.Descricao,
            cancellationToken);

        if (assuntoExistente)
        {
            throw new ValidationException("Já existe um assunto com esta descrição.");
        }

        // Cria o novo assunto
        var assunto = new Assunto(request.Descricao);

        // Adiciona ao repositório
        await _assuntoRepository.AddAsync(assunto, cancellationToken);

        // Salva as alterações
        await _assuntoRepository.UnitOfWork.CommitAsync(cancellationToken);

        return Result<int>.Success(assunto.Id);
    }
}

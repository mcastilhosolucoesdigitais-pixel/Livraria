using FluentValidation;
using Livraria.TJRJ.API.Application.Features.Assuntos.Commands;

namespace Livraria.TJRJ.API.Application.Features.Assuntos.Validators;

public class DeletarAssuntoCommandValidator : AbstractValidator<DeletarAssuntoCommand>
{
    public DeletarAssuntoCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id do assunto é obrigatório.");
    }
}

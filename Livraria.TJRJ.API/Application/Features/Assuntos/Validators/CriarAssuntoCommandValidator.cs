using FluentValidation;
using Livraria.TJRJ.API.Application.Features.Assuntos.Commands;

namespace Livraria.TJRJ.API.Application.Features.Assuntos.Validators;

public class CriarAssuntoCommandValidator : AbstractValidator<CriarAssuntoCommand>
{
    public CriarAssuntoCommandValidator()
    {
        RuleFor(x => x.Descricao)
            .NotEmpty().WithMessage("Descrição do assunto é obrigatória.")
            .MaximumLength(20).WithMessage("Descrição do assunto não pode exceder 20 caracteres.");
    }
}

using FluentValidation;
using Livraria.TJRJ.API.Application.Features.Autores.Commands;

namespace Livraria.TJRJ.API.Application.Features.Autores.Validators;

public class AtualizarAutorCommandValidator : AbstractValidator<AtualizarAutorCommand>
{
    public AtualizarAutorCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id do autor é obrigatório.");

        RuleFor(x => x.Nome)
            .NotEmpty().WithMessage("Nome do autor é obrigatório.")
            .MaximumLength(40).WithMessage("Nome do autor não pode exceder 40 caracteres.");
    }
}

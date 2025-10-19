using FluentValidation;
using Livraria.TJRJ.API.Application.Features.Autores.Commands;

namespace Livraria.TJRJ.API.Application.Features.Autores.Validators;

public class DeletarAutorCommandValidator : AbstractValidator<DeletarAutorCommand>
{
    public DeletarAutorCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id do autor é obrigatório.");
    }
}

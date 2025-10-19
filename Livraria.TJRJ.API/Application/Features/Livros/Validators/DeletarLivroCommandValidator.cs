using FluentValidation;
using Livraria.TJRJ.API.Application.Features.Livros.Commands;

namespace Livraria.TJRJ.API.Application.Features.Livros.Validators;

public class DeletarLivroCommandValidator : AbstractValidator<DeletarLivroCommand>
{
    public DeletarLivroCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id do livro é obrigatório.");
    }
}

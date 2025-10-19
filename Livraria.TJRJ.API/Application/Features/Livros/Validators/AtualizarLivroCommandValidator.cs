using FluentValidation;
using Livraria.TJRJ.API.Application.Features.Livros.Commands;

namespace Livraria.TJRJ.API.Application.Features.Livros.Validators;

public class AtualizarLivroCommandValidator : AbstractValidator<AtualizarLivroCommand>
{
    public AtualizarLivroCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id do livro é obrigatório.");

        RuleFor(x => x.Titulo)
            .NotEmpty().WithMessage("Título do livro é obrigatório.")
            .MaximumLength(40).WithMessage("Título do livro não pode exceder 40 caracteres.");

        RuleFor(x => x.Editora)
            .NotEmpty().WithMessage("Editora é obrigatória.")
            .MaximumLength(40).WithMessage("Editora não pode exceder 40 caracteres.");

        RuleFor(x => x.Edicao)
            .GreaterThan(0).WithMessage("Edição deve ser um número positivo.");

        RuleFor(x => x.AnoPublicacao)
            .NotEmpty().WithMessage("Ano de publicação é obrigatório.")
            .Length(4).WithMessage("Ano de publicação deve ter 4 caracteres.")
            .Must(BeValidYear).WithMessage("Ano de publicação deve ser um ano válido.");

        RuleFor(x => x.AutoresIds)
            .NotEmpty().WithMessage("O livro deve ter pelo menos um autor.");

        RuleFor(x => x.AssuntosIds)
            .NotEmpty().WithMessage("O livro deve ter pelo menos um assunto.");

        RuleForEach(x => x.Precos).ChildRules(preco =>
        {
            preco.RuleFor(p => p.Valor)
                .GreaterThan(0).WithMessage("Valor do preço deve ser positivo.");

            preco.RuleFor(p => p.FormaDeCompra)
                .NotEmpty().WithMessage("Forma de compra é obrigatória.")
                .Must(BeValidFormaDeCompra).WithMessage("Forma de compra inválida. Valores aceitos: Balcao, SelfService, Internet, Evento.");
        });
    }

    private bool BeValidYear(string ano)
    {
        if (!int.TryParse(ano, out int anoInt))
            return false;

        return anoInt >= 1000 && anoInt <= DateTime.Now.Year;
    }

    private bool BeValidFormaDeCompra(string formaDeCompra)
    {
        var formasValidas = new[] { "Balcao", "SelfService", "Internet", "Evento" };
        return formasValidas.Contains(formaDeCompra, StringComparer.OrdinalIgnoreCase);
    }
}

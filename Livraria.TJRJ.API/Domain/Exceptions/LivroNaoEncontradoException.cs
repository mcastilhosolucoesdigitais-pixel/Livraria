namespace Livraria.TJRJ.API.Domain.Exceptions;

/// <summary>
/// Exceção lançada quando um livro não é encontrado
/// </summary>
public class LivroNaoEncontradoException : DomainException
{
    public LivroNaoEncontradoException(int id)
        : base($"Livro com ID '{id}' não foi encontrado.")
    {
    }

    public LivroNaoEncontradoException(string isbn)
        : base($"Livro com ISBN '{isbn}' não foi encontrado.")
    {
    }
}

namespace Livraria.TJRJ.API.Domain.Exceptions;

/// <summary>
/// Exceção lançada quando um autor não é encontrado
/// </summary>
public class AutorNaoEncontradoException : DomainException
{
    public AutorNaoEncontradoException(int id)
        : base($"Autor com ID '{id}' não foi encontrado.")
    {
    }
}

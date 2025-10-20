namespace Livraria.TJRJ.API.Domain.Exceptions;

/// <summary>
/// Exceção lançada quando um assunto não é encontrado
/// </summary>
public class AssuntoNaoEncontradoException : DomainException
{
    public AssuntoNaoEncontradoException(int id)
        : base($"Assunto com ID '{id}' não foi encontrado.")
    {
    }
}

using Livraria.TJRJ.API.Domain.Common;
using Livraria.TJRJ.API.Domain.Enums;

namespace Livraria.TJRJ.API.Domain.ValueObjects;

public class PrecoLivro : ValueObject
{
    public decimal Valor { get; private set; }
    public FormaDeCompra FormaDeCompra { get; private set; }

    private PrecoLivro() { }

    public PrecoLivro(decimal valor, FormaDeCompra formaDeCompra)
    {
        if (valor <= 0)
            throw new ArgumentException("Valor do livro deve ser positivo.", nameof(valor));

        Valor = valor;
        FormaDeCompra = formaDeCompra;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Valor;
        yield return FormaDeCompra;
    }

    public override string ToString()
    {
        return $"R$ {Valor:N2} - {FormaDeCompra}";
    }
}

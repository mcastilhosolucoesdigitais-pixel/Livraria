using Livraria.TJRJ.API.Domain.Common;

namespace Livraria.TJRJ.API.Domain.Entities;

public class Assunto : Entity
{
    public string Descricao { get; private set; }

    private readonly List<Livro> _livros;
    public IReadOnlyCollection<Livro> Livros => _livros.AsReadOnly();

    private Assunto()
    {
        _livros = new List<Livro>();
        Descricao = string.Empty;
    }

    public Assunto(string descricao)
    {
        if (string.IsNullOrWhiteSpace(descricao))
            throw new ArgumentException("Descrição do assunto não pode ser vazia.", nameof(descricao));

        if (descricao.Length > 20)
            throw new ArgumentException("Descrição do assunto não pode exceder 20 caracteres.", nameof(descricao));

        Descricao = descricao.Trim();
        _livros = new List<Livro>();
    }

    public void AtualizarDescricao(string descricao)
    {
        if (string.IsNullOrWhiteSpace(descricao))
            throw new ArgumentException("Descrição do assunto não pode ser vazia.", nameof(descricao));

        if (descricao.Length > 20)
            throw new ArgumentException("Descrição do assunto não pode exceder 20 caracteres.", nameof(descricao));

        Descricao = descricao.Trim();
    }

    internal void AdicionarLivro(Livro livro)
    {
        if (livro == null)
            throw new ArgumentNullException(nameof(livro));

        if (!_livros.Contains(livro))
            _livros.Add(livro);
    }

    internal void RemoverLivro(Livro livro)
    {
        if (livro != null)
            _livros.Remove(livro);
    }
}

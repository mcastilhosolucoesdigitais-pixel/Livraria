using Livraria.TJRJ.API.Domain.Common;

namespace Livraria.TJRJ.API.Domain.Entities;

public class Autor : Entity
{
    public string Nome { get; private set; }

    private readonly List<Livro> _livros;
    public IReadOnlyCollection<Livro> Livros => _livros.AsReadOnly();

    private Autor()
    {
        _livros = new List<Livro>();
        Nome = string.Empty;
    }

    public Autor(string nome)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("Nome do autor não pode ser vazio.", nameof(nome));

        if (nome.Length > 40)
            throw new ArgumentException("Nome do autor não pode exceder 40 caracteres.", nameof(nome));

        Nome = nome.Trim();
        _livros = new List<Livro>();
    }

    public void AtualizarNome(string nome)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("Nome do autor não pode ser vazio.", nameof(nome));

        if (nome.Length > 40)
            throw new ArgumentException("Nome do autor não pode exceder 40 caracteres.", nameof(nome));

        Nome = nome.Trim();
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

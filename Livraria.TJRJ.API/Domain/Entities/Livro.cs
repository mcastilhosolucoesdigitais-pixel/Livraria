using Livraria.TJRJ.API.Domain.Common;
using Livraria.TJRJ.API.Domain.Enums;
using Livraria.TJRJ.API.Domain.ValueObjects;

namespace Livraria.TJRJ.API.Domain.Entities;

public class Livro : Entity
{
    public string Titulo { get; private set; }
    public string Editora { get; private set; }
    public int Edicao { get; private set; }
    public string AnoPublicacao { get; private set; }

    private readonly List<Autor> _autores;
    public IReadOnlyCollection<Autor> Autores => _autores.AsReadOnly();

    private readonly List<Assunto> _assuntos;
    public IReadOnlyCollection<Assunto> Assuntos => _assuntos.AsReadOnly();

    private readonly List<PrecoLivro> _precos;
    public IReadOnlyCollection<PrecoLivro> Precos => _precos.AsReadOnly();

    private Livro()
    {
        Titulo = string.Empty;
        Editora = string.Empty;
        AnoPublicacao = string.Empty;
        _autores = new List<Autor>();
        _assuntos = new List<Assunto>();
        _precos = new List<PrecoLivro>();
    }

    public Livro(string titulo, string editora, int edicao, string anoPublicacao)
    {
        ValidarTitulo(titulo);
        ValidarEditora(editora);
        ValidarEdicao(edicao);
        ValidarAnoPublicacao(anoPublicacao);

        Titulo = titulo.Trim();
        Editora = editora.Trim();
        Edicao = edicao;
        AnoPublicacao = anoPublicacao.Trim();
        _autores = new List<Autor>();
        _assuntos = new List<Assunto>();
        _precos = new List<PrecoLivro>();
    }

    public void AtualizarDados(string titulo, string editora, int edicao, string anoPublicacao)
    {
        ValidarTitulo(titulo);
        ValidarEditora(editora);
        ValidarEdicao(edicao);
        ValidarAnoPublicacao(anoPublicacao);

        Titulo = titulo.Trim();
        Editora = editora.Trim();
        Edicao = edicao;
        AnoPublicacao = anoPublicacao.Trim();
    }

    public void AdicionarAutor(Autor autor)
    {
        if (autor == null)
            throw new ArgumentNullException(nameof(autor));

        if (_autores.Contains(autor))
            return;

        _autores.Add(autor);
        autor.AdicionarLivro(this);
    }

    public void RemoverAutor(Autor autor)
    {
        if (autor == null)
            return;

        if (_autores.Remove(autor))
            autor.RemoverLivro(this);
    }

    public void LimparAutores()
    {
        var autores = _autores.ToList();
        foreach (var autor in autores)
        {
            RemoverAutor(autor);
        }
    }

    public void AdicionarAssunto(Assunto assunto)
    {
        if (assunto == null)
            throw new ArgumentNullException(nameof(assunto));

        if (_assuntos.Contains(assunto))
            return;

        _assuntos.Add(assunto);
        assunto.AdicionarLivro(this);
    }

    public void RemoverAssunto(Assunto assunto)
    {
        if (assunto == null)
            return;

        if (_assuntos.Remove(assunto))
            assunto.RemoverLivro(this);
    }

    public void LimparAssuntos()
    {
        var assuntos = _assuntos.ToList();
        foreach (var assunto in assuntos)
        {
            RemoverAssunto(assunto);
        }
    }

    public void DefinirPreco(decimal valor, FormaDeCompra formaDeCompra)
    {
        var precoExistente = _precos.FirstOrDefault(p => p.FormaDeCompra == formaDeCompra);

        if (precoExistente != null)
            _precos.Remove(precoExistente);

        var novoPreco = new PrecoLivro(valor, formaDeCompra);
        _precos.Add(novoPreco);
    }

    public PrecoLivro? ObterPreco(FormaDeCompra formaDeCompra)
    {
        return _precos.FirstOrDefault(p => p.FormaDeCompra == formaDeCompra);
    }

    public void RemoverPreco(FormaDeCompra formaDeCompra)
    {
        var preco = _precos.FirstOrDefault(p => p.FormaDeCompra == formaDeCompra);
        if (preco != null)
            _precos.Remove(preco);
    }

    private static void ValidarTitulo(string titulo)
    {
        if (string.IsNullOrWhiteSpace(titulo))
            throw new ArgumentException("Título do livro não pode ser vazio.", nameof(titulo));

        if (titulo.Length > 40)
            throw new ArgumentException("Título do livro não pode exceder 40 caracteres.", nameof(titulo));
    }

    private static void ValidarEditora(string editora)
    {
        if (string.IsNullOrWhiteSpace(editora))
            throw new ArgumentException("Editora não pode ser vazia.", nameof(editora));

        if (editora.Length > 40)
            throw new ArgumentException("Editora não pode exceder 40 caracteres.", nameof(editora));
    }

    private static void ValidarEdicao(int edicao)
    {
        if (edicao <= 0)
            throw new ArgumentException("Edição deve ser um número positivo.", nameof(edicao));
    }

    private static void ValidarAnoPublicacao(string anoPublicacao)
    {
        if (string.IsNullOrWhiteSpace(anoPublicacao))
            throw new ArgumentException("Ano de publicação não pode ser vazio.", nameof(anoPublicacao));

        if (anoPublicacao.Length != 4)
            throw new ArgumentException("Ano de publicação deve ter 4 caracteres.", nameof(anoPublicacao));

        if (!int.TryParse(anoPublicacao, out int ano))
            throw new ArgumentException("Ano de publicação deve ser um número válido.", nameof(anoPublicacao));

        if (ano < 1000 || ano > DateTime.Now.Year)
            throw new ArgumentException($"Ano de publicação deve estar entre 1000 e {DateTime.Now.Year}.", nameof(anoPublicacao));
    }
}

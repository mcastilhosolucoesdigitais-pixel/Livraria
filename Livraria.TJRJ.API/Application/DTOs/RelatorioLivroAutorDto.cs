namespace Livraria.TJRJ.API.Application.DTOs;

public class RelatorioLivroAutorAgrupadoDTO
{
    public int AutorId { get; set; }
    public string AutorNome { get; set; } = string.Empty;
    public List<RelatorioLivroAutorDto> Livros { get; set; } = new();
}

public class RelatorioLivroAutorDto
{
    public int AutorId { get; set; }
    public string AutorNome { get; set; } = string.Empty;
    public int LivroId { get; set; }
    public string LivroTitulo { get; set; } = string.Empty;
    public string LivroEditora { get; set; } = string.Empty;
    public int LivroEdicao { get; set; }
    public string LivroAnoPublicacao { get; set; } = string.Empty;
    public string Assuntos { get; set; } = string.Empty;
    public decimal? PrecoBalcao { get; set; }
    public decimal? PrecoSelfService { get; set; }
    public decimal? PrecoInternet { get; set; }
    public decimal? PrecoEvento { get; set; }
}

namespace Livraria.TJRJ.API.Application.DTOs;

public class LivroDto
{
    public int Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Editora { get; set; } = string.Empty;
    public int Edicao { get; set; }
    public string AnoPublicacao { get; set; } = string.Empty;
    public List<AutorDto> Autores { get; set; } = new();
    public List<AssuntoDto> Assuntos { get; set; } = new();
    public List<PrecoLivroDto> Precos { get; set; } = new();
}

public class LivroSimplificadoDto
{
    public int Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Editora { get; set; } = string.Empty;
    public int Edicao { get; set; }
    public string AnoPublicacao { get; set; } = string.Empty;
}

public class PrecoLivroDto
{
    public decimal Valor { get; set; }
    public string FormaDeCompra { get; set; } = string.Empty;
}

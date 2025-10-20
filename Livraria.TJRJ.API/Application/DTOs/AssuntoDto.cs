namespace Livraria.TJRJ.API.Application.DTOs;

public class AssuntoDto
{
    public int Id { get; set; }
    public string Descricao { get; set; } = string.Empty;
}

public class AssuntoComLivrosDto
{
    public int Id { get; set; }
    public string Descricao { get; set; } = string.Empty;
    public List<LivroSimplificadoDto> Livros { get; set; } = new();
}

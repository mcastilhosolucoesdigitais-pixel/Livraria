namespace Livraria.TJRJ.API.Application.DTOs;

public class AssuntoDto
{
    public Guid Id { get; set; }
    public string Descricao { get; set; } = string.Empty;
}

public class AssuntoComLivrosDto
{
    public Guid Id { get; set; }
    public string Descricao { get; set; } = string.Empty;
    public List<LivroSimplificadoDto> Livros { get; set; } = new();
}

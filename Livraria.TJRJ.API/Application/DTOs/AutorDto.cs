namespace Livraria.TJRJ.API.Application.DTOs;

public class AutorDto
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
}

public class AutorComLivrosDto
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public List<LivroSimplificadoDto> Livros { get; set; } = new();
}

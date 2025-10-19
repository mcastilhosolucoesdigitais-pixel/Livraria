using Livraria.TJRJ.API.Application.Common;
using MediatR;

namespace Livraria.TJRJ.API.Application.Features.Livros.Commands;

public class AtualizarLivroCommand : IRequest<Result>
{
    public int Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Editora { get; set; } = string.Empty;
    public int Edicao { get; set; }
    public string AnoPublicacao { get; set; } = string.Empty;
    public List<int> AutoresIds { get; set; } = new();
    public List<int> AssuntosIds { get; set; } = new();
    public List<PrecoInput> Precos { get; set; } = new();
}

using Livraria.TJRJ.API.Application.Common;
using MediatR;

namespace Livraria.TJRJ.API.Application.Features.Livros.Commands;

public class CriarLivroCommand : IRequest<Result<Guid>>
{
    public string Titulo { get; set; } = string.Empty;
    public string Editora { get; set; } = string.Empty;
    public int Edicao { get; set; }
    public string AnoPublicacao { get; set; } = string.Empty;
    public List<Guid> AutoresIds { get; set; } = new();
    public List<Guid> AssuntosIds { get; set; } = new();
    public List<PrecoInput> Precos { get; set; } = new();
}

public class PrecoInput
{
    public decimal Valor { get; set; }
    public string FormaDeCompra { get; set; } = string.Empty;
}

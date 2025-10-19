using Livraria.TJRJ.API.Application.DTOs;
using Livraria.TJRJ.API.Domain.Enums;
using Livraria.TJRJ.API.Domain.Interfaces;
using Livraria.TJRJ.API.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Livraria.TJRJ.API.Infra.Repositories;

public class RelatorioRepository : IRelatorioRepository
{
    private readonly ApplicationDbContext _context;

    public RelatorioRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<RelatorioLivroAutorDto>> GetRelatorioLivrosPorAutorAsync(
        Guid? autorId = null,
        CancellationToken cancellationToken = default)
    {
        var query = _context.Livros
            .Include(l => l.Autores)
            .Include(l => l.Assuntos)
            .AsQueryable();

        if (autorId.HasValue)
        {
            query = query.Where(l => l.Autores.Any(a => a.Id == autorId.Value));
        }

        var livros = await query.ToListAsync(cancellationToken);

        var relatorio = new List<RelatorioLivroAutorDto>();

        foreach (var livro in livros)
        {
            foreach (var autor in livro.Autores)
            {
                relatorio.Add(new RelatorioLivroAutorDto
                {
                    AutorId = autor.Id,
                    AutorNome = autor.Nome,
                    LivroId = livro.Id,
                    LivroTitulo = livro.Titulo,
                    LivroEditora = livro.Editora,
                    LivroEdicao = livro.Edicao,
                    LivroAnoPublicacao = livro.AnoPublicacao,
                    Assuntos = livro.Assuntos.Select(a => a.Descricao).ToList(),
                    PrecoBalcao = livro.ObterPreco(FormaDeCompra.Balcao)?.Valor,
                    PrecoSelfService = livro.ObterPreco(FormaDeCompra.SelfService)?.Valor,
                    PrecoInternet = livro.ObterPreco(FormaDeCompra.Internet)?.Valor,
                    PrecoEvento = livro.ObterPreco(FormaDeCompra.Evento)?.Valor
                });
            }
        }

        return relatorio.OrderBy(r => r.AutorNome).ThenBy(r => r.LivroTitulo);
    }
}

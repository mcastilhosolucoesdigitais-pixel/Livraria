using Dapper;
using Livraria.TJRJ.API.Application.DTOs;
using Livraria.TJRJ.API.Domain.Enums;
using Livraria.TJRJ.API.Domain.Interfaces;
using Livraria.TJRJ.API.Infra.Data;
using Microsoft.Data.SqlClient;
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
        int? autorId = null,
        CancellationToken cancellationToken = default)
    {
        string filtro = string.Empty;
        if (autorId.HasValue)
        {
            filtro = $"WHERE a.Id = {autorId}";
        }
        using (var conn = new SqlConnection(_context.Database.GetConnectionString()))
        {
            return await conn.QueryAsync<RelatorioLivroAutorDto>($"SELECT * FROM VIEW_LISTA_PRECOS {filtro}", cancellationToken);
        }

    }

}

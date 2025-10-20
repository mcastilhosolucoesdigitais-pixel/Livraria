using Livraria.TJRJ.API.Domain.Entities;
using Livraria.TJRJ.API.Domain.Interfaces;
using Livraria.TJRJ.API.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Livraria.TJRJ.API.Infra.Repositories;

public class AssuntoRepository : IAssuntoRepository
{
    private readonly ApplicationDbContext _context;

    public AssuntoRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public IUnitOfWork UnitOfWork => _context;

    public async Task<Assunto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Assuntos.FindAsync(new object[] { id }, cancellationToken);
    }

    public async Task<IEnumerable<Assunto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Assuntos.ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Assunto entity, CancellationToken cancellationToken = default)
    {
        await _context.Assuntos.AddAsync(entity, cancellationToken);
    }

    public void Update(Assunto entity)
    {
        _context.Assuntos.Update(entity);
    }

    public void Delete(Assunto entity)
    {
        _context.Assuntos.Remove(entity);
    }

    public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Assuntos.AnyAsync(a => a.Id == id, cancellationToken);
    }

    public async Task<Assunto?> GetByIdWithLivrosAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Assuntos
            .Include(a => a.Livros)
            .FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<Assunto>> GetByDescricaoAsync(string descricao, CancellationToken cancellationToken = default)
    {
        return await _context.Assuntos
            .Where(a => a.Descricao.Contains(descricao))
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> ExistsByDescricaoAsync(string descricao, CancellationToken cancellationToken = default)
    {
        return await _context.Assuntos
            .AnyAsync(a => a.Descricao == descricao, cancellationToken);
    }

    public async Task<bool> ExistsByDescricaoExcludingIdAsync(string descricao, int id, CancellationToken cancellationToken = default)
    {
        return await _context.Assuntos
            .AnyAsync(a => a.Descricao == descricao && a.Id != id, cancellationToken);
    }
}

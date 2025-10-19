using Livraria.TJRJ.API.Domain.Entities;
using Livraria.TJRJ.API.Domain.Interfaces;
using Livraria.TJRJ.API.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Livraria.TJRJ.API.Infra.Repositories;

public class AutorRepository : IAutorRepository
{
    private readonly ApplicationDbContext _context;

    public AutorRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public IUnitOfWork UnitOfWork => _context;

    public async Task<Autor?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Autores.FindAsync(new object[] { id }, cancellationToken);
    }

    public async Task<IEnumerable<Autor>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Autores.ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Autor entity, CancellationToken cancellationToken = default)
    {
        await _context.Autores.AddAsync(entity, cancellationToken);
    }

    public void Update(Autor entity)
    {
        _context.Autores.Update(entity);
    }

    public void Delete(Autor entity)
    {
        _context.Autores.Remove(entity);
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Autores.AnyAsync(a => a.Id == id, cancellationToken);
    }

    public async Task<Autor?> GetByIdWithLivrosAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Autores
            .Include(a => a.Livros)
            .FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<Autor>> GetByNomeAsync(string nome, CancellationToken cancellationToken = default)
    {
        return await _context.Autores
            .Where(a => a.Nome.Contains(nome))
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> ExistsByNomeAsync(string nome, CancellationToken cancellationToken = default)
    {
        return await _context.Autores
            .AnyAsync(a => a.Nome == nome, cancellationToken);
    }

    public async Task<bool> ExistsByNomeExcludingIdAsync(string nome, Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Autores
            .AnyAsync(a => a.Nome == nome && a.Id != id, cancellationToken);
    }
}

using Livraria.TJRJ.API.Domain.Entities;
using Livraria.TJRJ.API.Domain.Interfaces;
using Livraria.TJRJ.API.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Livraria.TJRJ.API.Infra.Repositories;

public class LivroRepository : ILivroRepository
{
    private readonly ApplicationDbContext _context;

    public LivroRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public IUnitOfWork UnitOfWork => _context;

    public async Task<Livro?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Livros.FindAsync(new object[] { id }, cancellationToken);
    }

    public async Task<IEnumerable<Livro>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Livros.ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Livro entity, CancellationToken cancellationToken = default)
    {
        await _context.Livros.AddAsync(entity, cancellationToken);
    }

    public void Update(Livro entity)
    {
        _context.Livros.Update(entity);
    }

    public void Delete(Livro entity)
    {
        _context.Livros.Remove(entity);
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Livros.AnyAsync(l => l.Id == id, cancellationToken);
    }

    public async Task<Livro?> GetByIdWithDetailsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Livros
            .Include(l => l.Autores)
            .Include(l => l.Assuntos)
            .FirstOrDefaultAsync(l => l.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<Livro>> GetAllWithDetailsAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Livros
            .Include(l => l.Autores)
            .Include(l => l.Assuntos)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Livro>> GetByTituloAsync(string titulo, CancellationToken cancellationToken = default)
    {
        return await _context.Livros
            .Where(l => l.Titulo.Contains(titulo))
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Livro>> GetByAutorIdAsync(Guid autorId, CancellationToken cancellationToken = default)
    {
        return await _context.Livros
            .Include(l => l.Autores)
            .Include(l => l.Assuntos)
            .Where(l => l.Autores.Any(a => a.Id == autorId))
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Livro>> GetByAssuntoIdAsync(Guid assuntoId, CancellationToken cancellationToken = default)
    {
        return await _context.Livros
            .Include(l => l.Autores)
            .Include(l => l.Assuntos)
            .Where(l => l.Assuntos.Any(a => a.Id == assuntoId))
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> ExistsByTituloAsync(string titulo, CancellationToken cancellationToken = default)
    {
        return await _context.Livros
            .AnyAsync(l => l.Titulo == titulo, cancellationToken);
    }
}

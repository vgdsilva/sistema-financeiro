using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SistemaFinanceiro.Dominio.Entidades;
using SistemaFinanceiro.Server.Infra.Data.Interfaces;

namespace SistemaFinanceiro.Server.Infra.Data.Repositories;

public abstract class AbstractRepository<T> : IGenericRepository<T> where T : EntidadeControle
{
    protected readonly SistemaFinanceiroContext _context;
    protected readonly DbSet<T> _dbSet;
    protected readonly ILogger<AbstractRepository<T>> _logger;

    public AbstractRepository(SistemaFinanceiroContext context, ILogger<AbstractRepository<T>> logger)
    {
        _context = context;
        _dbSet = context.Set<T>();
        _logger = logger;
    }

    public virtual async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbSet.FindAsync(new object[] { id }, cancellationToken);
    }

    public virtual async Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet.AsNoTracking().ToListAsync(cancellationToken);
    }

    public virtual async Task<IReadOnlyList<T>> GetPagedAsync(int page, int size, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .AsNoTracking()
            .Skip((page - 1) * size)
            .Take(size)
            .ToListAsync(cancellationToken);
    }

    public virtual async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        await _dbSet.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public virtual async Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public virtual async Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
    {
        _dbSet.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public virtual async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbSet.AnyAsync(e => e.Id == id, cancellationToken);
    }
}

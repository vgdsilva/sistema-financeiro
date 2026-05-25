using SistemaFinanceiro.Dominio.Entidades;

namespace SistemaFinanceiro.Server.Infra.Data.Interfaces;

public interface IGenericRepository<T> where T : EntidadeControle
{
    Task<T?>               GetByIdAsync (Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<T>> GetAllAsync  (CancellationToken cancellationToken = default);
    Task<IReadOnlyList<T>> GetPagedAsync(int page, int size, CancellationToken cancellationToken = default);
    Task<T>                AddAsync     (T entity, CancellationToken cancellationToken = default);
    Task                   UpdateAsync  (T entity, CancellationToken cancellationToken = default);
    Task                   DeleteAsync  (T entity, CancellationToken cancellationToken = default);
    Task<bool>             ExistsAsync  (Guid id, CancellationToken cancellationToken = default);
}

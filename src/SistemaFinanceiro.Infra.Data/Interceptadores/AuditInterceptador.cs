using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using SistemaFinanceiro.Dominio.Interfaces;

namespace SistemaFinanceiro.Infra.Data.Interceptadores;


/// <summary>
/// Intercepta SaveChanges/SaveChangesAsync e persiste AuditLogs automaticamente
/// para qualquer entidade que implemente <see cref="IAuditavel"/>.
/// </summary>
public sealed class AuditInterceptador : SaveChangesInterceptor
{
    private readonly IUsuarioAuditContexto _usuarioContexto;

    // Campos técnicos que não fazem sentido auditar
    private static readonly HashSet<string> CamposIgnorados = new(StringComparer.OrdinalIgnoreCase)
    {
        "RowVersion", "ConcurrencyStamp", "xmin"
    };

    public AuditInterceptador(IUsuarioAuditContexto usuarioContexto)
    {
        _usuarioContexto = usuarioContexto;
    }

    // ── Síncrono ──────────────────────────────────────────────────────────

    public override InterceptionResult<int> SavingChanges(
        DbContextEventData eventData,
        InterceptionResult<int> result)
    {
        GerarLogs(eventData.Context);
        return base.SavingChanges(eventData, result);
    }

    // ── Assíncrono ────────────────────────────────────────────────────────

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        GerarLogs(eventData.Context);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    // ── Lógica central ────────────────────────────────────────────────────

    private void GerarLogs(DbContext? context)
    {
        if (context is null) return;

        context.ChangeTracker.DetectChanges();

        var logs = context.ChangeTracker
            .Entries()
            .Where(e => e.Entity is IAuditavel
                     && e.State  is EntityState.Added
                                 or EntityState.Modified
                                 or EntityState.Deleted)
            .Select(ConstruirLog)
            .Where(log => log is not null && log.Mudancas.PossuiMudancas)
            .Cast<AuditLog>()
            .ToList();

        if (logs.Count > 0)
            context.Set<AuditLog>().AddRange(logs);
    }

    private AuditLog? ConstruirLog(EntityEntry entry)
    {
        var tabela = entry.Metadata.GetTableName() ?? entry.Metadata.Name;
        var objetoId = ObterChavePrimaria(entry);
        var usuarioId = _usuarioContexto.UsuarioId;

        return entry.State switch
        {
            EntityState.Added => AuditLog.CriarInsert(
                tabela,
                objetoId,
                usuarioId,
                ColetarValoresAtuais(entry)),

            EntityState.Deleted => AuditLog.CriarDelete(
                tabela,
                objetoId,
                usuarioId,
                ColetarValoresOriginais(entry)),

            EntityState.Modified => AuditLog.CriarUpdate(
                tabela,
                objetoId,
                usuarioId,
                ColetarValoresOriginais(entry),
                ColetarValoresAtuais(entry)),

            _ => null
        };
    }

    // ── Helpers ───────────────────────────────────────────────────────────

    private static Dictionary<string, object?> ColetarValoresAtuais(EntityEntry entry) =>
        entry.Properties
            .Where(p => !CamposIgnorados.Contains(p.Metadata.Name))
            .ToDictionary(p => p.Metadata.Name, p => p.CurrentValue);

    private static Dictionary<string, object?> ColetarValoresOriginais(EntityEntry entry) =>
        entry.Properties
            .Where(p => !CamposIgnorados.Contains(p.Metadata.Name))
            .ToDictionary(p => p.Metadata.Name, p => p.OriginalValue);

    private static string ObterChavePrimaria(EntityEntry entry)
    {
        var partes = entry.Metadata
            .FindPrimaryKey()
            ?.Properties
            .Select(p => entry.Property(p.Name).CurrentValue?.ToString() ?? "null")
            .ToList();

        return partes?.Count > 0 ? string.Join("|", partes) : "desconhecido";
    }
}


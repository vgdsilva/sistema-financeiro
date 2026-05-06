using System.Text.Json;
using System.Text.Json.Serialization;

namespace SistemaFinanceiro.Dominio.ValueObjects;

/// <summary>
/// Value Object que representa o conjunto de mudanças ocorridas em uma entidade.
/// Imutável após criação — use os factory methods para construir.
/// </summary>
public sealed class AuditChanges
{
    private static readonly JsonSerializerOptions JsonOpts = new()
    {
        WriteIndented = false,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    private readonly Dictionary<string, AuditFieldChange> _changes;

    /// <summary>Mudanças indexadas por nome do campo.</summary>
    public IReadOnlyDictionary<string, AuditFieldChange> Valores => _changes;

    public bool PossuiMudancas => _changes.Count > 0;

    // Construtor privado: ninguém cria diretamente
    private AuditChanges(Dictionary<string, AuditFieldChange> changes)
    {
        _changes = changes;
    }

    // ── Factory methods ───────────────────────────────────────────────────

    /// <summary>
    /// Cria AuditChanges para INSERT: todos os campos ficam com Antes = null.
    /// </summary>
    public static AuditChanges ParaInsert(Dictionary<string, object?> valoresNovos)
    {
        var changes = valoresNovos.ToDictionary(
            kv => kv.Key,
            kv => AuditFieldChange.Adicionado(kv.Value));

        return new AuditChanges(changes);
    }

    /// <summary>
    /// Cria AuditChanges para DELETE: todos os campos ficam com Depois = null.
    /// </summary>
    public static AuditChanges ParaDelete(Dictionary<string, object?> valoresAntigos)
    {
        var changes = valoresAntigos.ToDictionary(
            kv => kv.Key,
            kv => AuditFieldChange.Removido(kv.Value));

        return new AuditChanges(changes);
    }

    /// <summary>
    /// Cria AuditChanges para UPDATE: inclui apenas os campos cujo valor mudou.
    /// </summary>
    public static AuditChanges ParaUpdate(
        Dictionary<string, object?> valoresAntigos,
        Dictionary<string, object?> valoresNovos)
    {
        var changes = valoresNovos
            .Where(kv =>
            {
                var antigo = valoresAntigos.GetValueOrDefault(kv.Key);
                return !Equals(antigo, kv.Value);
            })
            .ToDictionary(
                kv => kv.Key,
                kv => AuditFieldChange.Criar(valoresAntigos.GetValueOrDefault(kv.Key), kv.Value));

        return new AuditChanges(changes);
    }

    // ── Serialização (usada pelo EF Core e pela API) ──────────────────────

    public string ParaJson() =>
        JsonSerializer.Serialize(_changes, JsonOpts);

    public static AuditChanges DeJson(string json)
    {
        var dict = JsonSerializer.Deserialize<Dictionary<string, AuditFieldChange>>(json, JsonOpts)
                   ?? new Dictionary<string, AuditFieldChange>();

        return new AuditChanges(dict);
    }

    // ── Igualdade por valor (comportamento de Value Object) ───────────────

    public override bool Equals(object? obj) =>
        obj is AuditChanges other && ParaJson() == other.ParaJson();

    public override int GetHashCode() =>
        HashCode.Combine(ParaJson());
}

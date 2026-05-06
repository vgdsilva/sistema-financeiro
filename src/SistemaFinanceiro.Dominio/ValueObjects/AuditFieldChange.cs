using System.Text.Json;

namespace SistemaFinanceiro.Dominio.ValueObjects;

/// <summary>
/// Representa a mudança de um campo específico.
/// Armazena os valores serializados como string para garantir fidelidade
/// na persistência e evitar perda de tipo ao usar object?.
/// </summary>
public sealed record AuditFieldChange
{
    private static readonly JsonSerializerOptions JsonOpts = new()
    {
        WriteIndented = false,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    /// <summary>Valor antes da alteração (null em Inserts).</summary>
    public string? Antes { get; }

    /// <summary>Valor após a alteração (null em Deletes).</summary>
    public string? Depois { get; }

    private AuditFieldChange(string? antes, string? depois)
    {
        Antes = antes;
        Depois = depois;
    }

    /// <summary>
    /// Cria uma mudança serializando os valores para JSON.
    /// Aceita qualquer tipo — primitivos, enums, objetos complexos.
    /// </summary>
    public static AuditFieldChange Criar(object? antes, object? depois) =>
        new(Serializar(antes), Serializar(depois));

    /// <summary>Conveniência para campos adicionados em um INSERT.</summary>
    public static AuditFieldChange Adicionado(object? valor) =>
        new(null, Serializar(valor));

    /// <summary>Conveniência para campos removidos em um DELETE.</summary>
    public static AuditFieldChange Removido(object? valor) =>
        new(Serializar(valor), null);

    /// <summary>Tenta desserializar o valor anterior para o tipo desejado.</summary>
    public T? AntesAs<T>() => Desserializar<T>(Antes);

    /// <summary>Tenta desserializar o valor posterior para o tipo desejado.</summary>
    public T? DepoisAs<T>() => Desserializar<T>(Depois);

    // ── Helpers ──────────────────────────────────────────────────────────

    private static string? Serializar(object? valor) =>
        valor is null ? null : JsonSerializer.Serialize(valor, JsonOpts);

    private static T? Desserializar<T>(string? json) =>
        json is null ? default : JsonSerializer.Deserialize<T>(json, JsonOpts);

    // record já gera Equals/GetHashCode por Antes e Depois — perfeito para VO.
}

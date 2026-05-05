namespace SistemaFinanceiro.Dominio.ValueObjects;

/// <summary>
/// Representa a mudança de um campo específico, armazenando os valores antes e depois da alteração para fins de auditoria.
/// </summary>
/// <param name="Before"></param>
/// <param name="After"></param>
public record AuditFieldChange(object? Before, object? After);


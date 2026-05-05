namespace SistemaFinanceiro.Dominio.Entidades;

public sealed class AuditLog : EntidadeControle
{
    public DateTime          Data         { get; set; } = DateTime.UtcNow;
    public AuditOperacaoEnum Operacao     { get; set; }
    public string            TipoEntidade { get; set; } = string.Empty;
    public string            EntidadeId   { get; set; } = string.Empty;
    public string            UsuarioId    { get; set; } = string.Empty;
    public AuditChanges      Mudancas     { get; set; } = null!;
}

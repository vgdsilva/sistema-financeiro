namespace SistemaFinanceiro.Dominio.Entidades;

public abstract class EntidadeControle
{
    public Guid      Id { get; set; }
    public DateTime  CreatedAt { get; set; }
    public string?    CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string?   UpdatedBy { get; set; }
    public Guid      SystemDeleted { get; set; } = Guid.Empty;
    public string?   SystemDeletedBy { get; set; }
    public DateTime? SystemDateDeleted { get; set; }
}

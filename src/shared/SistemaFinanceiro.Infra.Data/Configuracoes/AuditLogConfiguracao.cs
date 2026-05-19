using SistemaFinanceiro.Dominio.ValueObjects;

namespace SistemaFinanceiro.Infra.Data.Configuracoes;

public sealed class AuditLogConfiguracao //: IEntityTypeConfiguration<AuditLog>
{
    public void Configure(EntityTypeBuilder<AuditLog> builder)
    {
        builder.ToTable("audit_logs");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Data)
            .HasColumnName("data")
            .IsRequired();

        builder.Property(x => x.Operacao)
            .HasColumnName("operacao")
            .HasConversion<int>()
            .HasMaxLength(10)
            .IsRequired();

        builder.Property(x => x.NomeDaTabela)
            .HasColumnName("nome_da_tabela")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.ObjetoId)
            .HasColumnName("objeto_id")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.UsuarioId)
            .HasColumnName("usuario_id")
            .HasMaxLength(100)
            .IsRequired();

        // AuditChanges → coluna JSONB (PostgreSQL) ou nvarchar(max) (SQL Server)
        // O conversor chama ParaJson()/DeJson() transparentemente.
        builder.Property(x => x.Mudancas)
            .HasColumnName("mudancas")
            .HasColumnType("jsonb")
            .IsRequired()
            .HasConversion(
                vo => vo.ParaJson(),
                json => AuditChanges.DeJson(json));

        // ── Índices ───────────────────────────────────────────────────────
        builder.HasIndex(x => x.NomeDaTabela)
            .HasDatabaseName("ix_audit_logs_nome_da_tabela");

        builder.HasIndex(x => x.ObjetoId)
            .HasDatabaseName("ix_audit_logs_objeto_id");

        builder.HasIndex(x => x.Data)
            .HasDatabaseName("ix_audit_logs_data");

        builder.HasIndex(x => new { x.NomeDaTabela, x.ObjetoId })
            .HasDatabaseName("ix_audit_logs_tabela_objeto");
    }
}

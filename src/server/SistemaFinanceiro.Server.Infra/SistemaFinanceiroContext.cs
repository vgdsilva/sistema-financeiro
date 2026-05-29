using Microsoft.EntityFrameworkCore;
using SistemaFinanceiro.Dominio.Entidades;
using SistemaFinanceiro.Dominio.Enums;

namespace SistemaFinanceiro.Server.Infra.Data;

public class SistemaFinanceiroContext : DbContext
{
    public SistemaFinanceiroContext(DbContextOptions<SistemaFinanceiroContext> options) : base(options)
    {
    }

    // Defina suas DbSet aqui, por exemplo:
    public DbSet<Usuario> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        

        // Configurações adicionais de mapeamento podem ser feitas aqui
        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.ToTable("usuario");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Nome)
                  .IsRequired();
            entity.Property(e => e.Username)
                  .IsRequired()
                  .HasMaxLength(80);
            entity.Property(e => e.Email)
                  .IsRequired();
            entity.Property(e => e.SenhaHash)
                  .IsRequired();
            entity.Property(e => e.Ativo)
                  .IsRequired()
                  .HasDefaultValue(true);
            entity.Property(e => e.TipoUsuario)
                  .IsRequired()
                  .HasConversion<int>()
                  .HasDefaultValue(TipoUsuarioEnum.USUARIO);
        });

        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            // Nome da tabela
            entity.SetTableName(entity.GetTableName()?.ToLower());

            // Nome das colunas
            foreach (var property in entity.GetProperties())
                property.SetColumnName(property.GetColumnName().ToLower());

            // Chaves e índices
            foreach (var key in entity.GetKeys())
                key.SetName(key.GetName()?.ToLower());

            foreach (var index in entity.GetIndexes())
                index.SetDatabaseName(index.GetDatabaseName()?.ToLower());

            foreach (var fk in entity.GetForeignKeys())
                fk.SetConstraintName(fk.GetConstraintName()?.ToLower());
        }

        base.OnModelCreating(modelBuilder);
    }
}

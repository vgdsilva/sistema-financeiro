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
        base.OnModelCreating(modelBuilder);

        // Configurações adicionais de mapeamento podem ser feitas aqui
        modelBuilder.Entity<Usuario>(entity =>
        {
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
    }
}

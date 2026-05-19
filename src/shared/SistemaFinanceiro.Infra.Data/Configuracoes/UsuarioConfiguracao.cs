namespace SistemaFinanceiro.Infra.Data.Configuracoes;

public class UsuarioConfiguracao //: IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.ToTable("usuario");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id");
        
        builder.Property(x => x.Nome)
            .IsRequired()
            .HasMaxLength(200)
            .HasColumnName("nome");
        
        builder.Property(x => x.Username)
            .IsRequired()
            .HasMaxLength(200)
            .HasColumnName("username");
        
        builder.Property(x => x.Email)
            .IsRequired()
            .HasMaxLength(200)
            .HasColumnName("email");
        
        builder.Property(x => x.SenhaHash)
            .IsRequired()
            .HasMaxLength(200)
            .HasColumnName("senhahash");
        
        builder.Property(x => x.Ativo)
            .IsRequired()
            .HasColumnName("ativo");

        builder.Property(x => x.Root)
            .IsRequired()
            .HasColumnName("root");

        builder.HasMany(x => x.Sessoes)
            .WithOne(x => x.Usuario)
            .HasForeignKey(x => x.UsuarioId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

namespace SistemaFinanceiro.Infra.Data.Configurations;

public class CategoriaConfiguration : IEntityTypeConfiguration<Categoria>
{
    public void Configure(EntityTypeBuilder<Categoria> builder)
    {
        builder.ToTable("categoria");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.Descricao).IsRequired().HasMaxLength(200).HasColumnName("descricao");
        builder.Property(x => x.DirecaoTransacao).IsRequired().HasConversion<int>().HasColumnName("direcaotransacao") ;
        builder.Property(x => x.Ativo).IsRequired().HasColumnName("ativo");

        builder.HasIndex(x => x.Ativo, "IX_Categoria_Ativo");
    }
}

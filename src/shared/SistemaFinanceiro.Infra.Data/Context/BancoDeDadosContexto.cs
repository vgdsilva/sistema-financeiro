namespace SistemaFinanceiro.Infra.Data.Context;

public class BancoDeDadosContexto : DbContext
{

    public DbSet<Categoria> Categorias => Set<Categoria>();

    public BancoDeDadosContexto(DbContextOptions<BancoDeDadosContexto> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BancoDeDadosContexto).Assembly);
    }
}

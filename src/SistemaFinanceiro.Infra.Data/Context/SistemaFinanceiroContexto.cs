namespace SistemaFinanceiro.Infra.Data.Context;

public class SistemaFinanceiroContexto : DbContext
{
    public SistemaFinanceiroContexto(DbContextOptions<SistemaFinanceiroContexto> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SistemaFinanceiroContexto).Assembly);
    }
}

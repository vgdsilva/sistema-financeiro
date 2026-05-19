using Microsoft.Extensions.DependencyInjection;
using SistemaFinanceiro.Infra.Data.Context;

namespace SistemaFinanceiro;

public static class ProjectExtensions
{
    public static IServiceCollection AdicionarInfraestrutura(this IServiceCollection services, string stringDeConexao)
    {
        services.AddPooledDbContextFactory<BancoDeDadosContexto>((sp, options) =>
        {
            options.UseNpgsql(stringDeConexao);
        });

        return services;
    }
}

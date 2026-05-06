using Microsoft.Extensions.DependencyInjection;
using SistemaFinanceiro.Infra.Data.Context;
using SistemaFinanceiro.Infra.Data.Interceptadores;
using SistemaFinanceiro.Infra.Data.Utils;

namespace SistemaFinanceiro;

public static class ProjectExtensions
{
    public static IServiceCollection AdicionarInfraestrutura(this IServiceCollection services, string stringDeConexao)
    {
        services.AddHttpContextAccessor();

        // Contexto do usuário por request HTTP
        services.AddScoped<IUsuarioAuditContexto, UsuarioAuditHttpContexto>();

        // Interceptador Scoped (precisa do IUsuarioAuditContexto)
        services.AddScoped<AuditInterceptador>();

        services.AddDbContext<SistemaFinanceiroContexto>((sp, options) =>
        {
            var interceptador = sp.GetRequiredService<AuditInterceptador>();

            options
                .UseNpgsql(stringDeConexao)
                .AddInterceptors(interceptador);
        });

        return services;
    }
}

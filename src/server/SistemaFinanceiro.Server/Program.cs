
using Microsoft.EntityFrameworkCore;
using SistemaFinanceiro.Server.Infra.Data;
using SistemaFinanceiro.Server.Infra.Data.Repositories;

namespace SistemaFinanceiro.Server;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllers();

        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();

        var connectionsString = builder.Configuration.GetConnectionString("PostgreSQL");

        builder.Services.AddDbContext<SistemaFinanceiroContext>(options =>
        {
            options.UseNpgsql(connectionsString, sqlOptions =>
            {
                sqlOptions.CommandTimeout(30);
                sqlOptions.EnableRetryOnFailure(maxRetryCount: 3, maxRetryDelay: TimeSpan.FromSeconds(5), errorCodesToAdd: null);
            });

        }, ServiceLifetime.Scoped);

        builder.Services.AddDbContextPool<SistemaFinanceiroContext>(options => { options.UseNpgsql(connectionsString); }, poolSize: 120);

        builder.Services.AddScoped<UsuarioRepository>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}

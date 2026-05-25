using Microsoft.Extensions.Logging;
using SistemaFinanceiro.Dominio.Entidades;

namespace SistemaFinanceiro.Server.Infra.Data.Repositories;

public class UsuarioRepository : AbstractRepository<Usuario>
{
    public UsuarioRepository(SistemaFinanceiroContext context, ILogger<AbstractRepository<Usuario>> logger) : base(context, logger)
    {

    }
}

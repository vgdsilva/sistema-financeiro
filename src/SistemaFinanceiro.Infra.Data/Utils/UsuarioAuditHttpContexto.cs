using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace SistemaFinanceiro.Infra.Data.Utils;

/// <summary>
/// Extrai o usuário autenticado do HttpContext (JWT/Cookie).
/// Registre como Scoped no container de DI.
/// </summary>
public sealed class UsuarioAuditHttpContexto : IUsuarioAuditContexto
{
    private readonly IHttpContextAccessor _accessor;

    public UsuarioAuditHttpContexto(IHttpContextAccessor accessor)
    {
        _accessor = accessor;
    }

    public string UsuarioId =>
        _accessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier)
        ?? _accessor.HttpContext?.User.FindFirstValue("sub")
        ?? "anonimo";
}



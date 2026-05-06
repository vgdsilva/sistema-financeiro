namespace SistemaFinanceiro.Infra.Data.Interfaces;

/// <summary>
/// Fornece o ID do usuário autenticado para o interceptador.
/// Registre sua implementação no container de DI.
/// </summary>
public interface IUsuarioAuditContexto
{
    string UsuarioId { get; }
}

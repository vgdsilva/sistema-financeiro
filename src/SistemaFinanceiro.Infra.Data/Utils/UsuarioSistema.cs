using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaFinanceiro.Infra.Data.Utils;

/// <summary>
/// Implementação para jobs de background ou testes onde não há HttpContext.
/// </summary>
public sealed class UsuarioSistema : IUsuarioAuditContexto
{
    public string UsuarioId => "SISTEMA";
}

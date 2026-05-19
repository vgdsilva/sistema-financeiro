using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaFinanceiro.Dominio.Interfaces;

/// <summary>
/// Marque suas entidades de domínio com esta interface
/// para que sejam auditadas automaticamente pelo AuditInterceptador.
/// </summary>
public interface IAuditavel { }
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaFinanceiro.Dominio.Entidades;

public abstract class EntidadeControle
{
    public Guid Id { get; set; }
}

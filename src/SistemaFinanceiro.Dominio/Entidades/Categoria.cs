using SistemaFinanceiro.Dominio.Interfaces;

namespace SistemaFinanceiro.Dominio.Entidades;

public class Categoria : EntidadeControle, IAuditavel
{
    public string               Descricao        { get; set; } = string.Empty;
    public DirecaoTransacaoEnum DirecaoTransacao { get; set; }
    public bool                 Ativo            { get; set; } = true;
}

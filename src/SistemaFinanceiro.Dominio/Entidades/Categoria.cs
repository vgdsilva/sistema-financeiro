using SistemaFinanceiro.Dominio.Enums;

namespace SistemaFinanceiro.Dominio.Entidades;

public class Categoria : EntidadeControle
{
    public string               Descricao        { get; set; } = string.Empty;
    public DirecaoTransacaoEnum DirecaoTransacao { get; set; }
}

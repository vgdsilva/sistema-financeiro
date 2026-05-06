namespace SistemaFinanceiro.Dominio.Entidades;

public class Usuario : EntidadeControle
{
    public string Nome      { get; set; } = string.Empty;
    public string Username  { get; set; } = string.Empty;
    public string Email     { get; set; } = string.Empty;
    public string SenhaHash { get; set; } = string.Empty;
    public bool   Ativo     { get; set; } = true;
    public bool   Root      { get; set; } = false;


    public ICollection<UsuarioSessao> Sessoes { get; set; } = new List<UsuarioSessao>();
}

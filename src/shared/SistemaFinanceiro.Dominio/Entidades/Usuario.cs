using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaFinanceiro.Dominio.Entidades;

public class Usuario : EntidadeControle
{
    [Column("nome")]
    public string Nome      { get; set; } = string.Empty;

    [Column("username")]
    public string Username  { get; set; } = string.Empty;

    [Column("email")]
    public string Email     { get; set; } = string.Empty;

    [Column("senha_hash")]
    public string SenhaHash { get; set; } = string.Empty;

    [Column("ativo")]
    public bool   Ativo     { get; set; } = true;

    [Column("root")]
    public bool   Root      { get; set; } = false;


    public ICollection<UsuarioSessao> Sessoes { get; set; } = new List<UsuarioSessao>();
}

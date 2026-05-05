using SistemaFinanceiro.Dominio.Enums;
using System.Security.Cryptography;

namespace SistemaFinanceiro.Dominio.Entidades;

public class Sessao : EntidadeControle
{
    public Guid?                UsuarioId      { get; set; } = Guid.Empty;
    public TipoAutorSessaoEnum TipoAutorSessao { get; set; }
    public string              Token           { get; set; } = string.Empty;
    public string              Ip              { get; set; } = string.Empty;
    public string?             UserAgent       { get; set; } = string.Empty;
    public DateTime            CriadoEm        { get; set; } = DateTime.UtcNow;
    public DateTime?           EncerradaEm     { get; set; } = null;

    public bool Ativo => EncerradaEm is null;

    public virtual Usuario Usuario { get; set; }

    public static Sessao ParaUsuario(Guid usuarioId, string ip, string? userAgent, Usuario? usuario = null) =>
        new()
        {
            UsuarioId = usuarioId,
            Usuario = usuario,
            TipoAutorSessao = TipoAutorSessaoEnum.USUARIO,
            Token = GerarToken(),
            Ip = ip,
            UserAgent = userAgent
        };

    // Cadastro público, sem usuário ainda
    public static Sessao Anonima(string ip, string? userAgent) =>
        new()
        {
            UsuarioId = null,
            TipoAutorSessao = TipoAutorSessaoEnum.ANONIMO,
            Token = GerarToken(),
            Ip = ip,
            UserAgent = userAgent,
            CriadoEm = DateTime.UtcNow
        };

    // Jobs, seeds, migrations
    public static Sessao DoSistema() =>
        new()
        {
            UsuarioId = null,
            TipoAutorSessao = TipoAutorSessaoEnum.SISTEMA,
            Token = GerarToken(),
            Ip = "127.0.0.1",
            UserAgent = null
        };

    public void Encerrar() => EncerradaEm = DateTime.UtcNow;

    private static string GerarToken() =>
        Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
}

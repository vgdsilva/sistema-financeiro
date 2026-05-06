using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaFinanceiro.Dominio.Entidades;

public class UsuarioSessao : EntidadeControle
{
    public Guid?     UsuarioId              { get; set; }
    public string    TokenSessao            { get; set; }
    public string?   CsrfToken              { get; set; }
    public string?   IpAddress              { get; set; }
    public string?   UserAgent              { get; set; }
    public DateTime  CriadoEm               { get; set; }
    public DateTime  UltimaAtividadeEm      { get; set; }
    public DateTime  ExpiraEm               { get; set; }
    public DateTime? RevogadoEm             { get; set; }
    public bool      EstaAtivo              { get; set; }
    public string?   SessaoData             { get; set; }
    public string?   InformacoesDispositivo { get; set; }

    // ── Navigation Properties ─────────────────────────────────────────────

    public virtual Usuario? Usuario { get; set; }

    // ── Métodos de Domínio ────────────────────────────────────────────────

    public bool EstaValido(DateTime? utcNow = null)
    {
        var now = utcNow ?? DateTime.UtcNow;
        return EstaAtivo
            && RevogadoEm is null
            && ExpiraEm > now;
    }

    public void Revogar(DateTime? utcNow = null)
    {
        RevogadoEm = utcNow ?? DateTime.UtcNow;
        EstaAtivo = false;
    }

    public void Renovar(TimeSpan slidingDuration, DateTime? utcNow = null)
    {
        var now = utcNow ?? DateTime.UtcNow;
        UltimaAtividadeEm = now;
        ExpiraEm = now.Add(slidingDuration);
    }
}

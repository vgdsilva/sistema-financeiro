namespace SistemaFinanceiro.Dominio.Entidades;

public sealed class AuditLog : EntidadeControle
{
    // ── Propriedades somente-leitura após criação ─────────────────────────

    public DateTime          Data         { get; private set; }
    public AuditOperacaoEnum Operacao     { get; private set; }
    public string            NomeDaTabela { get; private set; } = string.Empty;
    public string            ObjetoId     { get; private set; } = string.Empty;
    public string            UsuarioId    { get; private set; } = string.Empty;
    public AuditChanges      Mudancas     { get; private set; } = null!;

    // Construtor vazio exigido pelo EF Core
    private AuditLog() { }

    private AuditLog(
        AuditOperacaoEnum operacao,
        string nomeDaTabela,
        string objetoId,
        string usuarioId,
        AuditChanges mudancas)
    {
        Data = DateTime.UtcNow;
        Operacao = operacao;
        NomeDaTabela = nomeDaTabela;
        ObjetoId = objetoId;
        UsuarioId = usuarioId;
        Mudancas = mudancas;
    }

    // ── Factory methods espelhando as operações possíveis ─────────────────

    public static AuditLog CriarInsert(
        string nomeDaTabela,
        string objetoId,
        string usuarioId,
        Dictionary<string, object?> valoresNovos) =>
        new(
            AuditOperacaoEnum.CREATE,
            nomeDaTabela,
            objetoId,
            usuarioId,
            AuditChanges.ParaInsert(valoresNovos));

    public static AuditLog CriarUpdate(
        string nomeDaTabela,
        string objetoId,
        string usuarioId,
        Dictionary<string, object?> valoresAntigos,
        Dictionary<string, object?> valoresNovos) =>
        new(
            AuditOperacaoEnum.UPDATE,
            nomeDaTabela,
            objetoId,
            usuarioId,
            AuditChanges.ParaUpdate(valoresAntigos, valoresNovos));

    public static AuditLog CriarDelete(
        string nomeDaTabela,
        string objetoId,
        string usuarioId,
        Dictionary<string, object?> valoresAntigos) =>
        new(
            AuditOperacaoEnum.DELETE,
            nomeDaTabela,
            objetoId,
            usuarioId,
            AuditChanges.ParaDelete(valoresAntigos));
}

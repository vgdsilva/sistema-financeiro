
namespace SistemaFinanceiro.Infra.Data.Configuracoes;

public class UsuarioSessaoConfiguracao : IEntityTypeConfiguration<UsuarioSessao>
{
    public void Configure(EntityTypeBuilder<UsuarioSessao> builder)
    {
        builder.ToTable("usuario_sessao");
        builder.HasKey(us => us.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id");

        builder.Property(us => us.UsuarioId)
            .HasColumnName("usuario_id")
            .IsRequired(false);

        builder.Property(us => us.TokenSessao)
            .HasColumnName("token_sessao")
            .IsRequired();

        builder.Property(us => us.CsrfToken)
            .HasColumnName("csrf_token")
            .IsRequired(false);

        builder.Property(us => us.IpAddress)
            .HasColumnName("ip_address")
            .IsRequired(false);

        builder.Property(us => us.UserAgent)
            .HasColumnName("user_agent")
            .IsRequired(false);

        builder.Property(us => us.CriadoEm)
            .HasColumnName("criado_em")
            .IsRequired();

        builder.Property(us => us.UltimaAtividadeEm)
            .HasColumnName("ultima_atividade_em")
            .IsRequired();

        builder.Property(us => us.ExpiraEm)
            .HasColumnName("expira_em")
            .IsRequired();

        builder.Property(us => us.RevogadoEm)
            .HasColumnName("revogado_em")
            .IsRequired(false);

        builder.Property(us => us.EstaAtivo)
            .HasColumnName("esta_ativo")
            .IsRequired();

        builder.Property(us => us.SessaoData)
            .HasColumnName("sessao_data")
            .IsRequired(false);

        builder.Property(us => us.InformacoesDispositivo)
            .HasColumnName("informacoes_dispositivo")
            .IsRequired(false);

        builder.HasOne(us => us.Usuario)
            .WithMany()
            .HasForeignKey(us => us.UsuarioId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}

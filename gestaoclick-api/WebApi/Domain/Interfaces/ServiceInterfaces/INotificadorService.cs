using Services.Notificacoes;

namespace Domain.Interfaces
{
    public interface INotificadorService
    {
        bool TemNotificacao();
        List<Notificacao> ObterNotificacoes();
        void Handle(Notificacao notificacao);
    }
}
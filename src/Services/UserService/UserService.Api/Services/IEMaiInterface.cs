

namespace UserService.Api.Service
{
    //Kullanıcı şifre yenileme bağlantısı mailine gönderen fonksiyonun interfacesi
    public interface IEMailInterface
    {
        Task EMailLinkSend(string email);
    }
}

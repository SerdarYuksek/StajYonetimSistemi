using NETCore.MailKit.Core;

namespace IdentityService.Api.Service
{
    public interface IEMailInterface
    {
        Task EMailSend(string email);
    }
}

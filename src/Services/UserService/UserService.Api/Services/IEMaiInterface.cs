using NETCore.MailKit.Core;

namespace UserService.Api.Service
{
    public interface IEMailInterface
    {
        Task EMailLinkSend(string email);
    }
}

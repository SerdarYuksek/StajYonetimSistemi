using IdentityService.Api.Service;
using MailKit.Net.Smtp;
using MimeKit;


namespace IdentityService.Api.Manager
{
    public class EMailRepository : IEMailInterface
    {
        private readonly string senderadress = "l2012729008@isparta.edu.tr";
        public async Task EMailSend(string email)
        {
            var EMail = new MimeMessage();
            MailboxAddress mailBoxAddressFrom = new MailboxAddress("Staj Takip Sistemi", senderadress);
            MailboxAddress mailboxAddressTo = new MailboxAddress("User", email);

            EMail.From.Add(mailBoxAddressFrom);
            EMail.To.Add(mailboxAddressTo);

            var bodybuilder = new BodyBuilder();
            bodybuilder.TextBody = "Kayıt İşlemini Gerçekleştirmek İçin Onay Kodunuz";
            EMail.Body = bodybuilder.ToMessageBody();

            EMail.Subject = "Staj Takip Sistemi Onay Kodu";

            SmtpClient client = new SmtpClient();

            await client.ConnectAsync("eposta.isparta.edu.tr", 587, MailKit.Security.SecureSocketOptions.StartTlsWhenAvailable);
            await client.AuthenticateAsync("l2012729008@isparta.edu.tr", "31884234");
            await client.SendAsync(EMail);
            await client.DisconnectAsync(true);
        }
    }
}

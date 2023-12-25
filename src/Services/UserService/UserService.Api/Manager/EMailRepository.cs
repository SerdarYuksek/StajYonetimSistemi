using MailKit.Net.Smtp;
using MimeKit;
using UserService.Api.Service;


namespace UserService.Api.Manager
{
    //Şifre sıfırlama işlemi için gelen mail bilgisine sayfa bağlantısı linkini gönderme 
    public class EMailRepository : IEMailInterface
    {
        private readonly string senderadress = "l2012729008@isparta.edu.tr"; //gönderen adres
        private readonly string link = "Şifre Sıfırlama Bağlantısının adresi yazılacak"; //link adresi
        public async Task EMailLinkSend(string email)
        {
            var EMail = new MimeMessage();
            MailboxAddress mailBoxAddressFrom = new MailboxAddress("Staj Takip Sistemi", senderadress);
            MailboxAddress mailboxAddressTo = new MailboxAddress("User", email);

            EMail.From.Add(mailBoxAddressFrom);
            EMail.To.Add(mailboxAddressTo);

            var bodybuilder = new BodyBuilder();
            bodybuilder.TextBody = $"<p>Şifre Sıfırlama İşlemini Gerçekleştirmek için Bağlantı Adresiniz: <a href='{link}'>TIKLAYINIZ</a>.</p>"; //mail bodysi
            EMail.Body = bodybuilder.ToMessageBody();

            EMail.Subject = "Staj Yönetim Sistemi Şifre Sıfırlama Bağlantısı"; //mail alt başlığı

            SmtpClient client = new SmtpClient();

            await client.ConnectAsync("eposta.isparta.edu.tr", 587, MailKit.Security.SecureSocketOptions.StartTlsWhenAvailable); //mail bağlantısı
            await client.AuthenticateAsync("l2012729008@isparta.edu.tr", "31884234"); //mail hesabına authentice olma
            await client.SendAsync(EMail); //mail gönderme
            await client.DisconnectAsync(true); //hesaptan çıkma
        }
    }
}

using MailKit.Net.Smtp;
using MimeKit;
using MovieApp.Interfaces;

namespace MovieApp.Repository
{
    public class EmailRepository : IEmail
    {
        public async Task SendEmailAsync(string body)
        {
            var mail = new MimeMessage();
            mail.From.Add(MailboxAddress.Parse("virgie40@ethereal.email"));
            mail.To.Add(MailboxAddress.Parse("virgie40@ethereal.email"));
            mail.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = body };
            mail.Subject = "Email Sender";
            using var smtp = new SmtpClient();
            await  smtp.ConnectAsync("smtp.ethereal.email", 587, MailKit.Security.SecureSocketOptions.StartTls);
            await  smtp.AuthenticateAsync("virgie40@ethereal.email", "1R9dsUBNb9GnYhW7qt");
            await  smtp.SendAsync(mail);
            await smtp.DisconnectAsync(true);
        }
    }
}

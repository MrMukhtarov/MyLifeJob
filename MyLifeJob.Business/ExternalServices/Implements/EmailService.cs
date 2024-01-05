using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MyLifeJob.Business.ExternalServices.Interfaces;
using MyLifeJob.Core.Entity;

namespace MyLifeJob.Business.ExternalServices.Implements;

public class EmailService : IEmailService
{
    private readonly EmailConfiguration _configuration;

    public EmailService(EmailConfiguration configuration)
    {
        _configuration = configuration;
    }

    private MimeMessage CreateEmailMessage(Message message)
    {
        var emailmessage = new MimeMessage();
        emailmessage.From.Add(new MailboxAddress("email", _configuration.From));
        emailmessage.To.AddRange(message.To);
        emailmessage.Subject = message.Subject;
        emailmessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = message.Content };

        return emailmessage;
    }

    public void SendEmail(Message message)
    {
        var emailMessage = CreateEmailMessage(message);
        Send(emailMessage);
    }

    private void Send(MimeMessage mailMessage)
    {
        using var client = new SmtpClient();
        try
        {
            client.Connect(_configuration.SmtpServer, _configuration.Port, SecureSocketOptions.StartTls);
            client.AuthenticationMechanisms.Remove("XDAUTH2");
            client.Authenticate(_configuration.UserName, _configuration.Password);

            client.Send(mailMessage);
        }
        catch (Exception)
        {

            throw;
        }
        finally
        {
            client.Disconnect(true);
            client.Dispose();
        }
    }
}

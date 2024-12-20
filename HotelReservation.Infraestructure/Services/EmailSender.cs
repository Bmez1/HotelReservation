using HotelReservation.Application.Interfaces;
using HotelReservation.Infraestructure.Configurations;

using MailKit.Net.Smtp;

using Microsoft.Extensions.Options;

using MimeKit;

using Serilog;

namespace HotelReservation.Infraestructure.Services;

internal class EmailSender(ILogger logger, IOptions<SmtpConfiguration> options) : IEmailSender
{
    public SmtpConfiguration SmtpConfiguration { get; } = options.Value;
    public async Task SendEmailAsync(string email, string subject, string body)
    {
        logger.Information("Sending email to {To} from {From} with subject {Subject}.", email, SmtpConfiguration.Sender, subject);

        using var client = new SmtpClient();
        await client.ConnectAsync(SmtpConfiguration.Host, SmtpConfiguration.Port, false);
        await client.AuthenticateAsync(SmtpConfiguration.User, SmtpConfiguration.Key);
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress(SmtpConfiguration.Sender, SmtpConfiguration.Sender));
        message.To.Add(new MailboxAddress(email, email));
        message.Subject = subject;
        message.Body = new TextPart("plain") { Text = body };

        await client.SendAsync(message);

        await client.DisconnectAsync(true,
          new CancellationToken(canceled: true));
    }
}

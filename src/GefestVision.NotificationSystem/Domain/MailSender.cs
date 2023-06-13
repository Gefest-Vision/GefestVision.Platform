using System.Net;
using System.Net.Mail;
using System.Text;

namespace GefestVision.NotificationSystem.Domain;

public sealed class MailSender
{
    private readonly Logger<NotifyMail>? _logger ;

    private IConfiguration _config;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="config"></param>
    public MailSender(Logger<NotifyMail> logger, IConfiguration config)
    {
        _logger = logger;
        _config = config;
    }

    public void SendMail(string email, string subject, string body, MailAddress? mailAddress = null)
    {

        try
        {
            // if (!_config.EnableMail)
            // {
            //     _logger.Debug("Email : {0} {1} \t Subject: {2} {3} Body: {4}", email, Environment.NewLine, subject,
            //         Environment.NewLine, body);
            //     return;
            // }

            mailAddress ??= new MailAddress(Config.MailSetting.SmtpReply, Config.MailSetting.SmtpUser);
            MailMessage message = new MailMessage(
                mailAddress,
                new MailAddress(email))
            {
                Subject = subject,
                BodyEncoding = Encoding.UTF8,
                Body = body,
                IsBodyHtml = true,
                SubjectEncoding = Encoding.UTF8
            };
            SmtpClient client = new SmtpClient
            {
                Host = _config.MailSetting.SmtpServer,
                Port = _config.MailSetting.SmtpPort,
                UseDefaultCredentials = false,
                EnableSsl = _config.MailSetting.EnableSsl,
                Credentials =
                    new NetworkCredential(_config.MailSetting.SmtpUserName,
                        _config.MailSetting.SmtpPassword),
                DeliveryMethod = SmtpDeliveryMethod.Network
            };
            client.Send(message);
        }
        catch (Exception ex)
        {
            _logger.Error("Mail send exception", ex.Message);
        }
    }
}

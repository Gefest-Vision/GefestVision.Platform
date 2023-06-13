namespace GefestVision.NotificationSystem.Domain;

public class NotifyMail
{
    private Logger<NotifyMail>? _logger ;

    private IConfiguration _config;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="config"></param>
    public NotifyMail(Logger<NotifyMail> logger, IConfiguration config)
    {
        _logger = logger;
        _config = config;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="templateName"></param>
    /// <param name="email"></param>
    /// <param name="subject"></param>
    /// <param name="body"></param>
    public void SendNotify(
        string templateName,
        string email,
        Func<string, string> subject,
        Func<string, string> body)
    {
        var template = _config.MailTemplates.FirstOrDefault(p => string.Compare(p.Name, templateName, true) == 0);
        if (template == null)
        {
            _logger.LogCritical("Can't find template (" + templateName + ")");
        }
        else
        {
            MailSender.SendMail(email,
                subject.Invoke(template.Subject),
                body.Invoke(template.Template));
        }
    }
}


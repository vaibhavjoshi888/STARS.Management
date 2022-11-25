using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using STARS.Management.Core.DTO;
using STARS.Management.Core.Interface;
using STARS.Management.Core.Models;

namespace STARS.Management.Core.Services;
public class EmailService : IEmailService
{
    private IEmailSettings _emailSettings;
    public EmailService(IEmailSettings emailSettings)
    {
        _emailSettings = emailSettings;
    }

    public async Task SendEmail(string to, string subject, EmailDTO body)
    {
        MailMessage mm = new MailMessage();
        mm.Body = GetEmailTemplate(body);
        mm.Subject = subject;
        mm.IsBodyHtml = true;
        mm.To.Add(to);
        mm.From = new MailAddress("no-reply@myapp.com");
        await _emailSettings.SendEmail(mm);
    }

    private string GetEmailTemplate(EmailDTO body)
    {
        string readText = File.ReadAllText("Email.html");
        var htmlToSend = readText.Replace("{PlaceholderCongrats}",body.PlaceholderCongrats)
            .Replace("{PlaceholderName}", body.FullName)
            .Replace("{PlaceholderEmail}", body.Email)
            .Replace("{PlaceholderPhone}",body.Phone)
            .Replace("{PlaceholderManager}",body.Manager)
            .Replace("{PlaceholderDate}", body.CreatedDate.ToString())
            .Replace("{PlaceholderMessage}", body.PlaceholderMessage)
            .Replace("{PlaceholderButtonText}", body.PlaceholderButtonText)
            .Replace("{PlaceholderView}",body.PlaceholderView);
        return htmlToSend;
    }

}


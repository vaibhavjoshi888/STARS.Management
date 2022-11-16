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
    
    public async Task SendEmail(string from, string to, string subject, string body)
    {
        MailMessage mm = new MailMessage();
        mm.Body = GetEmailTemplate();
        mm.Subject = subject;
        mm.IsBodyHtml = true;
        mm.To.Add(to);
        mm.From = new MailAddress(from);
        await _emailSettings.SendEmail(mm);
    }

    private string GetEmailTemplate()
    {
        string readText = File.ReadAllText("Email.html");
        StringBuilder strBuilder = new StringBuilder();
        StringBuilder strBuildeFinalr = new StringBuilder();

        var htmlToSend = readText.Replace("{placeholder}", "");
        return htmlToSend;
    }

}


using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using STARS.Management.Core.Interface;

namespace STARS.Management.Infrastructure.Utility;
public class EmailSettings : IEmailSettings
{
    private readonly SmtpClient _smtoclient;
    public EmailSettings()
    {
        _smtoclient=new SmtpClient(){
            Host="",
            Port=00
        };
    }

    public async Task SendEmail(MailMessage mailMessage)
    {
       await  _smtoclient.SendMailAsync(mailMessage);
    }
}
using System;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Net;
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
        // _smtoclient=new SmtpClient(){
        //     Host="",
        //     Port=00
        // };
       
    }

    public async Task SendEmail(MailMessage mailMessage)
    {
        try
        {
            var client = new SmtpClient("smtp.mailtrap.io", 2525)
            {
                Credentials = new NetworkCredential("248cb4cbeccccb", "ee3a33d09022eb"),
                EnableSsl = true
            };
          await  client.SendMailAsync(mailMessage);
        }
        catch(Exception ex)
        {
            throw ex;
        }

    }
}
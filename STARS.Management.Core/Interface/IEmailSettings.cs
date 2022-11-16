using System.Collections.Generic;
using System.Net.Mail;
using System.Threading.Tasks;
using STARS.Management.Core.DTO;
using STARS.Management.Core.Models;

namespace STARS.Management.Core.Interface;
public interface IEmailSettings
{
   Task SendEmail(MailMessage mailMessage);
    
}


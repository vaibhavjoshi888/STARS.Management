using STARS.Management.Core.DTO;
using STARS.Management.Core.Models;

namespace STARS.Management.Core.Interface;




public interface ILDAPService
{
    ADUser GetUserFromAD(string userNameOrEmail, bool isEmail);
    bool IsValidADUser(string corpUserId, string pwd);
    
}


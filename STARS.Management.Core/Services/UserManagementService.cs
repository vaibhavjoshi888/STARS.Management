using System.Collections.Generic;
using System.Linq;
using STARS.Management.Core.DTO;
using STARS.Management.Core.Interface;
using STARS.Management.Core.Repository;

namespace STARS.Management.Core.Services;

public class UserManagementService : IUserManagementService
{

    private readonly IUserManagementRepository _userManagementRepository;
    private readonly ILDAPService _lDAPService;

    public UserManagementService(IUserManagementRepository userManagementRepository, ILDAPService lDAPService)
    {
        _userManagementRepository = userManagementRepository;
        _lDAPService = lDAPService;
    }

    public void SaveUser(UserDTO user)
    {
        if (ValidateUser(user))
            _userManagementRepository.CreateUser(user);
    }

    public IEnumerable<UserDTO> GetAllUsers()
    {
        return _userManagementRepository.GetAllUsers().Result;
    }

    public SignedInUserDTO IsvalidUser(LogInDTO loginDTO)
    {
        if (_lDAPService.IsValidADUser(loginDTO.UserName, loginDTO.Password))
        {
            var user = _userManagementRepository.GetUserByCorpUserId(loginDTO.UserName).Result;
            if (user != null)
            {
                var adUserInfo = _lDAPService.GetUserFromAD(loginDTO.UserName, false);
                SignedInUserDTO signedInUser = new SignedInUserDTO();
                signedInUser.CorpUserId = string.Format(@"CORP\{0}", adUserInfo.CorpID);
                signedInUser.DisplayName = adUserInfo.DisplayName;
                signedInUser.FirstName = adUserInfo.GivenName;
                signedInUser.LastName = adUserInfo.Surname;
                signedInUser.Email = adUserInfo.Email;
                signedInUser.AppUserId = user.AppUserId;
                signedInUser.RoleId = user.RoleId;
                signedInUser.RoleName = user.RoleDisplayName;
                
                return signedInUser;
            }
        }

        return null;
    }

    public List<UserDTO> SearchUser(string username)
    {
        throw new System.NotImplementedException();
    }

    public void UpdateUser(string appuserid, UserAssignRoleDTO user)
    {
        if (ValidateUpdateUserUser(user))
            _userManagementRepository.UpdateUser(user);
    }

    public void DeleteUser(string appuserid)
    {
        _userManagementRepository.DeleteUser(appuserid);
    }

    public List<RolesDTO> GetAllRoles()
    {
        return _userManagementRepository.GetAllRoles().Result.ToList();
    }
    private bool ValidateUser(UserDTO user)
    {
        var userinfo = _lDAPService.GetUserFromAD(user.CorpID, false);

        if (userinfo != null)
        {
            var appuser = _userManagementRepository.GetUserByCorpUserId(user.CorpID).Result;
            if (appuser == null)
            {
                return true;
            }
        }
        return false;
    }

    private bool ValidateUpdateUserUser(UserAssignRoleDTO user)
    {
        var userinfo = _lDAPService.GetUserFromAD(user.CorpID, false);

        if (userinfo != null)
        {
            var appuser = _userManagementRepository.GetUserByCorpUserId(user.CorpID).Result;
            if (appuser != null)
            {
                return true;
            }
        }
        return false;
    }

}

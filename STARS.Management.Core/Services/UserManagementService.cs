using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
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
        // if (_lDAPService.IsValidADUser(loginDTO.UserName, loginDTO.Password))
        // {
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
            else
            {
                SignedInUserDTO signedInUser = new SignedInUserDTO();
                signedInUser.CorpUserId = string.Format(@"CORP\{0}","thakapds");
                signedInUser.DisplayName ="Shrikant";
                signedInUser.FirstName = "Reddy";
                signedInUser.LastName = "Reddy";
                signedInUser.Email = "Reddy@shrikant.com";
                return signedInUser;
            }
        //}

        return null;
    }

  
    public List<UserDTO> SearchUser(string username)
    {
        var userDto= _lDAPService.SearchADUsers(username);
        if (userDto != null)
        {
            List <UserDTO> lstUserDTO= new List<UserDTO>();

            foreach (var user in userDto)
            {
                UserDTO userDTO = new UserDTO();
                userDTO.CorpID =user.CorpID;
                userDTO.Email =user.Email;
                userDTO.Phone = user.Phone;
                userDTO.FullName = user.FullName;
                userDTO.DisplayName = user.DisplayName;
                userDTO.GivenName = user.GivenName;
                userDTO.Surname = user.Surname;
                userDTO.SamaAccountName = user.SamaAccountName;
                userDTO.PhysicalDeliveryOfficeName = user.PhysicalDeliveryOfficeName;
                userDTO.EmployeeType = user.EmployeeType;
                userDTO.EmployeeId = user.EmployeeId;
                userDTO.EmployeeNumber = user.EmployeeNumber;
                userDTO.Title = user.Title;
                userDTO.Department = user.Department;
                userDTO.Division = user.Division;
                userDTO.Manager = user.Manager;
                userDTO.ManagerDisplayName = user.ManagerDisplayName;
                userDTO.ManagerEmail = user.ManagerEmail;
                userDTO.ManagerCorpID = user.ManagerCorpID;
                var thumbnail = JsonConvert.SerializeObject(user.ThumbnailPhoto);
                if(thumbnail.Length <=2)
                userDTO.ThumbnailPhoto="data:image/png;base64,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";
                else
                userDTO.ThumbnailPhoto = "data:image/png;base64," + thumbnail.Replace('"',' ').Trim();
                
                lstUserDTO.Add(userDTO);

            }
            return lstUserDTO;
        }
        return null;
    }


    public void UpdateUser(string corpuserid, UserAssignRoleDTO user)
    {
        if (ValidateUpdateUserUser(corpuserid, user))
            _userManagementRepository.UpdateUser(corpuserid, user);
    }

    public void DeleteUser(string corpuserid)
    {
        _userManagementRepository.DeleteUser(corpuserid);
    }

    public List<RolesDTO> GetAllRoles()
    {
        return _userManagementRepository.GetAllRoles().Result.ToList();
    }

    private bool ValidateUser(UserDTO user)
    {
        // var userinfo = _lDAPService.GetUserFromAD(user.CorpID, false);

        // if (userinfo != null)
        // {
        var appuser = _userManagementRepository.GetUserByCorpUserId(user.CorpID).Result;
        if (appuser == null)
        {
            return true;
        }
        //  }
        return false;
    }

    private bool ValidateUpdateUserUser(string corpuserid, UserAssignRoleDTO user)
    {
        // var userinfo = _lDAPService.GetUserFromAD(corpuserid, false);

        // if (userinfo != null)
        // {
        var appuser = _userManagementRepository.GetUserByCorpUserId(corpuserid).Result;
        if (appuser != null)
        {
            return true;
        }
        //  }
        return false;
    }

}

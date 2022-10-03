using System.Collections.Generic;
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
        _userManagementRepository.CreateUser(user);
    }

    public IEnumerable<UserDTO> GetAllUsers()
    {
        return _userManagementRepository.GetAllUsers().Result;
    }

    public void DeleteUser(string appuserid)
    {
        throw new System.NotImplementedException();
    }

    public UserDTO IsvalidUser(LogInDTO loginDTO)
    {
        if (_lDAPService.IsValidADUser(loginDTO.UserName, loginDTO.Password))
        {
            var adUserinfo = _lDAPService.GetUserFromAD(loginDTO.UserName, false);
            UserDTO userDTO = new UserDTO
            {
                CorpID = adUserinfo.CorpID,
            };
            return userDTO;
        }

        return null;
    }

    public List<UserDTO> SearchUser(string username)
    {
        throw new System.NotImplementedException();
    }

    public void UpdateUser(string appuserid, UserDTO user)
    {
        throw new System.NotImplementedException();
    }
}

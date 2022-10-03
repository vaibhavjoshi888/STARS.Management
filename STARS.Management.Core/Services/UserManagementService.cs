using System.Collections.Generic;
using STARS.Management.Core.DTO;
using STARS.Management.Core.Interface;
using STARS.Management.Core.Repository;

namespace STARS.Management.Core.Services;

public class UserManagementService : IUserManagementService
{

    private readonly IUserManagementRepository _userManagementRepository;
    
    public UserManagementService(IUserManagementRepository userManagementRepository)
    {
        _userManagementRepository = userManagementRepository;
    }

    public void DeleteUser(string appuserid)
    {
        throw new System.NotImplementedException();
    }

    public List<UserDTO> GetAllUsers()
    {
        var test = _userManagementRepository.GetAllUsers();
        return null;
    }

    public UserDTO IsvalidUser(LogInDTO loginDTO)
    {
        throw new System.NotImplementedException();
    }

    public void SaveUser(UserDTO user)
    {
        _userManagementRepository.CreateUser(user);
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

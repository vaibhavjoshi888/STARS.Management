using System.Collections.Generic;
using STARS.Management.Core.DTO;
using STARS.Management.Core.Interface;

namespace STARS.Management.Core.Services;

public class UserManagementService : IUserManagementService
{
    
    public void DeleteUser(string appuserid)
    {
        throw new System.NotImplementedException();
    }

    public List<UserDTO> GetAllUsers()
    {
        throw new System.NotImplementedException();
    }

    public UserDTO IsvalidUser(LogInDTO loginDTO)
    {
        throw new System.NotImplementedException();
    }

    public void SaveUser(UserDTO user)
    {
        throw new System.NotImplementedException();
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

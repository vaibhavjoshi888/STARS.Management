using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using STARS.Management.Core.DTO;

namespace STARS.Management.Core.Interface;

public interface IUserManagementService
{
    IEnumerable<UserDTO> GetAllUsers();
    Tuple<bool, SignedInUserDTO> IsvalidUser(LogInDTO loginDTO);
    List<UserDTO> SearchUser(string username);
    void SaveUser(UserDTO user);
    void UpdateUser(string corpuserid, UserAssignRoleDTO user);
    void DeleteUser(string corpuserid);
    List<RolesDTO> GetAllRoles();

}

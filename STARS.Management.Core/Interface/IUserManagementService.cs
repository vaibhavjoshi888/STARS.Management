using System.Collections.Generic;
using System.Threading.Tasks;
using STARS.Management.Core.DTO;

namespace STARS.Management.Core.Interface;

public interface IUserManagementService
{
    IEnumerable<UserDTO> GetAllUsers();
    UserDTO IsvalidUser(LogInDTO loginDTO);
    List<UserDTO> SearchUser(string username);
    void SaveUser(UserDTO user);
    void UpdateUser(string appuserid, UserDTO user);
    void DeleteUser(string appuserid);
    List<RolesDTO> GetAllRoles();

}

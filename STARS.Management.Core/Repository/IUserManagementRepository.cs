using System.Collections.Generic;
using System.Threading.Tasks;
using STARS.Management.Core.DTO;

namespace STARS.Management.Core.Repository;

public interface IUserManagementRepository
{
    public Task<IEnumerable<UserDTO>> GetAllUsers();
    public Task CreateUser(UserDTO userDTO);
    public Task<IEnumerable<RolesDTO>> GetAllRoles();
    public Task<UserRolesDTO> GetUserByCorpUserId(string userid);
    public Task UpdateUser(UserAssignRoleDTO userDTO);
    public  Task DeleteUser(string userid);

}


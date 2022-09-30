using STARS.Management.Core.DTO;

namespace STARS.Management.Core.Interface;

public interface IUserManagementService
{
    void GetAllUsers();
    void SearchUser(string username);
    void SaveUser(UserDTO user);
    void UpdateUser(string appuserid,UserDTO user);
    void DeleteUser(string appuserid);

}

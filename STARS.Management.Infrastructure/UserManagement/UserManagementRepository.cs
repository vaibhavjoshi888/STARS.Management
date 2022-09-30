using STARS.Management.Infrastructure.Context;
using STARS.Management.Infrastructure.Repository;

namespace STARS.Management.Infrastructure.UserManagement;

public class UserManagementRepository:IUserManagementRepository
{
    private readonly DapperContext _context;
    public UserManagementRepository(DapperContext context)
    {
        _context = context;
    }
    

}

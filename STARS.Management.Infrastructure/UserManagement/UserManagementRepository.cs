using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using STARS.Management.Core.DTO;
using STARS.Management.Core.Repository;
using STARS.Management.Infrastructure.Context;

namespace STARS.Management.Infrastructure.UserManagement;

public class UserManagementRepository : IUserManagementRepository
{
    private readonly DapperContext _context;
    public UserManagementRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<List<UserDTO>> GetAllUsers()
    {
        try
        {
            var query = SqlList._insert_app_user;
            using (var connection = _context.CreateConnection())
            {
                var roles = await connection.QueryAsync<object>(query);
                return null;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}

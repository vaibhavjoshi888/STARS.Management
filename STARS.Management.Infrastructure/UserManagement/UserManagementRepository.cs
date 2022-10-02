using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using STARS.Management.Core.DTO;
using STARS.Management.Core.Interface;
using STARS.Management.Core.Repository;
using STARS.Management.Infrastructure.Context;
using STARS.Management.Infrastructure.UserManagement.SQL;

namespace STARS.Management.Infrastructure.UserManagement;

public class UserManagementRepository : IUserManagementRepository
{
    private readonly DapperContext _context;
    private readonly IQueryProviderService _QueryProviderService;
    public UserManagementRepository(DapperContext context, IQueryProviderService QueryProviderService)
    {
        _context = context;
        _QueryProviderService = QueryProviderService;
    }

    public async Task<List<UserDTO>> GetAllUsers()
    {
        try
        {
            var query =_QueryProviderService.GetQuery(UserSqlList._insert_app_user);
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

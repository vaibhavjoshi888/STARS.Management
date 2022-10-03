using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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

    public async Task<IEnumerable<UserDTO>> GetAllUsers()
    {
        try
        {
            var query = _QueryProviderService.GetQuery(UserSqlList.GetallUsers);
            using (var connection = _context.CreateConnection())
            {
                var user = await connection.QueryAsync<UserDTO>(query);
                if (user.Any())
                    return user;
                else
                    return null;
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public async Task<IEnumerable<RolesDTO>> GetAllRoles()
    {
        try
        {
            var query = _QueryProviderService.GetQuery(UserSqlList.GetallUsers);
            using (var connection = _context.CreateConnection())
            {
                var rolesDTOs = await connection.QueryAsync<RolesDTO>(query);
                if (rolesDTOs.Any())
                    return rolesDTOs;
                else
                    return null;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public async Task CreateUser(UserDTO userDTO)
    {
        var query = _QueryProviderService.GetQuery(UserSqlList._insert_app_user);
        var parameters = new DynamicParameters();
        
        parameters.Add("CorpUserId", userDTO.CorpID, DbType.String);
        parameters.Add("Email", userDTO.Email, DbType.String);
        parameters.Add("Phone", userDTO.Phone, DbType.String);
        parameters.Add("FirstName", userDTO.FullName, DbType.String);
        parameters.Add("LastName", userDTO.Surname, DbType.String);
        parameters.Add("DisplayName", userDTO.DisplayName, DbType.String);
        parameters.Add("EmployeeType", userDTO.EmployeeType, DbType.String);
        parameters.Add("EmployeeNumber", userDTO.EmployeeNumber, DbType.String);
        parameters.Add("PhysicalDeliveryOfficeName", userDTO.PhysicalDeliveryOfficeName, DbType.String);
        parameters.Add("Department", userDTO.Department, DbType.String);
        parameters.Add("Division", userDTO.Division, DbType.String);
        parameters.Add("Title", userDTO.Title, DbType.String);
        parameters.Add("ManagerCorpUserId", userDTO.ManagerCorpID, DbType.String);
        parameters.Add("ManagerDisplayName", userDTO.ManagerDisplayName, DbType.String);
        parameters.Add("Note", "", DbType.String);
        parameters.Add("CreatedDate", DateTime.Now, DbType.String);
        parameters.Add("CreatedBy", "", DbType.String);
        parameters.Add("ActiveStatus", "IsActive", DbType.String);

        parameters.Add("AppUserId", "IsActive", DbType.String);
        parameters.Add("RoleId", "IsActive", DbType.String);


        using (var connection = _context.CreateConnection().QueryMultiple(query, parameters))
        {
            //await connection.(query, parameters);
        }
    }

}

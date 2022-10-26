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
            var parameters = new DynamicParameters();
            parameters.Add("@operation", "GetAll", DbType.String);
            using (var connection = _context.CreateConnection())
            {
                var user = await connection.QueryAsync<UserDTO>(query, parameters);
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

    public async Task<UserRolesDTO> GetUserByCorpUserId(string userid)
    {
        try
        {
            var query = _QueryProviderService.GetQuery(UserSqlList.GetUserRole);
            var parameters = new DynamicParameters();
           parameters.Add("@operation", "GetSignedUser", DbType.String);
            parameters.Add("@userId", userid, DbType.String);
            using (var connection = _context.CreateConnection())
            {
                var user = await connection.QueryAsync<UserRolesDTO>(query, parameters);
                if (user.Any())
                    return user.SingleOrDefault();
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
            var query = _QueryProviderService.GetQuery(UserSqlList.GetallRoles);
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
        try
        {
            var queryAppUser = _QueryProviderService.GetQuery(UserSqlList._insert_app_user);
            var parameters = new DynamicParameters();
            parameters.Add("operation", "Insert", DbType.String);
            parameters.Add("corpuserid", userDTO.CorpID, DbType.String);
            parameters.Add("email", userDTO.Email, DbType.String);
            parameters.Add("phone", userDTO.Phone, DbType.String);
            parameters.Add("firstname", userDTO.FullName, DbType.String);
            parameters.Add("lastname", userDTO.Surname, DbType.String);
            parameters.Add("displayname", userDTO.DisplayName, DbType.String);
            parameters.Add("employeetype", userDTO.EmployeeType, DbType.String);
            parameters.Add("employeenumber", userDTO.EmployeeNumber, DbType.String);
            parameters.Add("physicaldeliveryofficename", userDTO.PhysicalDeliveryOfficeName, DbType.String);
            parameters.Add("department", userDTO.Department, DbType.String);
            parameters.Add("division", userDTO.Division, DbType.String);
            parameters.Add("title", userDTO.Title, DbType.String);
            parameters.Add("managercorpuserid", userDTO.ManagerCorpID, DbType.String);
            parameters.Add("managerdisplayname", userDTO.ManagerDisplayName, DbType.String);
            parameters.Add("note", "", DbType.String);
            parameters.Add("createdby",userDTO.CreatedBy, DbType.String);
            parameters.Add("activestatus", "1", DbType.String);
            parameters.Add("roleid", userDTO.UserRoleId, DbType.Int32);
            parameters.Add("thumbnail", userDTO.ThumbnailPhoto, DbType.String);

            using (var connection = _context.CreateConnection())
            {
                var identity = await connection.ExecuteAsync(queryAppUser, parameters);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public async Task UpdateUser(string corpuserid,UserAssignRoleDTO userDTO)
    {
        try
        {
            var queryAppUser = _QueryProviderService.GetQuery(UserSqlList.Update_app_user);
            var parameters = new DynamicParameters();
            parameters.Add("operation", "Update", DbType.String);
            parameters.Add("corpuserid", corpuserid, DbType.String);
            parameters.Add("roleid", userDTO.UserRoleId, DbType.Int32);

            using (var connection = _context.CreateConnection())
            {
                var identity = await connection.ExecuteAsync(queryAppUser, parameters);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    
     public async Task DeleteUser(string userid)
    {
        try
        {
            var queryAppUser = _QueryProviderService.GetQuery(UserSqlList.delete_app_user);
            var parameters = new DynamicParameters();
            parameters.Add("operation", "Delete", DbType.String);
            parameters.Add("corpuserid",userid, DbType.String);
            using (var connection = _context.CreateConnection())
            {
                var identity = await connection.ExecuteAsync(queryAppUser, parameters);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}

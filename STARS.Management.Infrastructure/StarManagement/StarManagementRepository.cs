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

namespace STARS.Management.Infrastructure.StarManagement;

public class StarManagementRepository : IStarManagementRepository
{
    private readonly DapperContext _context;
    private readonly IQueryProviderService _QueryProviderService;
    public StarManagementRepository(DapperContext context, IQueryProviderService QueryProviderService)
    {
        _context = context;
        _QueryProviderService = QueryProviderService;
    }

    public async Task CreateUser(UserDTO userDTO)
    {
        try
        {
            var queryAppUser = _QueryProviderService.GetQuery(UserSqlList._insert_app_user);
            var parameters = new DynamicParameters();
            parameters.Add("corpuserid", userDTO.CorpID, DbType.String);
            parameters.Add("message", userDTO.Email, DbType.String);
            parameters.Add("status", "P", DbType.String);
            parameters.Add("employeename", userDTO.EmployeeNumber, DbType.String);
            parameters.Add("note", "", DbType.String);
            parameters.Add("createdby", userDTO.CreatedBy, DbType.String);
            parameters.Add("isactive", "1", DbType.String);
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


}

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
using STARS.Management.Infrastructure.StarManagement.SQL;
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

    public async Task SubmitStarRequest(UserStarConfigurationDTO userStarConfigurationDTO)
    {
        try
        {
            var queryAppUser = _QueryProviderService.GetQuery(StarSqlList.Insert_Submit_Star_Config);
            var parameters = new DynamicParameters();
            parameters.Add("corpuserid", userStarConfigurationDTO.CorpUserId, DbType.String);
            parameters.Add("message", userStarConfigurationDTO.Message, DbType.String);
            parameters.Add("status", "P", DbType.String);
            parameters.Add("employeename", userStarConfigurationDTO.EmployeeName, DbType.String);
            parameters.Add("createdby", userStarConfigurationDTO.CreatedBy, DbType.String);
            parameters.Add("isactive", "1", DbType.String);
            parameters.Add("thumbnail", userStarConfigurationDTO.ThumbnailPhoto, DbType.String);

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

    public async Task<IEnumerable<UserStarConfigurationDTO>> GetAllStarRequest()
    {
        try
        {
            var query = _QueryProviderService.GetQuery(StarSqlList.Get_all_user_star_config_request);
            using (var connection = _context.CreateConnection())
            {
                var userStarConfigDTOs = await connection.QueryAsync<UserStarConfigurationDTO>(query);
                if (userStarConfigDTOs.Any())
                    return userStarConfigDTOs;
                else
                    return null;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public async Task UpdateStarRequest(string userstarid, UpdateStarRequestDTO UpdateStarRequestDTO)
    {
        try
        {
            
            var queryAppUser = _QueryProviderService.GetQuery(StarSqlList.Updatel_star_request);
            var parameters = new DynamicParameters();
            parameters.Add("message", UpdateStarRequestDTO.Message, DbType.String);
            parameters.Add("status", UpdateStarRequestDTO.Status, DbType.String);
            parameters.Add("approvedby", UpdateStarRequestDTO.Approvedby, DbType.String);
            parameters.Add("feedback", UpdateStarRequestDTO.Feedback, DbType.String);
            parameters.Add("modifiedby", UpdateStarRequestDTO.ModifiedBy, DbType.String);
            parameters.Add("userstarid", userstarid, DbType.String);
             parameters.Add("userstarid", UpdateStarRequestDTO.CorpUserId, DbType.String);
       
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

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
            parameters.Add("starfirstname", userStarConfigurationDTO.DisplayName, DbType.String);
            parameters.Add("starlastname", userStarConfigurationDTO.Surname, DbType.String);
            parameters.Add("createdby", userStarConfigurationDTO.CreatedBy, DbType.String);
            parameters.Add("isactive", "1", DbType.String);

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
            parameters.Add("userstarid", userstarid, DbType.Int32);
            parameters.Add("corpuserid", UpdateStarRequestDTO.CorpUserId, DbType.String);

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
    public async Task<StarRequestCountDTO> GetStarRequestCount()
    {
        try
        {
            var query = _QueryProviderService.GetQuery(StarSqlList.Get_star_request_count);
            using (var connection = _context.CreateConnection())
            {
                var starRequestCountDTO = await connection.QueryAsync<StarRequestCountDTO>(query);
                if (starRequestCountDTO.Any())
                    return starRequestCountDTO.SingleOrDefault();
                else
                    return null;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public async Task<IEnumerable<StarsDTO>> GetAllActiveStar()
    {
        try
        {
            var query = _QueryProviderService.GetQuery(StarSqlList.Get_all_active_star);
            using (var connection = _context.CreateConnection())
            {
                var starDTO = await connection.QueryAsync<StarsDTO>(query);
                if (starDTO.Any())
                    return starDTO;
                else
                    return null;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
   
    public async Task UpdateStarLikeCount(string userstarid)
    {
        try
        {

            var queryAppUser = _QueryProviderService.GetQuery(StarSqlList.Updatel_star_like);
            var parameters = new DynamicParameters();
            parameters.Add("userstarid", userstarid, DbType.Int32);

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
   
    public async Task UpdateStarShareCountCount(string userstarid)
    {
        try
        {

            var queryAppUser = _QueryProviderService.GetQuery(StarSqlList.Updatel_star_share);
            var parameters = new DynamicParameters();
            parameters.Add("userstarid", userstarid, DbType.Int32);

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

    public async Task<IEnumerable<RecentStarsDTO>> GetRecentStar()
    {
         try
        {
            var query = _QueryProviderService.GetQuery(StarSqlList.Get_recent_star);
            using (var connection = _context.CreateConnection())
            {
                var starDTO = await connection.QueryAsync<RecentStarsDTO>(query);
                if (starDTO.Any())
                    return starDTO;
                else
                    return null;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

}

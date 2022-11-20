using System.Collections.Generic;
using System.Threading.Tasks;
using STARS.Management.Core.DTO;

namespace STARS.Management.Core.Repository;

public interface IStarManagementRepository
{
    public  Task SubmitStarRequest(UserStarConfigurationDTO userStarConfigurationDTO);
    public  Task<IEnumerable<UserStarConfigurationDTO>> GetAllStarRequest();
    public  Task UpdateStarRequest(string userstarid, UpdateStarRequestDTO UpdateStarRequestDTO);
    public  Task<StarRequestCountDTO> GetStarRequestCount();
    public  Task<IEnumerable<StarsDTO>> GetAllActiveStar();
    public  Task UpdateStarShareCountCount(string userstarid);
    public  Task UpdateStarLikeCount(string userstarid);

}


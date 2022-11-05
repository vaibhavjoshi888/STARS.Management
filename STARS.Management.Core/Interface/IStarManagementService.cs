using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using STARS.Management.Core.DTO;

namespace STARS.Management.Core.Interface;

public interface IStarManagementService
{
    public void SubmitStarRequest(UserStarConfigurationDTO userStarConfigurationDTO);
    public  IEnumerable<UserStarConfigurationDTO> GetAllStarRequest();
    public  void UpdateStarRequest(string userstarid, UpdateStarRequestDTO UpdateStarRequestDTO);
    public  StarRequestCountDTO GetStarRequestCount();
    public IEnumerable<StarsDTO> GetAllActiveStar();
}

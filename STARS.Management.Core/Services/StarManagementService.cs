using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using STARS.Management.Core.DTO;
using STARS.Management.Core.Interface;
using STARS.Management.Core.Repository;

namespace STARS.Management.Core.Services;

public class StarManagementService : IStarManagementService
{

    private readonly IStarManagementRepository _starManagementRepository;
    private readonly ILDAPService _lDAPService;

    public StarManagementService(IStarManagementRepository starManagementRepository, ILDAPService lDAPService)
    {
        _starManagementRepository = starManagementRepository;
        _lDAPService = lDAPService;
    }

    public IEnumerable<UserStarConfigurationDTO> GetAllStarRequest()
    {
        throw new NotImplementedException();
    }

    public void SubmitStarRequest(UserStarConfigurationDTO userStarConfigurationDTO)
    {
        _starManagementRepository.SubmitStarRequest(userStarConfigurationDTO).GetAwaiter().GetResult();
    }

    public void UpdateStarRequest(string userstarid, UpdateStarRequestDTO UpdateStarRequestDTO)
    {
        throw new NotImplementedException();
    }
}

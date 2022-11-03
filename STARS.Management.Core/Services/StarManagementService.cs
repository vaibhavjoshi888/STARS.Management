using System;
using System.Collections.Generic;
using System.Linq;
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
}

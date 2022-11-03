
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using STARS.Management.Core.DTO;
using STARS.Management.Core.Interface;

namespace STARS.Management.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StarManagementController : ControllerBase
    {
        private readonly ILogger<StarManagementController> _logger;
        private readonly IStarManagementService _StarManagementService;
        public StarManagementController(ILogger<StarManagementController> logger, IStarManagementService starManagementService)
        {
            _logger = logger;
            _StarManagementService = starManagementService;
        }

    }
}

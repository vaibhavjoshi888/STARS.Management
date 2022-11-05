
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

        [HttpPost("submitstarrequest")]
        public ActionResult SubmitStarRequest(UserStarConfigurationDTO userStarConfigurationDTO)
        {
            _StarManagementService.SubmitStarRequest(userStarConfigurationDTO);
            return Ok();

        }

        [HttpPut("updatestarrequest/{userstarid}")]
        public ActionResult UpdateStarRequest(string userstarid, UpdateStarRequestDTO user)
        {
            return Ok();
        }

        [HttpGet("getstarreaquest")]
        public ActionResult GetStarReaquest()
        {

            return Ok();

        }

        [HttpGet("getallactivestars")]
        public ActionResult GetAllActiveStars()
        {

            return Ok();

        }

        [HttpGet("getrecentstars")]
        public ActionResult GetRecentStars()
        {
            return Ok();

        }

        [HttpGet("getStarreaquestcount")]
        public ActionResult GetStarReaquestCount()
        {
            return Ok();

        }
    }
}

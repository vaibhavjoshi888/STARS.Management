using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using STARS.Management.Core.Interface;

namespace STARS.Management.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserManagementController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly ILDAPService _lDAPService;

        public UserManagementController(ILogger<WeatherForecastController> logger, ILDAPService lDAPService)
        {
            _logger = logger;
            _lDAPService = lDAPService;
        }

        [HttpGet(Name = "GetUserDetails/{username}")]
        public ActionResult Get([FromRoute] string username)
        {
            _lDAPService.GetUserFromAD(username,false);

            return Ok();

        }
    }
}

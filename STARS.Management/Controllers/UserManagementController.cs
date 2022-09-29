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
        private readonly ILogger<UserManagementController> _logger;
        private readonly ILDAPService _lDAPService;
        public UserManagementController(ILogger<UserManagementController> logger, ILDAPService lDAPService)
        {
            _logger = logger;
            _lDAPService = lDAPService;
        }

        [HttpGet(Name = "getuserdetails/{username}")]
        public ActionResult Get(string username)
        {
            _lDAPService.GetUserFromAD(username,false);

            return Ok();

        }
    }
}

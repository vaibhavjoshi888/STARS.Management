using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using STARS.Management.Core.DTO;
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

        [HttpGet("getuserdetails/{username}")]
        public ActionResult Get(string username)
        {
           _lDAPService.GetUserFromAD(username,false);
             return Ok();

        }

        [HttpGet("getallusers")]
        public ActionResult IsvalidUser()
        {
          // _lDAPService.GetUserFromAD(username,false);
             return Ok();

        }

        [HttpGet("searchuser/{username}")]
        public ActionResult SearchUser(string username)
        {
           //_lDAPService.GetUserFromAD(username,false);
             return Ok();

        }

        [HttpPost("user")]
        public ActionResult SaveUser(UserDTO user)
        {
          // _lDAPService.GetUserFromAD(username,false);
             return Ok();

        }

        [HttpPost("user/{appuserid}")]
        public ActionResult UpdateUser(string appuserid,UserDTO user)
        {
          // _lDAPService.GetUserFromAD(username,false);
             return Ok();

        }

        [HttpDelete("deleteuser/{appuserid}")]
        public ActionResult Delete(string appuserid)
        {
           _lDAPService.GetUserFromAD(appuserid,false);
             return Ok();

        }

    }
}

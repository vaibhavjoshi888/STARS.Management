
using System.Linq;
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
        private readonly IUserManagementService _UserManagementService;
        public UserManagementController(ILogger<UserManagementController> logger, IUserManagementService userManagementService)
        {
            _logger = logger;
            _UserManagementService = userManagementService;
        }

        [HttpGet("getuserdetails/{username}")]
        public ActionResult Get(string username)
        {
            var users = _UserManagementService.GetAllUsers();
            if (users.Any())
                return Ok(users);
            else return NotFound("Record Not Found");

        }

        [HttpPost("isvaliduser")]
        public ActionResult IsvalidUser(LogInDTO loginDTO)
        {

            var userDto = _UserManagementService.IsvalidUser(loginDTO);
            if (userDto != null)
                return Ok(userDto);
            return NotFound();
        }

        [HttpPost("user")]
        public ActionResult SaveUser(UserDTO user)
        {
            _UserManagementService.SaveUser(user);
            return Ok();

        }

        [HttpGet("searchuser/{searchtext}")]
        public ActionResult SearchUser(string searchtext)
        {
            //_lDAPService.GetUserFromAD(username,false);
            return Ok();

        }

        [HttpPut("user/{appuserid}")]
        public ActionResult UpdateUser(string appuserid, UserAssignRoleDTO user)
        {
            _UserManagementService.UpdateUser(appuserid, UserAssignRoleDTO);
            return Ok();

        }

        [HttpDelete("deleteuser/{appuserid}")]
        public ActionResult Delete(string appuserid)
        {
            _UserManagementService.DeleteUser(appuserid);
            return Ok();

        }


    }
}

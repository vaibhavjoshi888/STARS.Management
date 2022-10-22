
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

        [HttpGet("getuserdetails")]
        public ActionResult Get()
        {
            var users = _UserManagementService.GetAllUsers();
            if (users != null && users.Count() != 0)
                return Ok(users);
            else return NotFound("Record Not Found");

        }

        [HttpGet("getuserdetails/{username}")]
        public ActionResult GetByUsername(string username)
        {
            var users = _UserManagementService.GetAllUsers();
            if (users != null)
                return Ok(users);

            return NotFound("Record Not Found");

        }

        [HttpPost("isvaliduser")]
        public ActionResult IsvalidUser(LogInDTO loginDTO)
        {

            var userDto = _UserManagementService.IsvalidUser(loginDTO);
            if (userDto != null)
                return Ok(userDto);
            return BadRequest("Error with credential or user not found");
        }

        [HttpPost("user")]
        public ActionResult SaveUser(UserDTO userDTO)
        {
            _UserManagementService.SaveUser(userDTO);
            return Ok();

        }

        [HttpGet("searchuser/{searchtext}")]
        public ActionResult SearchUser(string searchtext)
        {
            var users = _UserManagementService.SearchUser(searchtext);
            if (users != null)
                return Ok(users);
            return NotFound("Record Not Found");
        }

        [HttpPut("user/{corpuserid}")]
        public ActionResult UpdateUser(string corpuserid, UserAssignRoleDTO user)
        {
            _UserManagementService.UpdateUser(corpuserid, user);
            return Ok();
        }

        [HttpDelete("deleteuser/{corpuserid}")]
        public ActionResult Delete(string corpuserid)
        {
            _UserManagementService.DeleteUser(corpuserid);
            return Ok();
        }
    }
}

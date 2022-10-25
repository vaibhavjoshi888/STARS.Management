
using System.Collections.Generic;
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
            if (users != null)
                return Ok(users);
            return NotFound("Record Not Found");

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
            SignedInUserDTO sss = new SignedInUserDTO();
            sss.AppUserId = 0;
            sss.CorpUserId = "Shrinith";
            sss.Email = "shrinith.sanil@gmail.com";
            sss.DisplayName = "Shrinith S";
            sss.FirstName = "Shrinith";
            sss.LastName = "Sanil";
            sss.RoleId = 1;
            sss.RoleName = "Software Developer";
            sss.Initial = "SSS";
            sss.ThumbnailPhoto = null;
            sss.HasThumbnailPhoto = false;
            sss.CanViewDailySchedule = false;

            return Ok(sss); ;

            //var userDto = _UserManagementService.IsvalidUser(loginDTO);
            //if (userDto != null)
            //    return Ok(userDto);
            //return BadRequest("Error with credential or user not found");
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
            //var users = _UserManagementService.SearchUser(searchtext);
            //if (users != null)
            //    return Ok(users);
            //return NotFound("Record Not Found");
            var userDTOs = GetSampleData();
            return Ok(userDTOs);
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

        private List<UserDTO> GetSampleData()
        {

            UserDTO user = new UserDTO();
            user.CorpID = "ABC123";
            user.Email = "shrinith.s.sanil@gmail.com";
            user.Phone = "11111111";
            user.FullName = "Shrinith Sanil";
            user.DisplayName = "Shrinith S";
            user.GivenName = "Shrinith";
            user.Surname = "Sanil";
            user.SamaAccountName = "111111";
            user.PhysicalDeliveryOfficeName = "rrrrrr";
            user.EmployeeType = "Permanent";
            user.EmployeeId = "111";
            user.EmployeeNumber = "1112";
            user.Title = "Mr";
            user.Department = "Dept";
            user.Division = "Div";
            user.Manager = "Vaibhav";
            user.ManagerDisplayName = "Vaibhav";
            user.ManagerEmail = "vaibhav888@gmail.com";
            user.ManagerCorpID = "1324";
            user.ThumbnailPhoto = null;
            user.UserRoleId = 1;

            UserDTO user2 = new UserDTO();
            user2.CorpID = "ABC444";
            user2.Email = "vailbhav.joshi@gmail.com";
            user2.Phone = "11111111";
            user2.FullName = "Vaibhav Joshi";
            user2.DisplayName = "Vaibhav J";
            user2.GivenName = "Vaibhav";
            user2.Surname = "Joshi";
            user2.SamaAccountName = "22222";
            user2.PhysicalDeliveryOfficeName = "vvvvvv";
            user2.EmployeeType = "Permanent";
            user2.EmployeeId = "222";
            user2.EmployeeNumber = "1113";
            user2.Title = "Mr";
            user2.Department = "Dept";
            user2.Division = "Div";
            user2.Manager = "Srikant";
            user2.ManagerDisplayName = "Srikant";
            user2.ManagerEmail = "srikant888@gmail.com";
            user2.ManagerCorpID = "9999";
            user2.ThumbnailPhoto = null;
            user2.UserRoleId = 1;



            UserDTO user3 = new UserDTO();
            user3.CorpID = "ABC444";
            user3.Email = "vailbhav.joshi@gmail.com";
            user3.Phone = "11111111";
            user3.FullName = "Vaibhav Joshi";
            user3.DisplayName = "Vaibhav J";
            user3.GivenName = "Vaibhav";
            user3.Surname = "Joshi";
            user3.SamaAccountName = "22222";
            user3.PhysicalDeliveryOfficeName = "vvvvvv";
            user3.EmployeeType = "Permanent";
            user3.EmployeeId = "222";
            user3.EmployeeNumber = "1113";
            user3.Title = "Mr";
            user3.Department = "Dept";
            user3.Division = "Div";
            user3.Manager = "Srikant";
            user3.ManagerDisplayName = "Srikant";
            user3.ManagerEmail = "srikant888@gmail.com";
            user3.ManagerCorpID = "9999";
            user3.ThumbnailPhoto = null;
            user3.UserRoleId = 1;


            UserDTO user4 = new UserDTO();
            user4.CorpID = "ABC444";
            user4.Email = "vailbhav.joshi@gmail.com";
            user4.Phone = "11111111";
            user4.FullName = "Vaibhav Joshi";
            user4.DisplayName = "Vaibhav J";
            user4.GivenName = "Vaibhav";
            user4.Surname = "Joshi";
            user4.SamaAccountName = "22222";
            user4.PhysicalDeliveryOfficeName = "vvvvvv";
            user4.EmployeeType = "Permanent";
            user4.EmployeeId = "222";
            user4.EmployeeNumber = "1113";
            user4.Title = "Mr";
            user4.Department = "Dept";
            user4.Division = "Div";
            user4.Manager = "Srikant";
            user4.ManagerDisplayName = "Srikant";
            user4.ManagerEmail = "srikant888@gmail.com";
            user4.ManagerCorpID = "9999";
            user4.ThumbnailPhoto = null;
            user4.UserRoleId = 1;



            UserDTO user5 = new UserDTO();
            user5.CorpID = "ABC444";
            user5.Email = "vailbhav.joshi@gmail.com";
            user5.Phone = "11111111";
            user5.FullName = "Test1 tttt Joshi";
            user5.DisplayName = "Vaibhav J";
            user5.GivenName = "Vaibhav";
            user5.Surname = "Joshi";
            user5.SamaAccountName = "22222";
            user5.PhysicalDeliveryOfficeName = "vvvvvv";
            user5.EmployeeType = "Permanent";
            user5.EmployeeId = "222";
            user5.EmployeeNumber = "1113";
            user5.Title = "Mr";
            user5.Department = "Dept";
            user5.Division = "Div";
            user5.Manager = "Srikant";
            user5.ManagerDisplayName = "Srikant";
            user5.ManagerEmail = "srikant888@gmail.com";
            user5.ManagerCorpID = "9999";
            user5.ThumbnailPhoto = null;
            user5.UserRoleId = 1;



            UserDTO user6 = new UserDTO();
            user6.CorpID = "ABC444";
            user6.Email = "vailbhav.joshi@gmail.com";
            user6.Phone = "11111111";
            user6.FullName = "Test Joshi";
            user6.DisplayName = "Vaibhav J";
            user6.GivenName = "Vaibhav";
            user6.Surname = "Joshi";
            user6.SamaAccountName = "22222";
            user6.PhysicalDeliveryOfficeName = "vvvvvv";
            user6.EmployeeType = "Permanent";
            user6.EmployeeId = "222";
            user6.EmployeeNumber = "1113";
            user6.Title = "Mr";
            user6.Department = "Dept";
            user6.Division = "Div";
            user6.Manager = "Srikant";
            user6.ManagerDisplayName = "Srikant";
            user6.ManagerEmail = "srikant888@gmail.com";
            user6.ManagerCorpID = "9999";
            user6.ThumbnailPhoto = null;
            user6.UserRoleId = 1;


            List<UserDTO> userDTOs = new List<UserDTO>();
            userDTOs.Add(user);
            userDTOs.Add(user2);
            userDTOs.Add(user3);
            userDTOs.Add(user4);
            userDTOs.Add(user5);
            userDTOs.Add(user6);

            return userDTOs;
        }
    }
}

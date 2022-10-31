
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
            user.ThumbnailPhoto = "data:image/png;base64,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";
            user.UserRoleId = 0;

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
            user2.ThumbnailPhoto = "data:image/png;base64,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";
            user2.UserRoleId = 0;




            UserDTO user3 = new UserDTO();
            user3.CorpID = "ABC111";
            user3.Email = "user.1@gmail.com";
            user3.Phone = "11111111";
            user3.FullName = "Username1 Joshi";
            user3.DisplayName = "Username1 j";
            user3.GivenName = "Username1";
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
            user3.ThumbnailPhoto = "data:image/png;base64,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";
            user3.UserRoleId = 0;


            UserDTO user4 = new UserDTO();
            user4.CorpID = "ABC222";
            user4.Email = "Username2.joshi@gmail.com";
            user4.Phone = "11111111";
            user4.FullName = "Username2 Joshi";
            user4.DisplayName = "Username2 J";
            user4.GivenName = "Username2";
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
            user4.ThumbnailPhoto = "data:image/png;base64,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";
            user4.UserRoleId = 0;



            UserDTO user5 = new UserDTO();
            user5.CorpID = "ABC333";
            user5.Email = "Username3.joshi@gmail.com";
            user5.Phone = "11111111";
            user5.FullName = "Username3 Joshi";
            user5.DisplayName = "Username3 J";
            user5.GivenName = "Username3";
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
            user5.ThumbnailPhoto = "data:image/png;base64,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";
            user5.UserRoleId = 0;



            UserDTO user6 = new UserDTO();
            user6.CorpID = "ABC555";
            user6.Email = "Username4.joshi@gmail.com";
            user6.Phone = "11111111";
            user6.FullName = "Username4 Joshi";
            user6.DisplayName = "Username4 J";
            user6.GivenName = "Username4";
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
            user6.ThumbnailPhoto = "data:image/png;base64,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";
            user6.UserRoleId = 0;


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

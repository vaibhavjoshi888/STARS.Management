
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
            // samle obj
 //{"listJson":"[{\"CorpID\":\"yerunkaa\",\"Email\":\"Ashish.Yerunkar@nychhc.org\",\"Phone\":\"646-694-6176\",\"FullName\":\"Yerunkar, Ashish\",\"DisplayName\":\"Yerunkar, Ashish\",\"GivenName\":\"Ashish\",\"Surname\":\"Yerunkar\",\"SamaAccountName\":\"yerunkaa\",\"PhysicalDeliveryOfficeName\":\"CO 55 WATER  24 Fl\",\"EmployeeType\":\"Consultant\",\"EmployeeId\":\"ay1007\",\"EmployeeNumber\":\"100032025\",\"Title\":\"Quality Assurance Coordinator\",\"Department\":\"DATA SCIENCES AND SUPPORT\",\"Division\":\"Central\",\"Manager\":\"CN=VEGODAA,OU=Central,OU=Users,OU=Managed Objects,DC=corp,DC=nychhc,DC=org\",\"ManagerDisplayName\":null,\"ManagerEmail\":null,\"ManagerCorpID\":null,\"ThumbnailPhoto\":\"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\"},{\"CorpID\":\"A-yerunkaa\",\"Email\":\"\",\"Phone\":\"\",\"FullName\":\"Yerunkar, Ashish (A-YERUNKAA)\",\"DisplayName\":\"Yerunkar, Ashish (A-YERUNKAA)\",\"GivenName\":\"Ashish\",\"Surname\":\"Yerunkar (A-YERUNKAA)\",\"SamaAccountName\":\"A-yerunkaa\",\"PhysicalDeliveryOfficeName\":\"\",\"EmployeeType\":\"\",\"EmployeeId\":\"\",\"EmployeeNumber\":\"\",\"Title\":\"\",\"Department\":\"\",\"Division\":\"\",\"Manager\":\"\",\"ManagerDisplayName\":null,\"ManagerEmail\":null,\"ManagerCorpID\":null,\"ThumbnailPhoto\":\"\"},{\"CorpID\":\"chakraba3\",\"Email\":\"\",\"Phone\":\"\",\"FullName\":\"Chakraborty, Ashish\",\"DisplayName\":\"Chakraborty, Ashish\",\"GivenName\":\"Ashish\",\"Surname\":\"Chakraborty\",\"SamaAccountName\":\"chakraba3\",\"PhysicalDeliveryOfficeName\":\"BELLEVUE HB BUILDING 16 FL\",\"EmployeeType\":\"Student\",\"EmployeeId\":\"ac0310\",\"EmployeeNumber\":\"100089630\",\"Title\":\"Medical Student\",\"Department\":\"IP MEDICINE\",\"Division\":\"SMHN\",\"Manager\":\"CN=Westbrook\\\\, Georgia,OU=SMHN,OU=Users,OU=Managed Objects,DC=corp,DC=nychhc,DC=org\",\"ManagerDisplayName\":null,\"ManagerEmail\":null,\"ManagerCorpID\":null,\"ThumbnailPhoto\":\"\"},{\"CorpID\":\"kadinjia\",\"Email\":\"kadinjia@nychhc.org\",\"Phone\":\"\",\"FullName\":\"Kadinjicandy, Ashish\",\"DisplayName\":\"Kadinjicandy, Ashish\",\"GivenName\":\"Ashish\",\"Surname\":\"Kadinjicandy\",\"SamaAccountName\":\"kadinjia\",\"PhysicalDeliveryOfficeName\":\"CONEY TOWER BUILDING 7 FL\",\"EmployeeType\":\"Employee\",\"EmployeeId\":\"ak0325\",\"EmployeeNumber\":\"100150781\",\"Title\":\"Staff Nurse - Critical Care\",\"Department\":\"CON01 T7W MED/SURG ICU\",\"Division\":\"SBSI\",\"Manager\":\"CN=Maantonia Mata-Castignani,OU=SBSI,OU=Users,OU=Managed Objects,DC=corp,DC=nychhc,DC=org\",\"ManagerDisplayName\":null,\"ManagerEmail\":null,\"ManagerCorpID\":null,\"ThumbnailPhoto\":\"\"},{\"CorpID\":\"A-patela77\",\"Email\":\"\",\"Phone\":\"\",\"FullName\":\"Patel, Ashish (A-patela77)\",\"DisplayName\":\"Patel, Ashish (A-patela77)\",\"GivenName\":\"Ashish\",\"Surname\":\"Patel\",\"SamaAccountName\":\"A-patela77\",\"PhysicalDeliveryOfficeName\":\"\",\"EmployeeType\":\"\",\"EmployeeId\":\"\",\"EmployeeNumber\":\"\",\"Title\":\"\",\"Department\":\"\",\"Division\":\"\",\"Manager\":\"\",\"ManagerDisplayName\":null,\"ManagerEmail\":null,\"ManagerCorpID\":null,\"ThumbnailPhoto\":\"\"},{\"CorpID\":\"ghewarea\",\"Email\":\"ghewarea@nychhc.org\",\"Phone\":\"\",\"FullName\":\"Gheware, Ashish\",\"DisplayName\":\"Gheware, Ashish\",\"GivenName\":\"Ashish\",\"Surname\":\"Gheware\",\"SamaAccountName\":\"ghewarea\",\"PhysicalDeliveryOfficeName\":\"50 Water Street 3rd Floor\",\"EmployeeType\":\"Remote\",\"EmployeeId\":\"ag0516\",\"EmployeeNumber\":\"100167954\",\"Title\":\"Auditor\",\"Department\":\"REVENUE CYCLE ADMIN\",\"Division\":\"Central\",\"Manager\":\"CN=MELICANR,OU=Central,OU=Users,OU=Managed Objects,DC=corp,DC=nychhc,DC=org\",\"ManagerDisplayName\":null,\"ManagerEmail\":null,\"ManagerCorpID\":null,\"ThumbnailPhoto\":\"\"},{\"CorpID\":\"sharmaa7\",\"Email\":\"sharmaa7@nychhc.org\",\"Phone\":\"\",\"FullName\":\"Sharma, Ashish\",\"DisplayName\":\"Sharma, Ashish\",\"GivenName\":\"Ashish\",\"Surname\":\"Sharma\",\"SamaAccountName\":\"sharmaa7\",\"PhysicalDeliveryOfficeName\":\"ELMHURST MAIN BUILDING 1 FL\",\"EmployeeType\":\"Employee\",\"EmployeeId\":\"as0823\",\"EmployeeNumber\":\"100062110\",\"Title\":\"Staff Nurse - Emergency Svcs\",\"Department\":\"EMERGENCY SVCS ADULT\",\"Division\":\"QHN\",\"Manager\":\"CN=Ferdinand Fernandez,OU=QHN,OU=Users,OU=Managed Objects,DC=corp,DC=nychhc,DC=org\",\"ManagerDisplayName\":null,\"ManagerEmail\":null,\"ManagerCorpID\":null,\"ThumbnailPhoto\":\"\"}]","resMsgType":"SUCCESS","resMsg":""}
    //         string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
    // Image1.ImageUrl = "data:image/png;base64," + base64String;

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
            user3.ThumbnailPhoto = null;
            user3.UserRoleId = 1;


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
            user4.ThumbnailPhoto = null;
            user4.UserRoleId = 1;



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
            user5.ThumbnailPhoto = null;
            user5.UserRoleId = 1;



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

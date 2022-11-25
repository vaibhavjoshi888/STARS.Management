using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using STARS.Management.Core.DTO;
using STARS.Management.Core.Interface;
using STARS.Management.Core.Models;
using STARS.Management.Core.Repository;

namespace STARS.Management.Core.Services;

public class StarManagementService : IStarManagementService
{

    private readonly IStarManagementRepository _starManagementRepository;
    private readonly ILDAPService _lDAPService;
    private readonly IEmailService _emailService;
    private readonly IUserManagementRepository _userManagementRepository;
    public StarManagementService(IStarManagementRepository starManagementRepository, ILDAPService lDAPService,
    IEmailService emailService,IUserManagementRepository userManagementRepository)
    {
        _starManagementRepository = starManagementRepository;
        _lDAPService = lDAPService;
        _emailService = emailService;
        _userManagementRepository=userManagementRepository;
    }

    public IEnumerable<StarsDTO> GetAllActiveStar()
    {
        List<StarsDTO> lstStarDto = new List<StarsDTO>();
        var test = _starManagementRepository.GetAllActiveStar().Result;
        // if (test !=null)
        // {
        // if (test.Any())
        // {
        //     foreach (var item in test)
        //     {
        //         var thumbnail = JsonConvert.SerializeObject(_lDAPService.GetUserFromAD(item.CorpUserId, false).ThumbnailPhoto);
        //         if (thumbnail.Length <= 2)
        //             item.Thumbnail = "data:image/png;base64,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";
        //         else
        //             item.Thumbnail = "data:image/png;base64," + thumbnail.Replace('"', ' ').Trim();
        //         lstStarDto.Add(item);
        //     }
        // }
        // return lstStarDto;
        // }

        return test;
    }

    public IEnumerable<UserStarConfigurationDTO> GetAllStarRequest()
    {
        return _starManagementRepository.GetAllStarRequest().Result;
    }

    public IEnumerable<RecentStarsDTO> GetRecentStar()
    {

        List<RecentStarsDTO> lstStarDto = new List<RecentStarsDTO>();
        var test = _starManagementRepository.GetRecentStar().Result;
        // if (test != null)
        // {
        //     if (test.Any())
        //     {
        //         foreach (var item in test)
        //         {
        //             var thumbnail = JsonConvert.SerializeObject(_lDAPService.GetUserFromAD(item.CorpUserId, false).ThumbnailPhoto);
        //             if (thumbnail.Length <= 2)
        //                 item.Thumbnail = "data:image/png;base64,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";
        //             else
        //                 item.Thumbnail = "data:image/png;base64," + thumbnail.Replace('"', ' ').Trim();
        //             lstStarDto.Add(item);
        //         }
        //     }
        //     return lstStarDto;
        // }
        return test;
    }

    public StarRequestCountDTO GetStarRequestCount()
    {
        return _starManagementRepository.GetStarRequestCount().Result;
    }

    public void SubmitStarRequest(UserStarConfigurationDTO userStarConfigurationDTO)
    {
        _starManagementRepository.SubmitStarRequest(userStarConfigurationDTO).GetAwaiter().GetResult();
        SendEmail(userStarConfigurationDTO);
    }

    public void UpdateStarLikeCount(string userstarid)
    {
        _starManagementRepository.UpdateStarLikeCount(userstarid);
    }

    public void UpdateStarRequest(string userstarid, UpdateStarRequestDTO UpdateStarRequestDTO)
    {
        _starManagementRepository.UpdateStarRequest(userstarid, UpdateStarRequestDTO);
        SendEmail(UpdateStarRequestDTO, userstarid);
    }

    public void UpdateStarShareCount(string userstarid)
    {
        _starManagementRepository.UpdateStarShareCountCount(userstarid);
    }

    private void SendEmail(UpdateStarRequestDTO userStarConfigurationDTO, string userstarid)
    {

        var userStarConfig = GetAllStarRequest().Where(x => x.UserStarId == Convert.ToInt32(userstarid)).FirstOrDefault();
        var adSubmitUser = _lDAPService.GetUserFromAD(userStarConfig.CreatedBy, false);

        if (userStarConfigurationDTO.Status == "A")
        {
            var adUser = _lDAPService.GetUserFromAD(userStarConfig.CreatedBy, false);
            EmailDTO emailDTO = new EmailDTO();
            emailDTO.CorpID = userStarConfigurationDTO.CorpUserId;
            emailDTO.CreatedBy = userStarConfig.CreatedBy;
            emailDTO.CreatedDate = DateTime.Parse(userStarConfig.CreatedDate);
            emailDTO.Email = adUser.Email;
            emailDTO.FullName = adUser.FullName;
            emailDTO.Manager = adUser.Manager;
            emailDTO.ManagerEmail = adUser.ManagerEmail;
            emailDTO.Phone = adUser.Phone;
            emailDTO.PlaceholderMessage = userStarConfigurationDTO.Message;
            emailDTO.CorpID = userStarConfigurationDTO.CorpUserId;
            
            emailDTO.PlaceholderButtonText = "Click Here to view STAR >";
            emailDTO.PlaceholderCongrats = "Congratulations, a new STAR has been submitted for you!";
            emailDTO.PlaceholderView = "To view this STAR,";

            EmailDTO emailSubmitDTO = new EmailDTO();
            emailSubmitDTO.CorpID = userStarConfigurationDTO.CorpUserId;
            emailSubmitDTO.CreatedBy = userStarConfig.CreatedBy;
            emailSubmitDTO.CreatedDate = DateTime.Parse(userStarConfig.CreatedDate);
            emailSubmitDTO.Email = adUser.Email;
            emailSubmitDTO.FullName = adUser.FullName;
            emailSubmitDTO.Manager = adUser.Manager;
            emailSubmitDTO.ManagerEmail = adUser.ManagerEmail;
            emailSubmitDTO.Phone = adUser.Phone;
            emailSubmitDTO.PlaceholderMessage = userStarConfigurationDTO.Message;
            emailSubmitDTO.CorpID = userStarConfigurationDTO.CorpUserId;
            
            emailSubmitDTO.PlaceholderButtonText = "Click Here to Review STAR >";
            emailSubmitDTO.PlaceholderCongrats = "The STAR you submitted was approved!";
            emailSubmitDTO.PlaceholderView = "To review this STAR";

            _emailService.SendEmail(adUser.Email, "Star App", emailDTO);

            _emailService.SendEmail(adSubmitUser.Email, "Star App", emailSubmitDTO);

        }
        else if (userStarConfigurationDTO.Status == "D")
        {
            var adUser = _lDAPService.GetUserFromAD(userStarConfig.CreatedBy, false);
            EmailDTO emailDTO = new EmailDTO();
            emailDTO.CorpID = userStarConfigurationDTO.CorpUserId;
            emailDTO.CreatedBy = userStarConfig.CreatedBy;
            emailDTO.CreatedDate = DateTime.Parse(userStarConfig.CreatedDate);
            emailDTO.Email = adUser.Email;
            emailDTO.FullName = adUser.FullName;
            emailDTO.Manager = adUser.Manager;
            emailDTO.ManagerEmail = adUser.ManagerEmail;
            emailDTO.Phone = adUser.Phone;
            emailDTO.Status = "D";
            emailDTO.PlaceholderDenial = userStarConfigurationDTO.Feedback;
            emailDTO.PlaceholderMessage = userStarConfigurationDTO.Message;

            emailDTO.PlaceholderButtonText = "Click Here to Review STAR >";
            emailDTO.PlaceholderCongrats = "The STAR you submitted was denied.";
            emailDTO.PlaceholderView = "To resubmit,";

            _emailService.SendEmail(adSubmitUser.Email, "Star App", emailDTO);
        }

    }

    private void SendEmail(UserStarConfigurationDTO userStarConfigurationDTO)
    {
        var adUser = _lDAPService.GetUserFromAD(userStarConfigurationDTO.CorpUserId, false);
        EmailDTO emailDTO = new EmailDTO();
        emailDTO.CorpID = userStarConfigurationDTO.CorpUserId;
        emailDTO.CreatedBy = userStarConfigurationDTO.CreatedBy;
        emailDTO.CreatedDate = DateTime.Parse(userStarConfigurationDTO.CreatedDate);
        emailDTO.Email = adUser.Email;
        emailDTO.FullName = adUser.FullName;
        emailDTO.Manager = adUser.Manager;
        emailDTO.ManagerEmail = adUser.ManagerEmail;
        emailDTO.Phone = adUser.Phone;
        emailDTO.PlaceholderMessage = userStarConfigurationDTO.Message;

        emailDTO.PlaceholderButtonText = "Click Here to Review STAR >";
        emailDTO.PlaceholderCongrats = "A new STAR has been submitted for your review";
        emailDTO.PlaceholderView = "To review this STAR";

        EmailDTO emailManagerDTO = new EmailDTO();
        emailManagerDTO.CorpID = userStarConfigurationDTO.CorpUserId;
        emailManagerDTO.CreatedBy = userStarConfigurationDTO.CreatedBy;
        emailManagerDTO.CreatedDate = DateTime.Parse(userStarConfigurationDTO.CreatedDate);
        emailManagerDTO.Email = adUser.Email;
        emailManagerDTO.FullName = adUser.FullName;
        emailManagerDTO.Manager = adUser.Manager;
        emailManagerDTO.ManagerEmail = adUser.ManagerEmail;
        emailManagerDTO.Phone = adUser.Phone;
        emailManagerDTO.PlaceholderMessage = userStarConfigurationDTO.Message;

        emailManagerDTO.PlaceholderButtonText = "Click Here to view STAR >";
        emailManagerDTO.PlaceholderCongrats = "Congratulations, a new STAR has been submitted for one of your employee";
        emailManagerDTO.PlaceholderView = "To view this STAR";

        _emailService.SendEmail("", "STAR App", emailDTO,_userManagementRepository.GetAdminEmail().Result.ToList());

        _emailService.SendEmail(adUser.ManagerEmail, "STAR App", emailManagerDTO);
    }
}

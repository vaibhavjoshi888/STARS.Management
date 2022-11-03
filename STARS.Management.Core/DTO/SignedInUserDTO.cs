namespace STARS.Management.Core.DTO;

public class SignedInUserDTO
{
        public int AppUserId { get; set; }
        public string CorpUserId { get; set; }
        public string Email { get; set; }
        public string DisplayName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string Initial { get; set; }
        public string ThumbnailPhoto { get; set; }
        public bool HasThumbnailPhoto { get; set; }
        public bool CanViewDailySchedule { get; set; }
}

namespace STARS.Management.Core.DTO;

public class UpdateStarRequestDTO
{
    public string CorpUserId { get; set; }
    public string EmployeeName { get; set; }
    public string Message { get; set; }
    public string Status { get; set; }
    public int Approvedby { get; set; }
    public string ModifiedBy { get; set; }
    public string Feedback { get; set; }

}

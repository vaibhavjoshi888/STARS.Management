namespace STARS.Management.Core.DTO;

public class StarRequestCountDTO
{
    public int TotalSubmited { get; set; }
    public int Approved { get; set; }
    public int Pending { get; set; }
    public int Denied { get; set; }
}

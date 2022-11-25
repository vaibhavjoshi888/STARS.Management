namespace STARS.Management.Core.DTO;
using System;
public class EmailDTO
{
    public string CorpID { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string FullName { get; set; }
    public string Manager { get; set; }
    public string ManagerDisplayName { get; set; }
    public string ManagerEmail { get; set; }
    public string ManagerCorpID { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    public string PlaceholderCongrats { get; set; }
    public string PlaceholderView { get; set; }
    public string PlaceholderMessage { get; set; }
    public string PlaceholderDenial { get; set; }
    public string PlaceholderButtonText { get; set; }
    public string Status { get; set; }

}
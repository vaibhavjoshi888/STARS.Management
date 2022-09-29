namespace STARS.Management.Core.Models;
using System;
public class ADUser
{
    public string CorpID { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string FullName { get; set; }
    public string DisplayName { get; set; }
    public string GivenName { get; set; }
    public string Surname { get; set; }
    public string SamaAccountName { get; set; }
    public string PhysicalDeliveryOfficeName { get; set; }
    public string EmployeeType { get; set; }
    public string EmployeeId { get; set; }
    public string EmployeeNumber { get; set; }
    public string Title { get; set; }
    public string Department { get; set; }
    public string Division { get; set; }
    public string Manager { get; set; }
    public string ManagerDisplayName { get; set; }
    public string ManagerEmail { get; set; }
    public string ManagerCorpID { get; set; }
    public Byte[] ThumbnailPhoto { get; set; }
}

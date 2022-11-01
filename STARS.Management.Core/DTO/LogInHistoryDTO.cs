using System;

namespace STARS.Management.Core.DTO;

public class LogInHistoryDTO
{
    public string UserName { get; set; }
    public DateTime LoginTime { get; set; }
    public int LoginAttempt { get; set; }
}

namespace STARS.Management.Infrastructure.UserManagement.SQL;

public static class UserSqlList
{
    private static readonly  string _Namespace="STARS.Management.Infrastructure.UserManagement.SQL";
    internal static string _insert_app_user { get => $"{_Namespace}.inser_app-user.sql";  }
    
}

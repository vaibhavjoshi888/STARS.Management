namespace STARS.Management.Infrastructure.UserManagement.SQL;

public static class UserSqlList
{
    private static readonly  string _Namespace="STARS.Management.Infrastructure.UserManagement.SQL";
    internal static string _insert_app_user { get => $"{_Namespace}.insert_app-user.sql";  }
    internal static string GetallUsers { get => $"{_Namespace}.get_all_users.sql";  }
    internal static string GetallRoles { get => $"{_Namespace}.get_all_roles.sql";  }

}

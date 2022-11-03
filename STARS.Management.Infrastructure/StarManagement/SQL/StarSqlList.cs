namespace STARS.Management.Infrastructure.StarManagement.SQL;

public static class StarSqlList
{
    private static readonly string _Namespace = "STARS.Management.Infrastructure.UserManagement.SQL";
    internal static string _insert_app_user { get => $"{_Namespace}.insert_app-user.sql"; }
    internal static string GetallUsers { get => $"{_Namespace}.get_all_users.sql"; }
    internal static string GetallRoles { get => $"{_Namespace}.get_all_roles.sql"; }
    internal static string GetUserRole { get => $"{_Namespace}.get_user_role.sql"; }
    internal static string delete_app_user { get => $"{_Namespace}.delete_app-user.sql"; }
    internal static string Update_app_user { get => $"{_Namespace}.update_app-user.sql"; }
    internal static string Insert_Login_History { get => $"{_Namespace}.insert_logging_history.sql"; }
    internal static string Get_Login_History { get => $"{_Namespace}.get_login_history.sql"; }
    internal static string Update_Login_History { get => $"{_Namespace}.update_login_history.sql"; }
    internal static string Delete_Login_History { get => $"{_Namespace}.delete_loginHistory.sql"; }



}

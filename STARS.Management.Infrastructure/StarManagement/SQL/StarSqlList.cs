namespace STARS.Management.Infrastructure.StarManagement.SQL;

public static class StarSqlList
{
    private static readonly string _Namespace = "STARS.Management.Infrastructure.UserManagement.SQL";
    internal static string Insert_Submit_Star_Config { get => $"{_Namespace}.insert_submit_star_config.sql"; }
    internal static string Get_all_user_star_config_request { get => $"{_Namespace}.get_all_user_star_config_request.sql"; }
    internal static string Updatel_star_request { get => $"{_Namespace}.updatel_star_request.sql"; }

}

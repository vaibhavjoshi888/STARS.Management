using STARS.Management.Core.Interface;
using System.IO;
using System.Reflection;
namespace STARS.Management.Core.Services;

public class QueryProviderService : IQueryProviderService
{
    public string GetQuery(string queryName)
    {
        string sqlQuery;
        var assembly = Assembly.GetCallingAssembly();
        using (Stream stream = assembly.GetManifestResourceStream(queryName))
        {

            using (StreamReader streamReader = new StreamReader(stream))
            {
                sqlQuery = streamReader.ReadToEnd();
            }

        }
        return sqlQuery;
    }
}

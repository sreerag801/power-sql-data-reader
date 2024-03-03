using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PowerDataNet.SqlServer;

namespace PowerDataNet.Infr
{
    public static class BindPowerDataModules
    {
        public static void Bind(this IServiceCollection collection)
        {
            collection.AddSingleton<ISqlConnectionStringProvider>(s => new GetSqlConnectionFromConfiguration(s.GetService<IConfiguration>(), "MyDatabase"));
        }
    }
}

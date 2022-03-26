using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using GeeksBank.Core.Api.Infrastructure.Extensions;
using GeeksBank.Core.Infrastructure.Repository;

namespace GeeksBank.Core.Api.Infrastructure.ServiceRegistrations
{
    public class RegisterDatabaseInstances : IServiceRegistration
    {
        public void RegisterAppServices(IServiceCollection services, IConfiguration configuration)
        {
            /* services.AddDbContext<SqlServerDbContext>(options =>
            {
                options.UseSqlServer(configuration["SQLServerConnection"]);
            }); */
                        var connectionString = configuration["ConnectionStrings:DefaultConnection"];
                        var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            services.AddDbContext<GeeksBankDbContext>(o => o.UseSqlServer(connectionString, b => b.MigrationsAssembly(migrationsAssembly)));
        }
    }
}

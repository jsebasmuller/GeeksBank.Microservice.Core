using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using GeeksBank.Core.Api.Infrastructure.Extensions;

namespace GeeksBank.Core.Api.Infrastructure.ServiceRegistrations
{
    internal class RegisterRedis : IServiceRegistration
    {
        public void RegisterAppServices(IServiceCollection services, IConfiguration configuration)
        {
            // Redis configuration
            // services.AddRedisCache(configuration);
        }
    }
}

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using GeeksBank.Core.Api.Infrastructure.Extensions;

namespace GeeksBank.Core.Api.Infrastructure.ServiceRegistrations
{
    internal class RegisterHealthChecks : IServiceRegistration
    {
        public void RegisterAppServices(IServiceCollection services, IConfiguration configuration)
        {
            // Register HealthChecks and UI
            services.AddHealthChecks();
            // .AddCheck("Google Ping", new PingHealthCheck("www.google.com", 100));
        }
    }
}

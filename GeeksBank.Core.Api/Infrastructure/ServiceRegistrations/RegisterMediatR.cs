using System.Reflection;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using GeeksBank.Core.Api.Infrastructure.Extensions;

namespace GeeksBank.Core.Api.Infrastructure.ServiceRegistrations
{
    public class RegisterMediatR : IServiceRegistration
    {
        public void RegisterAppServices(IServiceCollection services, IConfiguration configuration)
        {
            // Scan assemblies and add handlers, preprocessors, and postprocessors implementations to the container.
            services.AddMediatR(typeof(Startup).GetTypeInfo().Assembly);
        }
    }
}

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using GeeksBank.Core.Api.Infrastructure.Extensions;

namespace GeeksBank.Core.Api.Infrastructure.ServiceRegistrations
{
    public class RegisterCors : IServiceRegistration
    {
        public void RegisterAppServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials()
                        .WithOrigins("http://localhost:8081")
                        .WithOrigins("http://localhost:4200")
                        .WithOrigins("http://localhost:5000")
                ); // Disable this line to local development
                //.AllowAnyOrigin()); // Enable this line to local development
            });
        }
    }
}

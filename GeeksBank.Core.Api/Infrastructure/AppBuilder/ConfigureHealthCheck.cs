using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Configuration;
using GeeksBank.Core.Api.Infrastructure.Extensions;

namespace GeeksBank.Core.Api.Infrastructure.AppBuilder
{
    /// <summary>
    /// 
    /// </summary>
    public class ConfigureHealthChecks : ICustomAppBuilder
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="configuration"></param>
        public void ConfigureApp(IApplicationBuilder app, IConfiguration configuration)
        {
            //Enable HealthChecks and UI
            app.UseHealthChecks("/health", new HealthCheckOptions { });
        }
    }
}

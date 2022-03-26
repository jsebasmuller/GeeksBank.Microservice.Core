using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using GeeksBank.Core.Api.Infrastructure.Extensions;
using GeeksBank.Report.Api.Constants;

namespace GeeksBank.Core.Api.Infrastructure.AppBuilder
{
    /// <summary>
    /// 
    /// </summary>
    public class ConfigureSwagger : ICustomAppBuilder
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="configuration"></param>
        public void ConfigureApp(IApplicationBuilder app, IConfiguration configuration)
        {
            // Use swagger Doc
          app.UseSwagger(c =>
            {
                c.RouteTemplate = $"{ServiceConstants.ContextPath}swagger/{{documentName}}/swagger.json";
            });
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"v1/swagger.json", "GeeksBank.Core Template API Example");
                c.RoutePrefix = $"{ServiceConstants.ContextPath}swagger";
            });
        }
    }
}

using Autofac;
using Autofac.Extensions.DependencyInjection;
using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Exceptions;
using GeeksBank.Core.Api.Infrastructure.AutofacModules;
using GeeksBank.Core.Api.Infrastructure.Extensions;
using GeeksBank.Core.Domain.AggregatesModel.ResultsAggregate;
using GeeksBank.Core.Domain.AggregatesModel.FibonacciAggregate;
using GeeksBank.Core.Infrastructure.Repository;
using GeeksBank.Core.Api.SeedWork;
using Serilog.Formatting.Json;
using Microsoft.Extensions.Logging;
using GeeksBank.Core.Infrastructure;
using GeeksBank.Core.Domain.Helpers;

namespace GeeksBank.Core.Api
{
    public class Startup
    {
        /// <summary>
        /// Startup Main Class
        /// </summary>
        /// <param name="env"></param>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .WriteTo.Console(new JsonFormatter())
                .Enrich.WithExceptionDetails()
                .Filter.ByExcluding("RequestPath = '/health' and StatusCode = 200")
                .CreateLogger();

            Log.Information("Starting up" + env.EnvironmentName);
        }

        private readonly IConfiguration _configuration;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddServicesInAssembly(_configuration);

            services.AddSignalR(options =>
            {
                options.EnableDetailedErrors = true;
            }

            );
            services.AddControllers();
            services.AddMediatR(typeof(Startup).GetTypeInfo().Assembly);

            services.AddHttpContextAccessor();
            services.AddScoped(typeof(IResultsRepository), typeof(ResultsRepository));
            services.AddScoped(typeof(IFibonacciRepository), typeof(FibonacciRepository));

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme);

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
            services.AddValidatorsFromAssemblyContaining(typeof(Startup));

            services.Configure<ConfigServerData>(_configuration);
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            AESExtension._key = AESExtension.MD5Hash(_configuration.GetValue<string>("AESKey") != null ? _configuration.GetValue<string>("AESKey") : "geeksbank2021");
            //configure autofac
            var container = new ContainerBuilder();
            container.Populate(services);
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.AddAppConfigurationsInAssembly(_configuration);

            // Si se necesita Https, agregar en la siguiente linea el certificado
            // app.UseHttpsRedirection();
            app.UseRouting();
            app.UseWebSockets();

            app.UseCors("CorsPolicy");

            app.UseSerilogRequestLogging(opts =>
            {
                opts.EnrichDiagnosticContext = LogHelper.EnrichFromRequest;
                opts.GetLevel = LogHelper.ExcludeHealthChecks; // Use the custom level
            });

            app.ConfigureExceptionHandler(_configuration);

            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health");
            });

            loggerFactory.AddSerilog();
        }


        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new InfrastructureModule(_configuration));
            builder.RegisterModule(new MediatorModule());
        }
    }
}

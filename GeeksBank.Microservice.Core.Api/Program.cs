using System;
using System.IO;
using Autofac.Extensions.DependencyInjection;
using Figgle;
using IdentityServer4.EntityFramework.DbContexts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using GeeksBank.Microservice.Core.Api.Infrastructure.Extensions;
using GeeksBank.Microservice.Core.Infrastructure;
using GeeksBank.Microservice.Core.Api.Infrastructure.Seeding;
using GeeksBank.Microservice.Core.Api.SeedWork;
using Serilog;

namespace GeeksBank.Microservice.Core.Api
{
    public static class Program
    {
        public static readonly string ServiceName = "GeeksBank CoreService. ";

        public static void Main(string[] args)
        {
            try
            {
                Console.WriteLine(FiggleFonts.Standard.Render(ServiceName));
                var host = CreateHostBuilder(args).Build();
                
                host.MigrateDatabase<CoresContext>((context, services) =>
                {
                    // new CoresContextSeed(services)
                    //     .SeedAsync()
                    //     .Wait();
                });
                
                //PersistedGrantDbContext
                /*
                host.MigrateDatabase<PersistedGrantDbContext>((context, services) =>
                {
                });
                */
                //ConfigurationDbContext
                host.MigrateDatabase<ConfigurationDbContext>((context, services) =>
                {
                     new IdentityServerSeed()
                         .InitializeDatabaseAsync(services)
                         .Wait();
                });
                
                host.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "{@ServiceName} terminated unexpectedly ({ApplicationContext})!", ServiceName);
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.Sources.Clear();
                    var env = hostingContext.HostingEnvironment;
                    config.SetBasePath(Directory.GetCurrentDirectory() + "/Configuration")
                        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                        .AddJsonFile(ConfigMapFileProvider.FromRelativePath("/Configuration"),
                            $"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);
                })
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                        .UseContentRoot(Directory.GetCurrentDirectory())
                        .UseIISIntegration()
                        .UseStartup<Startup>();
                });
    }
}
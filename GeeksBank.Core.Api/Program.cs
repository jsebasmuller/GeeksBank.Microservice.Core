using System.IO;
using Autofac.Extensions.DependencyInjection;
using GeeksBank.Core.Api.SeedWork;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using System.Net;
using System;
using Steeltoe.Extensions.Configuration.ConfigServer;
using GeeksBank.Core.Api.Infrastructure.Extensions;
using GeeksBank.Core.Infrastructure.Repository;
namespace GeeksBank.Core.Api
{
    public static class Program
    {
        public static void Main(string[] args)
        {
           var host  =  CreateHostBuilder(args).Build();
           host.MigrateDatabase<GeeksBankDbContext>((context, services) =>
                {
                });
            host.Run();
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
                        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                        .AddConfigServer();

                    config.Build();
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

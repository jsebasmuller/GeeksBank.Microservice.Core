using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace GeeksBank.Core.Api.Infrastructure.Extensions
{
    public static class MigrationManager
    {
        /// <summary>
        /// We are using the IWebHost type because this allows us to chain this method in the Program.cs
        /// </summary>
        /// <param name="host"></param>
        /// <param name="seeder"></param>
        /// <returns></returns>
        public static IHost MigrateDatabase<TContext>(this IHost host, Action<TContext, IServiceProvider> seeder) where TContext : DbContext
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var context = services.GetService<TContext>();

                try
                {
                    Log.Information("Migrating database associated with context {DbContextName}", typeof(TContext).Name);

                    InvokeSeeder(seeder, context, services);

                    Log.Information("Migrated database associated with context {DbContextName}", typeof(TContext).Name);

                }
                catch (Exception ex)
                {
                    Log.Error(ex, "An error occurred while migrating the database used on context {DbContextName}", typeof(TContext).Name);

                    throw;
                }
            }

            return host;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="seeder"></param>
        /// <param name="context"></param>
        /// <param name="services"></param>
        /// <typeparam name="TContext"></typeparam>
        private static void InvokeSeeder<TContext>(Action<TContext, IServiceProvider> seeder, TContext context, IServiceProvider services)
            where TContext : DbContext
        {
            context.Database.Migrate();
            seeder(context, services);
        } 
        
    }
}
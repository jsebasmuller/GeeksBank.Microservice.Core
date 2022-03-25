using IdentityModel;
using IdentityServer4;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace GeeksBank.Microservice.Core.Api.Infrastructure.Seeding
{
    public class IdentityServerSeed
    {
        public async Task InitializeDatabaseAsync(IServiceProvider app)
        {
            var context = app.GetRequiredService<ConfigurationDbContext>();
            context.Database.Migrate();
            /*
            var roleStore = new RoleStore<IdentityRole>(context); //Pass the instance of your DbContext here
            var roleManager = new RoleManager<IdentityRole>(roleStore,null,null,null,null);
            await roleManager.CreateAsync(new IdentityRole { Name = "Admin" });
            */
        }
    }
}
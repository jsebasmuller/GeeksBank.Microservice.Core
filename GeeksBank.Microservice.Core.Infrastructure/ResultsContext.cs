using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using GeeksBank.Microservice.Core.Domain.SeedWork;
using GeeksBank.Microservice.Core.Infrastructure.Models;

namespace GeeksBank.Microservice.Core.Infrastructure
{
    public class ResultsContext : IdentityDbContext<Results>, IUnitOfWork
    {
        private readonly IConfiguration _configuration;
        
        public ResultsContext(DbContextOptions<ResultsContext> options) : base(options)
        {
        }

        public DbSet<Results> Results { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema("geeksbank");
            builder.Entity<Results>()
                .HasIndex(u => new {u.Id})
                .IsUnique();
            base.OnModelCreating(builder);
        }
        
        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            // After executing this line all the changes (from the Command Handler and Domain Event Handlers) 
            // performed through the DbContext will be committed
            _ = await base.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
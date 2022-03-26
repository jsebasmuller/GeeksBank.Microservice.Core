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
    public class ResultsContext : DbContext, IUnitOfWork
    {
        private readonly IConfiguration _configuration;

        public ResultsContext(DbContextOptions<ResultsContext> options) : base(options)
        {
        }

        public DbSet<Results> Results { get; set; }
        public DbSet<Fibonacci> Fibonacci { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema("geeksbank");
            builder.Entity<Fibonacci>()
                .HasIndex(u => new { u.Id })
                .IsUnique();
            builder.Entity<Results>()
                .HasIndex(u => new { u.Id })
                .IsUnique();
            int a, b, limite, i, auxiliar;
            limite = 100;
            a = 0;
            b = 1;
            builder.Entity<Fibonacci>()
              .HasData(
               new Fibonacci { Id = 1, Number = 0 },
               new Fibonacci { Id = 2, Number = 1 }
              );
            for (i = 3; i <= limite; i++)
            {

                auxiliar = a;
                a = b;
                b = auxiliar + a;
                builder.Entity<Fibonacci>()
                .HasData(
                    new Fibonacci { Id = i, Number = b }
                );
            }
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
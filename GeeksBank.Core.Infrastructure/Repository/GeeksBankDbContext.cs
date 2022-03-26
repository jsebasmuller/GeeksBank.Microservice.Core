using Microsoft.EntityFrameworkCore;
using GeeksBank.Core.Infrastructure.Models;
using System.Threading;
using System.Threading.Tasks;
using GeeksBank.Core.Domain.SeedWork;

namespace GeeksBank.Core.Infrastructure.Repository
{
    public class GeeksBankDbContext : DbContext, IUnitOfWork
    {

        public GeeksBankDbContext(DbContextOptions<GeeksBankDbContext> options)
        : base(options)
        {
        }
        public virtual DbSet<Results> Results { get; set; }
        public virtual DbSet<Fibonacci> Fibonacci { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Results>(entity =>
            {
                entity.HasKey(e => e.Id);
            });
            
            modelBuilder.Entity<Fibonacci>(entity =>
            {
                entity.HasKey(e => e.Id);
            });
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
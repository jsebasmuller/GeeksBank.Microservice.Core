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

      int n1 = 0;
      int n2 = 1;
      int n3 = 0;
      Fibonacci[] array = new Fibonacci[100];
      array[0] = new Fibonacci { Id = 1, Number = 0 };
      array[1] = new Fibonacci { Id = 2, Number = 1 };
      for (int i = 2; i < 100; ++i)    
      {
        n3 = n2 + n1;
        n1 = n2;
        n2 = n3;
        array[i] = new Fibonacci { Id = i + 1, Number = n3 };
      }
      modelBuilder.Entity<Fibonacci>().HasData(array);
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
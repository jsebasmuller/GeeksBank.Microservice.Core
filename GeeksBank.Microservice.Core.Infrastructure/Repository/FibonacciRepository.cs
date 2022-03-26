using GeeksBank.Microservice.Core.Domain.AggregatesModel.FibonacciAggregate;
using GeeksBank.Microservice.Core.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeeksBank.Microservice.Core.Infrastructure.Repository
{
  public class FibonacciRepository : IFibonacciRepository
  {
    private ResultsContext _context;

    public FibonacciRepository(ResultsContext resultContext)
    {
      _context = resultContext;
    }

    public IUnitOfWork UnitOfWork => _context;

    public async Task<List<int>> GetFibonacci()
    {
      return null;
    }
  }
}

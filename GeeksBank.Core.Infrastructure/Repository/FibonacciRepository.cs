using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using GeeksBank.Core.Domain.AggregatesModel.FibonacciAggregate;
using Serilog;
using GeeksBank.Core.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace GeeksBank.Core.Infrastructure.Repository
{
    public class FibonacciRepository : IFibonacciRepository
    {
        private GeeksBankDbContext _context;

        private readonly ILogger _logger = Log.ForContext<FibonacciRepository>();
        public IUnitOfWork UnitOfWork => _context;


        public FibonacciRepository(GeeksBankDbContext usersContext)
        {
            _context = usersContext;
        }

        public async Task<List<long>> GetFibonacci()
        {
            return await _context.Fibonacci.Select(x => x.Number).ToListAsync();
        }

        public async Task SaveAsync(){
            await UnitOfWork.SaveEntitiesAsync();
        }
    }
}
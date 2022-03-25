using System.Threading.Tasks;
using System.Collections.Generic;
using GeeksBank.Microservice.Core.Domain.SeedWork;

namespace GeeksBank.Microservice.Core.Domain.AggregatesModel.FibonacciAggregate
{
    public interface IFibonacciRepository : IRepository<Fibonacci>
    {
        Task<List<int>> GetFibonacci();
       
    }
}
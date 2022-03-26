using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GeeksBank.Core.Domain.AggregatesModel.FibonacciAggregate
{
    public interface IFibonacciRepository
    {
        Task<List<int>> GetFibonacci();
        Task SaveAsync();
    }
}
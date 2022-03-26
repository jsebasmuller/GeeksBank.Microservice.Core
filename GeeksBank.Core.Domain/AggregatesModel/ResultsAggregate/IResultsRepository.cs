using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GeeksBank.Core.Domain.AggregatesModel.ResultsAggregate
{
    public interface IResultsRepository
    {
        Task<Response> AddResult(Results result);
        Task SaveAsync();
    }
}
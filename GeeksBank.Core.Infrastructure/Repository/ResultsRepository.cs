using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using GeeksBank.Core.Domain.AggregatesModel.ResultsAggregate;
using Serilog;
using GeeksBank.Core.Domain.SeedWork;

namespace GeeksBank.Core.Infrastructure.Repository
{
  public class ResultsRepository : IResultsRepository
  {
    private GeeksBankDbContext _context;

    private readonly ILogger _logger = Log.ForContext<ResultsRepository>();
    public IUnitOfWork UnitOfWork => _context;


    public ResultsRepository(GeeksBankDbContext resultsContext)
    {
      _context = resultsContext;
    }

    public async Task<Response> AddResult(Results result)
    {
      var resultSave = result.Adapt<GeeksBank.Core.Infrastructure.Models.Results>();
      resultSave.Id = Guid.NewGuid().ToString();
      resultSave.Result = resultSave.Number1 + resultSave.Number2;
      await _context.Results.AddAsync(resultSave);
      Response resp = new Response();
      resp.Result = resultSave.Result;
      resp.IsInFibonacci = _context.Fibonacci.Any(x => x.Number == resp.Result);
      this.SaveAsync();
      return resp;
    }

    public async Task SaveAsync()
    {
      await UnitOfWork.SaveEntitiesAsync();
    }
  }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using GeeksBank.Microservice.Core.Domain.AggregatesModel.ResultAggregate;
using GeeksBank.Microservice.Core.Domain.SeedWork;
using GeeksBank.Microservice.Core.Infrastructure.Models;
using System.Web;
namespace GeeksBank.Microservice.Core.Infrastructure.Repository
{
  public class ResultsRepository : IResultsRepository
  {
    private ResultsContext _context;

    public ResultsRepository(ResultsContext resultContext)
    {
      _context = resultContext;
    }

    public IUnitOfWork UnitOfWork => _context;

    public async Task<Response> CreateResponse(Domain.AggregatesModel.ResultAggregate.Results request)
    {
      return null;
    }
  }
}
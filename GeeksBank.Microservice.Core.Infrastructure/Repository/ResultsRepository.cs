using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using GeeksBank.Microservice.Core.Domain.AggregatesModel.ResultAggregate;
using GeeksBank.Microservice.Core.Domain.Exception;
using GeeksBank.Microservice.Core.Domain.SeedWork;
using GeeksBank.Microservice.Core.Infrastructure.Models;
using GeeksBank.Microservice.Core.Infrastructure.Remote;
using System.Web;
namespace GeeksBank.Microservice.Core.Infrastructure.Repository
{
     public class ResultsRepository : IResultsRepository
    {
        private ResultsContext _context;
        public IUnitOfWork UnitOfWork => _context;
        
        public ResultsRepository(ResultsContext resultContext)
        {
            _context = resultContext;
        }
    }
}
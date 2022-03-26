using System.Threading.Tasks;
using GeeksBank.Microservice.Core.Domain.SeedWork;

namespace GeeksBank.Microservice.Core.Domain.AggregatesModel.ResultAggregate
{
  public interface IResultsRepository : IRepository<Results>
  {
    public IUnitOfWork UnitOfWork { get; }
    Task<Response> CreateResponse(Results result);

  }
}
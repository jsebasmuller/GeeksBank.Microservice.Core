using System.Threading.Tasks;
using GeeksBank.Microservice.Core.Domain.SeedWork;

namespace GeeksBank.Microservice.Core.Domain.AggregatesModel.ResultAggregate
{
    public interface IResultsRepository : IRepository<Results>
    {
        Task<List<int>> CreateResponse(UserAccount user);
        Task<UserAccount> UpdateUser(UserAccount user);
       
    }
}
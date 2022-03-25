using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using GeeksBank.Microservice.Core.Domain.SeedWork;
using GeeksBank.Microservice.Core.Infrastructure.Models;

namespace GeeksBank.Microservice.Core.Infrastructure.Remote
{
    public interface ICoreRemoteFinder : IRemoteFinder<ThirdCoreDto,string>
    {
    }

    public class ThirdCoreRemoteClient : ICoreRemoteFinder
    {
        public async Task<ThirdCoreDto> FindByIdAsync(string productId)
        {
            var singleGeocodeResponse = await "https://jsonplaceholder.typicode.com"
                .AppendPathSegment("todos")
                .AppendPathSegment(productId.ToString())
                .GetJsonAsync<ThirdCoreDto>();

            return singleGeocodeResponse;
        }
    }
}

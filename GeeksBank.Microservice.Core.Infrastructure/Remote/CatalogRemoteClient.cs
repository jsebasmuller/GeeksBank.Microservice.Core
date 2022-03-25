using System.Collections.Generic;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using Microsoft.Extensions.Configuration;
using GeeksBank.Microservice.Core.Domain.AggregatesModel.IntegrationAggregate;
using GeeksBank.Microservice.Core.Domain.SeedWork;
using GeeksBank.Microservice.Core.Infrastructure.Models;

namespace GeeksBank.Microservice.Core.Infrastructure.Remote
{
    public interface ICatalogRemoteFinder
    {
        Task<List<Subscriptions>> FindSubscriptions();
    }

    public class CatalogRemoteClient : ICatalogRemoteFinder
    {
        private readonly IConfigurationEndpoints _configuration;

        public CatalogRemoteClient(IConfigurationEndpoints configuration)
        {
            _configuration = configuration;
        }

        public async Task<List<Subscriptions>> FindSubscriptions()
        {
            var catalogUrl = _configuration.GetConfiguration("CatalogSubscriptions");
            var singleGeocodeResponse = await $"{catalogUrl.BaseUrl}{catalogUrl.Path}".GetJsonAsync<List<Subscriptions>>();
            return singleGeocodeResponse;
        }
    }
}
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using GeeksBank.IntegrationTest.Fixtures;
using GeeksBank.Core.Api;
using Xunit;

namespace GeeksBank.IntegrationTest
{
    public class HealthCheckTest : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient client;

        public HealthCheckTest(CustomWebApplicationFactory<Startup> factory) =>
            client = factory.CreateClient();

        [Fact]
        public async Task GetStatus_Default_Returns200Ok()
        {
            var response = await this.client.GetAsync("/health");

            response.EnsureSuccessStatusCode(); // Status Code 200-299

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}

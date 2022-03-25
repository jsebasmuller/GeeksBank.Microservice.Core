using GeeksBank.Microservice.Core.Infrastructure.Remote;
using Xunit;

namespace GeeksBank.Microservice.Core.Infrastructure.Test.Remote
{
    public class ThirdCoreRemoteClientTest
    {
        private readonly ThirdCoreRemoteClient _thirdCoreRemoteClient;

        public ThirdCoreRemoteClientTest()
        {
            _thirdCoreRemoteClient = new ThirdCoreRemoteClient();
        }
        
        
        [Fact]
        public async void CheckFlurlCientTest()
        {
            // Act
            var result =  await _thirdCoreRemoteClient.FindByIdAsync("1");
            // Assert
            Assert.NotNull(result);
        }
    }
}

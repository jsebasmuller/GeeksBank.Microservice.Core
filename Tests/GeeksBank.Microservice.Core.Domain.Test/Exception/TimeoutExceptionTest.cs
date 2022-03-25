using Microsoft.AspNetCore.Http;
using GeeksBank.Microservice.Core.Domain.Exception;
using Xunit;

namespace GeeksBank.Microservice.Core.Domain.Test.Exception
{
    public class TimeoutExceptionTest
    {
        private readonly string details = "Timeout Exception Detail";
        
        [Fact]
        public void CreateExceptionTest()
        {
            var exception = new TimeoutException(details);
            //Asserts
            Assert.Equal(details, exception.Details);
            Assert.Equal(StatusCodes.Status408RequestTimeout, exception.StatusCode);
            Assert.Equal("Request Timeout", exception.Message);
            
        }
    }
}

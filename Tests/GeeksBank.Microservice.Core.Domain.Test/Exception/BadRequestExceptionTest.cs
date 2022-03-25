using Microsoft.AspNetCore.Http;
using GeeksBank.Microservice.Core.Domain.Exception;
using Xunit;

namespace GeeksBank.Microservice.Core.Domain.Test.Exception
{
    public class BadRequestExceptionTest
    {
        private readonly string details = "BadRequestException Detail";
        
        [Fact]
        public void CreateExceptionTest()
        {
            var exception = new BadRequestException(details);
            //Asserts
            Assert.Equal(details, exception.Details);
            Assert.Equal(StatusCodes.Status400BadRequest, exception.StatusCode);
            Assert.Equal("Bad Request", exception.Message);
            
        }
    }
}

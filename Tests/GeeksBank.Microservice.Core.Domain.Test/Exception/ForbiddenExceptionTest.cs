using Microsoft.AspNetCore.Http;
using GeeksBank.Microservice.Core.Domain.Exception;
using Xunit;

namespace GeeksBank.Microservice.Core.Domain.Test.Exception
{
    public class ForbiddenExceptionTest
    {
        private readonly string details = "ForbiddenException Detail";
        
        [Fact]
        public void CreateExceptionTest()
        {
            var exception = new ForbiddenException(details);
            //Asserts
            Assert.Equal(details, exception.Details);
            Assert.Equal(StatusCodes.Status403Forbidden, exception.StatusCode);
            Assert.Equal("Forbidden", exception.Message);
            
        }
    }
}

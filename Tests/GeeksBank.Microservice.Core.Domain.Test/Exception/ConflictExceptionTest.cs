using Microsoft.AspNetCore.Http;
using GeeksBank.Microservice.Core.Domain.Exception;
using Xunit;

namespace GeeksBank.Microservice.Core.Domain.Test.Exception
{
    public class ConflictExceptionTest
    {
        private readonly string details = "ConflictException Detail";
        
        [Fact]
        public void CreateExceptionTest()
        {
            var exception = new ConflictException(details);
            //Asserts
            Assert.Equal(details, exception.Details);
            Assert.Equal(StatusCodes.Status409Conflict, exception.StatusCode);
            Assert.Equal("Conflict", exception.Message);
            
        }
    }
}

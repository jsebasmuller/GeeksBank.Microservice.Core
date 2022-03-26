using Microsoft.AspNetCore.Http;
using GeeksBank.Core.Domain.Exception;
using Xunit;

namespace GeeksBank.Core.Domain.Test.Exception
{
    public class BusinessConflictExceptionTest
    {
        [Fact]
        public void CreateExceptionTest()
        {
            var exception = new BusinessException("code", "message");
            Assert.Equal(StatusCodes.Status409Conflict, exception.StatusCode);
        }
    }
}

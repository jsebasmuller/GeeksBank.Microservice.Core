using System;
using Microsoft.AspNetCore.Http;

namespace GeeksBank.Microservice.Core.Domain.Exception
{
    [Serializable]
    public sealed class BusinessException : ClientErrorException
    {
        public BusinessException(
            string code, string message, string details = null
        ) : base(StatusCodes.Status409Conflict, message, details, code)
        {
        }
        public BusinessException(
            string message
        ) : base(StatusCodes.Status409Conflict, message,null,null)
        {
        }
    }
}
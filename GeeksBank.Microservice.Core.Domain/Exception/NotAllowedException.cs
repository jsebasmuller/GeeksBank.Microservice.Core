using System;
using Microsoft.AspNetCore.Http;

namespace GeeksBank.Microservice.Core.Domain.Exception
{
    [Serializable]
    public sealed class NotAllowedException : ClientErrorException
    {
        
        /// <summary>
        /// Create 405 NotAllowedException
        /// </summary>
        /// <param name="message"></param>
        /// <param name="details"></param>
        public NotAllowedException(string details = null) : base(StatusCodes.Status405MethodNotAllowed,
            "Method Not Allowed",
            details)
        {
        }
    }
}

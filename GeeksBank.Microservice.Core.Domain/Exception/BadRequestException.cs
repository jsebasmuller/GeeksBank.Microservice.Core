using System;
using Microsoft.AspNetCore.Http;

namespace GeeksBank.Microservice.Core.Domain.Exception
{
    [Serializable]
    public sealed class BadRequestException : ClientErrorException
    {
        /// <summary>
        /// Create 400 BadRequestException
        /// </summary>
        /// <param name="message"></param>
        /// <param name="details"></param>
        public BadRequestException(string details = null) : base(StatusCodes.Status400BadRequest, "Bad Request",
            details)
        {
        }

        /// <summary>
        /// Create 400 BadRequestException
        /// </summary>
        /// <param name="code"></param>
        /// <param name="message"></param>
        /// <param name="details"></param>       
        public BadRequestException(string code, string message, string details = null) : base(StatusCodes.Status400BadRequest, message, details, code)
        {
        }
        
        public BadRequestException(int status, string code, string message, string details = null) : base(status, message, details, code)
        {
        }
    }
}
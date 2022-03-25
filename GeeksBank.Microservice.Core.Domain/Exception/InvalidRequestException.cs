using System;
using System.Collections.Generic;

namespace GeeksBank.Microservice.Core.Domain.Exception
{
    public class InvalidRequestException : System.Exception
    {
        public string Details { get; }

        /// <summary>
        /// Exception for request not valid
        /// </summary>
        /// <param name="message"> Message to show in response</param>
        /// <param name="details"> Message to show in response</param>
        /// <param name="translationCode"> Message to show in response </param>
        public InvalidRequestException(string message) : base(message)
        {
        }

        /// <summary>
        /// Exception for request not valid
        /// </summary>
        /// <param name="message"> Message to show in response</param>
        /// <param name="details"> Message to show in response</param>
        /// <param name="translationCode"></param>
        public InvalidRequestException(string message, string details) : base(message)
        {
            Details = details;
        }
    }
}
using System;
using System.Runtime.Serialization;
using System.Collections.Generic;

namespace GeeksBank.Core.Api.SeedWork
{
    [Serializable]
    public sealed class InvalidRequestException : Exception
    {
        private List<ErrorDetail> _details;
        public List<ErrorDetail> Details { get => _details; }

        /// <summary>
        /// Exception for request not valid
        /// </summary>
        /// <param name="message"> Message to show in response</param>
        /// <param name="details"> Message to show in response</param>
        public InvalidRequestException(string message, List<ErrorDetail> details) : base(message)
        {
            _details = details;
        }
    }
}

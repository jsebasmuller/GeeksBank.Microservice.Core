using System;
using Microsoft.AspNetCore.Identity;

namespace GeeksBank.Microservice.Core.Infrastructure.Models
{
    public class Results
    {
        public string Id { get; set; }
        public int Number1 { get; set; } 
        public int Number2 { get; set; }
        public int Result { get; set; }
    }
}
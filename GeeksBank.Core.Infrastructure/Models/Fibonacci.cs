using System;
using System.ComponentModel.DataAnnotations;

namespace GeeksBank.Core.Infrastructure.Models
{
    public class Fibonacci
    {

        [Key]
        public int Id { get; set; }
        public long Number { get; set; }
    }
}
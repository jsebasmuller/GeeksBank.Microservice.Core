using System;
using System.ComponentModel.DataAnnotations;

namespace GeeksBank.Core.Infrastructure.Models
{
    public class Results
    {

        [Key]
        public string Id { get; set; }
        public int Number1 { get; set; }
        public int Number2 { get; set; }
        public int Result { get; set; }
    }
}
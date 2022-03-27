using System;
using System.Collections.Generic;
using FluentValidation;
using GeeksBank.Core.Domain.Helpers;
using GeeksBank.Core.Domain.SeedWork;
using System.ComponentModel.DataAnnotations;

namespace GeeksBank.Core.Domain.AggregatesModel.FibonacciAggregate
{
    public class Fibonacci : IAggregateRoot, IDto
    {
        public Fibonacci()
        {

        }
        [Key]
        public int Id { get; set; }
        public long Number { get; set; }
    }
}
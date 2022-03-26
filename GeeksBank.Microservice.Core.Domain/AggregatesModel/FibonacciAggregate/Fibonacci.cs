using GeeksBank.Microservice.Core.Domain.SeedWork;

namespace GeeksBank.Microservice.Core.Domain.AggregatesModel.FibonacciAggregate
{
    public class Fibonacci : IAggregateRoot
    {
        public int Id { get; set; }
        public int Number { get; set; }
    }
}
using GeeksBank.Microservice.Core.Domain.SeedWork;

namespace GeeksBank.Microservice.Core.Domain.AggregatesModel.ResultAggregate
{
    public class Results: Entity
    {
        public int Number1 { get; set; }
        public int Number2 { get; set; }
        public int Result { get; set; }
    }
}
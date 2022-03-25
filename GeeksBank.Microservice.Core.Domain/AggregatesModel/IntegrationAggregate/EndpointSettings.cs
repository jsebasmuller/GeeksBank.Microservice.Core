namespace GeeksBank.Microservice.Core.Domain.AggregatesModel.IntegrationAggregate
{
    public class EndpointSettings
    {
        public string Name { get; set; }
        public string BaseUrl { get; set; }
        public string Path { get; set; }
        public string ApiKey { get; set; }
    }
}
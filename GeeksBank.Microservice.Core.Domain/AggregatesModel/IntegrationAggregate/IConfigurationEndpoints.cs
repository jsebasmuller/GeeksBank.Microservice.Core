namespace GeeksBank.Microservice.Core.Domain.AggregatesModel.IntegrationAggregate
{
    public interface IConfigurationEndpoints
    {
        public void AddConfiguration(EndpointSettings settings);
        public EndpointSettings GetConfiguration(string name);
    }
}
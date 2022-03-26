using Autofac;
using Microsoft.Extensions.Configuration;
using GeeksBank.Core.Infrastructure;
using GeeksBank.Core.Domain.AggregatesModel.FibonacciAggregate;
using GeeksBank.Core.Infrastructure.Repository;
using GeeksBank.Core.Domain.AggregatesModel.ResultsAggregate;

namespace GeeksBank.Core.Api.Infrastructure.AutofacModules
{
    /// <summary>
    /// Register all infrastructure related objects
    /// </summary>
    public class InfrastructureModule : Module
    {
        private readonly string _databaseConnectionString;
        private readonly IConfiguration _configuration;

        public InfrastructureModule(IConfiguration configuration)
        {
            _configuration = configuration;
            _databaseConnectionString = _configuration["SqlServerConnection"];
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SqlConnectionFactory>()
                .As<ISqlConnectionFactory>()
                .WithParameter("connectionString", _databaseConnectionString)
                .InstancePerLifetimeScope();

            //  Repository’s lifetime should usually be set as scoped

            builder.RegisterInstance(_configuration).As<IConfiguration>();

            builder
                .RegisterType<FibonacciRepository>()
                .As<IFibonacciRepository>()
                .SingleInstance();

            builder
                .RegisterType<ResultsRepository>()
                .As<ResultsRepository>()
                .SingleInstance();
        }
    }
}

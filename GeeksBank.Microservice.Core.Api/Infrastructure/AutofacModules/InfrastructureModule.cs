using Autofac;
using Microsoft.ApplicationInsights.Extensibility.Implementation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using GeeksBank.Microservice.Core.Domain.AggregatesModel;
using GeeksBank.Microservice.Core.Infrastructure.Models;
using GeeksBank.Microservice.Core.Infrastructure.Repository;
using Serilog;
using Serilog.Core;
using GeeksBank.Microservice.Core.Infrastructure;
using GeeksBank.Microservice.Core.Domain.AggregatesModel.ResultAggregate;
using GeeksBank.Microservice.Core.Domain.AggregatesModel.FibonacciAggregate;

namespace GeeksBank.Microservice.Core.Api.Infrastructure.AutofacModules
{
    /// <summary>
    /// Register all infrastructure related objects
    /// </summary>
    public class InfrastructureModule : Module
    {
        private readonly IConfiguration _configuration; 
        
        public InfrastructureModule(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ResultsContext>()
                .As<ResultsContext>()
                .InstancePerLifetimeScope();
            
            builder.RegisterType<ResultsRepository>()
                .As<IResultsRepository>()
                .InstancePerLifetimeScope();
            
            builder.RegisterType<FibonacciRepository>()
                .As<IFibonacciRepository>()
                .InstancePerLifetimeScope();
            
            builder.RegisterInstance(_configuration).As<IConfiguration>();
         
            builder.RegisterType<Logger>().As<ILogger>().InstancePerLifetimeScope();
        }
    }
}

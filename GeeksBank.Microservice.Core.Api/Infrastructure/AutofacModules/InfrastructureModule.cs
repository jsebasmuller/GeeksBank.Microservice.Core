using Autofac;
using Microsoft.ApplicationInsights.Extensibility.Implementation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using GeeksBank.Microservice.Core.Domain.AggregatesModel;

using GeeksBank.Microservice.Core.Domain.AggregatesModel.CoreAggregate;
using GeeksBank.Microservice.Core.Infrastructure.Models;
using GeeksBank.Microservice.Core.Infrastructure.Remote;
using GeeksBank.Microservice.Core.Infrastructure.Repository;
using Serilog;
using Serilog.Core;

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
            builder.RegisterType<CoreContext>()
                .As<CoreContext>()
                .InstancePerLifetimeScope();
            
            builder.RegisterType<CoreRepository>()
                .As<ICoreRepository>()
                .InstancePerLifetimeScope();
            
            builder.RegisterType<AccountRepository>()
                .As<IAccountRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<CoreManager<ApplicationCore>>()
                .As<CoreManager<ApplicationCore>>()
                .InstancePerLifetimeScope();
            
            builder.RegisterType<SignInManager<ApplicationCore>>()
                .As<SignInManager<ApplicationCore>>()
                .InstancePerLifetimeScope();
            
            builder.RegisterType<ThirdCoreRemoteClient>()
                .As<ICoreRemoteFinder>()
                .SingleInstance();
            
            builder.RegisterType<CatalogRemoteClient>()
                .As<ICatalogRemoteFinder>()
                .SingleInstance();
            
            builder.RegisterType<LoginRepositoryClient>()
                .As<ILoginRemoteClient>()
                .SingleInstance();
            
            builder.RegisterInstance(_configuration).As<IConfiguration>();
         
            builder.RegisterType<Logger>().As<ILogger>().InstancePerLifetimeScope();
        }
    }
}

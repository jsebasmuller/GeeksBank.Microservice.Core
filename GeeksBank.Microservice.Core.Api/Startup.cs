using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text.Json;
using System.Text.RegularExpressions;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using FluentValidation;
using IdentityServer4;
using IdentityServer4.Models;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using GeeksBank.Microservice.Core.Api.Infrastructure.Extensions;
using GeeksBank.Microservice.Core.Domain.AggregatesModel.IntegrationAggregate;
using GeeksBank.Microservice.Core.Domain.AggregatesModel.ResultAggregate;
using GeeksBank.Microservice.Core.Domain.Exception;
using GeeksBank.Microservice.Core.Infrastructure;
using GeeksBank.Microservice.Core.Infrastructure.Models;
using GeeksBank.Microservice.Core.Api.Constants;
using GeeksBank.Microservice.Core.Api.Infrastructure.AutofacModules;
using GeeksBank.Microservice.Core.Api.SeedWork;
using Serilog;
using Serilog.Exceptions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;

namespace GeeksBank.Microservice.Core.Api
{
    public class Startup
    {
        /// <summary>
        /// Startup Main Classs
        /// </summary>
        /// <param name="env"></param>
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .Enrich.WithExceptionDetails()
                .Filter.ByExcluding("health")
                .CreateLogger();
            Log.Information("Starting up" + env.EnvironmentName);
        }
        private readonly IConfiguration _configuration;
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            services.AddApplicationInsightsTelemetry();
            services.AddControllers();
            services.AddCustomMvc(_configuration);
            services.AddHealthChecks();
            services.AddCustomIntegrations();
            services.AddMediatR(typeof(Startup).GetTypeInfo().Assembly);
            services.AddHttpContextAccessor();
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
            services.AddValidatorsFromAssemblyContaining(typeof(Startup));

            services.AddTransient<CoreManager<ApplicationCore>>();
            services.AddTransient<SignInManager<ApplicationCore>>();
            
            var activationAttempts = _configuration.GetSection("NotificationActivateConfig");
            services.Configure<NotificationActivateConfig>(activationAttempts);
            services.AddScoped<ITokenService, TokenService>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => {
                options.TokenValidationParameters = new TokenValidationParameters{
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration.GetSection("Token").Value)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });
            // Password policy
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 8;
                options.Password.RequiredUniqueChars = 1;
            });
            
            // Auth
            /*
            var catalogSubsSettings = _configuration.GetSection("IntegrationEndpoints:CatalogSubscriptions");
            services.Configure<EndpointSettings>(catalogSubsSettings);
            */
            var integrationEndpoint = "IntegrationEndpoints";
            var endpointConfiguration = _configuration.GetSection(integrationEndpoint);
            EndpointConfiguration endpointsConfig = new EndpointConfiguration();
            foreach (var config in endpointConfiguration.GetChildren())
            {
                EndpointSettings settings = new EndpointSettings();
                settings.Name = config.Key;
                var key = integrationEndpoint +":"+ config.Key;
                IConfigurationSection section = _configuration.GetSection(key);
                section.Bind(settings);
                endpointsConfig.AddConfiguration(settings);
            }

            services.AddSingleton<IConfigurationEndpoints>(endpointsConfig);

            
            // Configure Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "GeeksBank.Microservice.Core API", Version = "v1"});
                //First we define the security scheme

                c.AddSecurityDefinition("Bearer", //Name the security scheme
                    new OpenApiSecurityScheme
                    {
                        Description = "JWT Authorization header using the Bearer scheme.",
                        Type = SecuritySchemeType.Http, //We set the scheme type to http since we're using bearer authentication
                        Scheme = "bearer" //The name of the HTTP Authorization scheme to be used in the Authorization header. In this case "bearer".
                    });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement{
                    {
                        new OpenApiSecurityScheme{
                            Reference = new OpenApiReference{
                                Id = "Bearer", //The name of the previously defined security scheme.
                                Type = ReferenceType.SecurityScheme
                            }
                        },new List<string>()
                    }
                });
                
                c.CustomSchemaIds(x => x.ToString());

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
            
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            Log.Information("STRING DB "+connectionString);
            var builder = services.AddIdentityServer(options =>
                {
                    options.Events.RaiseErrorEvents = true;
                    options.Events.RaiseInformationEvents = true;
                    options.Events.RaiseFailureEvents = true;
                    options.Events.RaiseSuccessEvents = true;
                })
                .AddAspNetIdentity<ApplicationCore>()
                .AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = options => options.UseMySQL(connectionString,
                        sql => sql.MigrationsAssembly(migrationsAssembly));
                })
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = b => b.UseMySQL(connectionString,
                        sql => sql.MigrationsAssembly(migrationsAssembly));
                });
            var rsa = RSA.Create();
            rsa.FromXmlString("<RSAKeyValue><Modulus>slznBBv1/y6bQa6zrbtiMytsSbZKhnsKyrKkE4Lk3E/G4khR1mMzm8Fsrqb6jQY83zjI1JgGkBkAEF2wmSROUuTFjrVed8rNfixFo/zsWIt4Ev5ydCeHGLL9Gwl1Fdnim3dflhQpzEns2LZh5wHcVhyYbf6B+267CO3Mgihx4GEXEE5YJVDqBEGTAGin6rc6e51jMdfiz/FrcRvDGr9daOKIhlXSWK9zXjX9E3fhF5wDJu28xAT0oqKVnLdqTewlY0MKLEmP22Yh4FXPgITrwTSu0Mk87G7yVgnhjDZf1jV2UcMqt7wsqp5ydu28PpQVbVm3KpdrEUvucLCunBqx0Q==</Modulus><Exponent>AQAB</Exponent><P>1a2SBk/6H3+74k2F7SQD9YUixHjb5+wLCgDYKGtUEuLusj007R0ghwnLhSFhsVK2VBBtWsBx7tFNVVzMPn89ANsuQ2M5rnYP5sGDyg+IWu5lknxg4AbajNIxR4sN2y3qt68AQoucsw0lrtcJrJG91QNJwb6eQRm+Bvbiuf/DcPc=</P><Q>1bCyvQerRnYOoix+aZBQ0+oVwtgyEtfYf094mwmUK69MSBOE6e6c25uCLi1lHmjUYUrAfQoJ/10Ev99FZV5Jp6UqspiNg/b98twSlOgOMH6dkGHOQyxJctet6c0ZsN2qqGyv02muSCKi9aqqnTN4RgMoF5t+HZgsv+xm6o9biXc=</Q><DP>ye/vQOf0ijA9b/Gz0BlpZG8eHG/b46LADAQgRJKqMe8lhm0xx6TvSK+JF5gkq2Bvz6J2tn2JLxm+7B13KNk23chGQIlVyfrprDrWQe/L/aOvenDxXMcdZFiBGgvgXHNYj59jr4Ah51VVd5biHaTesqEY43EyPnQFkq9gNkfwfKE=</DP><DQ>t6Mh91Cf3+2UpcmW2SEsVPDVwpwIbCkR5FZnTtTsgJ4k78vWbTKhmhgJx6U7QObVnMagpNP7w3gsnLdC69obcfZ+uvxeoQmhMxPs5AqIJySoQ2vJ1fA5Lngq/MFFUrkr75F3iDdJjrQ1VulTtcSFl6UezyrRNp2P119REkIgaAE=</DQ><InverseQ>HOIKEZt57lwz6QTvrLsXech6TUYxIe302CjsEzljwslIOQIH1oFLKpLlX14Nk1p52VMWPbEvshrULL9E/kG62EayNybfgXdqeCZWNJj9NG2tPKIf02FQiJRVTBi97rna8xmvHinn57B6OqfLeWcgPPEFGDcY8uOPkWBCUgtw4q8=</InverseQ><D>ZESJseuuDLg8m14EsEPI3o8onv+VQahl1rE6P6Wz1o4adhbFusmlt4ey+zPvYdwB2FLpw2l7NwJ24LxqjIy2Yy8sSB95bcpaXvWwaJHEo7oz1CmqWdXmwmHMm6hjY9dK5q7i01GedbORK/rLarvHC7mjjyImHByRlGFqODlYWxQJTNrKBhzHbgJL7gzzAqmwd5EzcljO1Afx+RFlSet28KDc9xIq4YSrFeevoJ0R7znjDAliRU1cjYK7yJkoxwKmhEGm++Ytc9y5MpzAPJoVGP+JduZeU6XoahTNXVaAhUYddmSfpPdRI4aR86tJySSlobfzIEIsIGpTe9BS/B19aQ==</D></RSAKeyValue>");

            //var str = rsa.ToXmlString(true);
            RsaSecurityKey asymmetricSecurityKey = new RsaSecurityKey(rsa);

            builder
                .AddValidationKey(new SecurityKeyInfo { Key = asymmetricSecurityKey, SigningAlgorithm = SecurityAlgorithms.RsaSha256 })
                .AddSigningCredential(asymmetricSecurityKey, IdentityServerConstants.RsaSigningAlgorithm.RS256);

            //configure autofac
            var container = new ContainerBuilder();
            container.Populate(services);
            ConfigureModelBindingExceptionHandling(services);
        }
        
        private void ConfigureModelBindingExceptionHandling(IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    ValidationProblemDetails error = actionContext.ModelState
                        .Where(e => e.Value.Errors.Count > 0)
                        .Select(e => new ValidationProblemDetails(actionContext.ModelState)).FirstOrDefault();
                    var errorField = error.Errors.Last().Key;
                    var field = Regex.Replace(errorField, "[^A-Za-z0-9_ ]", "");
                    var errorValue = error.Errors.Values.Last()[0];
                    Match match = Regex.Match(errorValue, ".+?(?=Path)");
                    var message = match.Success ? match.Value : errorValue;
                    throw new BadRequestException("invalid_type", $"Tipo de dato inválido: {field}.", message);
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            // Si se necesita Https, agregar en la siguiente linea el certificado
            // app.UseHttpsRedirection();
            app.UseRouting();
            app.UseSerilogRequestLogging(opts => {
                opts.EnrichDiagnosticContext = LogHelper.EnrichFromRequest;
                opts.GetLevel = LogHelper.ExcludeHealthChecks; // Use the custom level
            });
            
            app.ConfigureExceptionHandler(_configuration);
            //app.UseIdentityServer();
            app.UseAuthorization();
            app.UseHttpsRedirection();
            //UseIdentityServer includes a call to UseAuthentication, so it’s not necessary to have both
            app.UseAuthentication();
            
            app.UseCors("CorsPolicy");
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health");
            });
            
            app.UseCookiePolicy(new CookiePolicyOptions()
            {
                MinimumSameSitePolicy = SameSiteMode.Lax
            });
            
            // Use swagger Doc
            app.UseSwagger(c=> {
                c.RouteTemplate = $"{CoreConstants.ContextPath}swagger/{{documentName}}/swagger.json";
            });
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"v1/swagger.json", "GeeksBank.Microservice.Core API");
                c.RoutePrefix = $"{CoreConstants.ContextPath}swagger";
            });
            
            app.ApplicationServices.GetService<CoresContext>();

            //var scope = app.ApplicationServices.CreateScope();
            //var configDbContext = scope.ServiceProvider.GetService<ConfigurationDbContext>();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new InfrastructureModule(_configuration));
            builder.RegisterModule(new MediatorModule());
            //builder.RegisterType<RoleStore>().InstancePerLifetimeScope();
            //builder.Register<ApplicationCore>(c => HttpContext.Current.GetOwinContext().GetCoreManager).As<ApplicationCoreManager>();
        }
    }

    static class CustomExtensionsMethods
    {
        public static IServiceCollection AddCustomIntegrations(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            //services.AddScoped<CoresContext>();

            return services;
        }

        public static IServiceCollection AddCustomMvc(this IServiceCollection services, IConfiguration configuration)
        {
            
            services.AddScoped<CoresContext>();
            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<CoresContext>(options =>
            {
                options.UseMySQL(connectionString, x => x
                    .MigrationsAssembly(migrationsAssembly)
                );
            });
            
            services.AddIdentity<ApplicationCore, IdentityRole>()
                .AddEntityFrameworkStores<CoresContext>()
                .AddDefaultTokenProviders();
            
            // Add framework services.
            services
                .AddMvc()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.IgnoreNullValues = true;
                    options.JsonSerializerOptions.DictionaryKeyPolicy =     SnakeCaseNamingPolicy.Instance;
                    options.JsonSerializerOptions.PropertyNamingPolicy =    SnakeCaseNamingPolicy.Instance;
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                .AddControllersAsServices();
            
            
            services.AddLocalization();
            
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder =>
                        builder
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .AllowAnyOrigin()
                        );
            });

            return services;
        }
    }
    
    /// <summary>
    ///  Configure Snake case default json response in APIs
    /// </summary>
    public class SnakeCaseNamingPolicy : JsonNamingPolicy
    {
        public static SnakeCaseNamingPolicy Instance { get; } = new SnakeCaseNamingPolicy();

        public override string ConvertName(string name)
        {
            // Conversion to other naming conventaion goes here. Like SnakeCase, KebabCase etc.
            return name.ToSnakeCase();
        }
    }
    
    /// <summary>
    /// Util Extensions to write fields in json response (snakeCase)
    /// </summary>
    public static class StringUtils
    {
        public static string ToSnakeCase(this string str)
        {
            return string.Concat(str.Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x : x.ToString())).ToLower();
        }
    }
}

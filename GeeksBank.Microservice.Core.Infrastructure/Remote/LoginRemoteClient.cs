using System;
using System.Net;
using System.Threading.Tasks;
using Flurl.Http;
using GeeksBank.Microservice.Core.Domain.AggregatesModel.IntegrationAggregate;
using GeeksBank.Microservice.Core.Domain.Exception;
using Serilog;

namespace GeeksBank.Microservice.Core.Infrastructure.Remote
{
    public interface ILoginRemoteClient
    {
        Task<string> RequestJWT(string core, string password, string clientId, string clientSecret);
        Task<string> SendEmailPasswordReset(string name, string email, string password);
        Task<string> SendConfirmAccountEmail(string email, string token);
    }
    
    public class LoginRepositoryClient: ILoginRemoteClient
    {
        private readonly IConfigurationEndpoints _configuration;
        private readonly ILogger _logger = Log.ForContext<LoginRepositoryClient>();
        
        public LoginRepositoryClient(IConfigurationEndpoints configuration)
        {
            _configuration = configuration;
        }
        public async Task<string> RequestJWT(string core, string password, string clientId, string clientSecret)
        {
            var auth = _configuration.GetConfiguration("Authentication");
            var response = await $"{auth.BaseUrl}{auth.Path}".PostUrlEncodedAsync(new
            {
                client_id = clientId,
                client_secret = clientSecret,
                grant_type = "password",
                scope = "afiansa.api",
                corename = core,
                password = password
            });
            var stringContent = response.Content.ReadAsStringAsync();
            if (response.StatusCode != HttpStatusCode.OK)
            {
                // Código 7 error InvalidEmailPasswd
                _logger.Error(response.StatusCode.ToString(),stringContent.Result);
                throw new BadRequestException("7","Invalid Email or Password");
            }
            return stringContent.Result;
        }

        public async Task<string> SendEmailPasswordReset(string name, string email, string password)
        {
            var auth = _configuration.GetConfiguration("NotificationRecoveryPassword");
            var response = await $"{auth.BaseUrl}{auth.Path}".PostJsonAsync(new { 
                name = name,
                token = password,
                email = email
            });
            return "Se envio contraseña temporal al email ingresado.";

        }

        public async Task<string> SendConfirmAccountEmail(string email, string token) { 
            var auth = _configuration.GetConfiguration("ConfirmAccountEmail");
            var response = await $"{auth.BaseUrl}{auth.Path}".PostJsonAsync(new {
                email = email,
                token = token
            });
            return "Se envio email de confirmación";
        }
    }
}
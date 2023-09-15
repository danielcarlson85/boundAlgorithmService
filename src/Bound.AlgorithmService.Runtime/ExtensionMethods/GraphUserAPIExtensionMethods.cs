using Bound.AlgorithmService.Abstractions.Interfaces;
using Bound.AlgorithmService.Abstractions.Models.Settings;
using Bound.AlgorithmService.Managers.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Graph;
using Microsoft.Graph.Auth;
using Microsoft.Identity.Client;

namespace Bound.AlgorithmService.Runtime.ExtensionMethods
{
    public static class GraphUserAPI
    {
        public static void AddGraphUserAPI(this IServiceCollection services, IConfiguration configuration)
        {
            var adGraphSettings = configuration.GetSection("ADGraphSettings").Get<ADGraphSettings>();

            IConfidentialClientApplication confidentialClientApplication = ConfidentialClientApplicationBuilder
                .Create(adGraphSettings.GraphAppId)
                .WithTenantId(adGraphSettings.TenantId)
                .WithClientSecret(adGraphSettings.GraphClientSecret)
                .Build();

            ClientCredentialProvider authProvider = new(confidentialClientApplication);

            services.AddScoped<IUserService, UserService>(_ => new UserService(new GraphServiceClient(authProvider), adGraphSettings));
        }
    }
}
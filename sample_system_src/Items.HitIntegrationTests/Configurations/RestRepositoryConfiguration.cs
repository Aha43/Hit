using Hit.Infrastructure.Attributes;
using Hit.Infrastructure.User;
using Items.Infrastructure.Repository.Rest;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Items.HitIntegrationTests.Configurations
{
    [SysCon(name: "rest_consuming_repository_test", Description = "Testing CRUD with Rest")]
    public class RestRepositoryConfiguration : SystemConfigurationAdapter<ItemCrudWorld>
    {
        private readonly string _uri = "https://localhost:44356/";

        public override IServiceCollection ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            return services.ConfigureRestRepositoryServices(_uri);
        }

        public override async Task<bool> AvailableAsync()
        {
            try
            {
                using var httpClient = new HttpClient();
                var response = await httpClient.GetAsync(_uri + "api/info");
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }

}

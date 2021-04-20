using Hit.Infrastructure.Attributes;
using Hit.Infrastructure.User;
using Items.Infrastructure.Repository.Rest;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Items.HitIntegrationTests.Configurations
{
    [SysCon(name: "rest_consuming_repository_test", Description = "Testing CRUD with Rest")]
    public class RestRepositoryConfiguration : SystemConfigurationAdapter<ItemCrudWorld>
    {
        public override IServiceCollection ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            return services.ConfigureRestRepositoryServices("https://localhost:44356/");
        }
    }

}

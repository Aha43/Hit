using Hit.Infrastructure.Attributes;
using Hit.Infrastructure.User;
using Items.Infrastructure.Repository.InMemory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Items.HitIntegrationTests.Configurations
{
    [SysCon(name: "in_memory_repository_test", Description = "Testing CRUD with database repository")]
    public class InMemoryRepositoryConfiguration : SystemConfigurationAdapter<ItemCrudWorld>
    {
        public override IServiceCollection ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            return services.ConfigureInMemoryRepositoryServices(); ;
        }

    }

}

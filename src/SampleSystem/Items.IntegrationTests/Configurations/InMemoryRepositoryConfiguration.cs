using Hit.Infrastructure.Attributes;
using Hit.Infrastructure.User;
using Items.Infrastructure.Repository.InMemory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Items.HitIntegrationTests.Configurations
{
    [SysCon("in_memory_repository_test", Description = "Testing CRUD with in-memory repository")]
    public class InMemoryRepositoryConfiguration : SystemConfigurationAdapter<ItemCrudWorld>
    {
        public override IServiceCollection ConfigureServices(IServiceCollection services, IConfiguration configuration, SysCon meta, string currentLayer)
        {
            return services.ConfigureInMemoryRepositoryServices(); ;
        }

    }

}

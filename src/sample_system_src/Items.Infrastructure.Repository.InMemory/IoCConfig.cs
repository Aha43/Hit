using Items.Specification;
using Microsoft.Extensions.DependencyInjection;

namespace Items.Infrastructure.Repository.InMemory
{
    public static class IoCConfig
    {
        public static IServiceCollection ConfigureInMemoryRepositoryServices(this IServiceCollection services)
        {
            return services.AddSingleton<IItemsRepository, ItemsRepository>();
        }
    }
}

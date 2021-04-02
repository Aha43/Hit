using Items.Specification;
using Microsoft.Extensions.DependencyInjection;

namespace Items.Infrastructure.Repository.Rest
{
    public static class IoCConfig
    {
        public static IServiceCollection ConfigureRestRepositoryServices(this IServiceCollection services, string apiUri)
        {
            return services.AddSingleton<IItemsRepository, ItemRepository>()
                .AddSingleton(new ApiUri { Uri = apiUri });
        }
    }
}

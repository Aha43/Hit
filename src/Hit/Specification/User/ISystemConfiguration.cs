using Hit.Specification.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hit.Specification.User
{
    public interface ISystemConfiguration<World> : IHitType<World>
    {
        IConfiguration GetConfiguration();
        IServiceCollection ConfigureServices(IServiceCollection services, IConfiguration configuration);
    }
}

using Hit.Specification.User;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hit.Infrastructure.User
{
    public abstract class SystemConfigurationAdapter<World> : ISystemConfiguration<World>
    {
        public virtual IServiceCollection ConfigureServices(IServiceCollection services, IConfiguration configuration) => services;
        public virtual IConfiguration GetConfiguration() => null;
    }
}

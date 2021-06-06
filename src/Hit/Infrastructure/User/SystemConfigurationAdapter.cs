using Hit.Infrastructure.Attributes;
using Hit.Specification.User;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace Hit.Infrastructure.User
{
    public abstract class SystemConfigurationAdapter<World> : ISystemConfiguration<World>
    {
        public virtual Task<bool> AvailableAsync() => Task.FromResult(true);
        public virtual IServiceCollection ConfigureServices(IServiceCollection services, IConfiguration configuration, SysCon meta, string currentLayer) => services;
        public virtual IConfiguration GetConfiguration(SysCon meta) => null;
    }
}
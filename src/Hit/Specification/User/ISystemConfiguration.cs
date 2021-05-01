using Hit.Infrastructure.Attributes;
using Hit.Specification.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace Hit.Specification.User
{
    public interface ISystemConfiguration<World> : IHitType<World>
    {
        IConfiguration GetConfiguration(SysCon meta);
        IServiceCollection ConfigureServices(IServiceCollection services, IConfiguration configuration, SysCon meta, string currentLayer);
        Task<bool> AvailableAsync();
    }
}

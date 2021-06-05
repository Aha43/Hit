using Hit.Infrastructure.Attributes;
using Hit.Infrastructure.User;
using HitUnitTests.Worlds;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HitUnitTests.Configurations
{
    [SysCon("system-with-configuration-no-section-1", JsonPath = "configuration-no-section-1.json")]
    [SysCon("system-with-configuration-no-section-2", JsonPath = "configuration-no-section-2.json")]
    public class SystemConfigurationForReading_ConfigurationNoSection : DefaultSystemConfigurationAdapter<World4>
    {
        public override IServiceCollection ConfigureServices(IServiceCollection services, IConfiguration configuration, SysCon meta, string currentLayer)
        {
            var section = configuration.GetSection("ConfSetting");
            var confSetting = section.Get<ConfSetting>();
            return services.AddSingleton(confSetting);
        }

    }

}

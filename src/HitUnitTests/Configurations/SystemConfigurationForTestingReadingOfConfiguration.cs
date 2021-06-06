using Hit.Infrastructure.Attributes;
using Hit.Infrastructure.User;
using HitUnitTests.Worlds;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HitUnitTests.Configurations
{
    [SysCon("system-with-configuration-no-sections-1", JsonPath = "configuration-no-sections-1.json")]
    [SysCon("system-with-configuration-no-sections-2", JsonPath = "configuration-no-sections-2.json")]
    [SysCon("system-with-configuration-sections-part1", JsonPath = "configuration-sections.json", ConfigurationSections = "Part1")]
    [SysCon("system-with-configuration-sections-part2", JsonPath = "configuration-sections.json", ConfigurationSections = "Part2")]
    [SysCon("system-with-configuration-user-secret", UserSecrets = true)]
    [SysCon("system-with-configuration-user-secret-part1", UserSecrets = true, ConfigurationSections = "Part1")]
    [SysCon("system-with-configuration-user-secret-part2", UserSecrets = true, ConfigurationSections = "Part2")]
    public class SystemConfigurationForTestingReadingOfConfiguration : DefaultSystemConfigurationAdapter<World4>
    {
        public override IServiceCollection ConfigureServices(IServiceCollection services, IConfiguration configuration, SysCon meta, string currentLayer)
        {
            var section = configuration.GetSection("ConfSetting");
            var confSetting = section.Get<ConfSetting>();
            if (confSetting != null)
            {
                services.AddSingleton(confSetting);
            }

            return services;
        }

    }

}

using Hit.Infrastructure.Attributes;
using Hit.Infrastructure.User;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HitUnitTests.Configurations
{
    [SysCon("system-with-configuration-user-secret", UserSecrets = true)]
    [SysCon("system-with-configuration-user-secret-part1", UserSecrets = true, ConfigurationSections = "Part1")]
    [SysCon("system-with-configuration-user-secret-part2", UserSecrets = true, ConfigurationSections = "Part2")]
    public class SystemConfigurationForTestingReadingOfConfigurationUserSecrets1 : DefaultSystemConfigurationAdapter<World5>
    {
        public override IServiceCollection ConfigureServices(IServiceCollection services, IConfiguration configuration, SysCon meta, string currentLayer)
        {
            var section = configuration.GetSection("ConfSetting1");
            var confSetting = section.Get<ConfSetting1>();
            if (confSetting != null)
            {
                services.AddSingleton(confSetting);
            }

            return services;
        }

    }

    [SysCon("system-with-configuration-sections-file-and-user", UserSecrets = true, JsonPath = "configuration-sections-2.json", ConfigurationSections = "Part2, Part3")]
    public class SystemConfigurationForTestingReadingOfConfigurationUserSecrets2 : DefaultSystemConfigurationAdapter<World7>
    {
        public override IServiceCollection ConfigureServices(IServiceCollection services, IConfiguration configuration, SysCon meta, string currentLayer)
        {
            var section2 = configuration.GetSection("ConfSetting2");
            var confSetting2 = section2.Get<ConfSetting2>();
            services.AddSingleton(confSetting2);

            var section3 = configuration.GetSection("ConfSetting3");
            var confSetting3 = section3.Get<ConfSetting3>();
            services.AddSingleton(confSetting3);

            return services;
        }

    }

}

using Hit.Infrastructure.Attributes;
using Hit.Infrastructure.User;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HitUnitTests.Configurations
{
    [SysCon("system-with-configuration-no-sections-1", JsonPath = "configuration-no-sections-1.json")]
    [SysCon("system-with-configuration-no-sections-2", JsonPath = "configuration-no-sections-2.json")]
    [SysCon("system-with-configuration-sections-part1", JsonPath = "configuration-sections-1.json", ConfigurationSections = "Part1")]
    [SysCon("system-with-configuration-sections-part2", JsonPath = "configuration-sections-1.json", ConfigurationSections = "Part2")]
    public class SystemConfigurationForTestingReadingOfConfiguration1 : DefaultSystemConfigurationAdapter<World4>
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

    [SysCon("system-with-configuration-sections-multi", JsonPath = "configuration-sections-2.json", ConfigurationSections = "Part1, Part2")]
    public class SystemConfigurationForTestingReadingOfConfiguration2 : DefaultSystemConfigurationAdapter<World6>
    {
        public override IServiceCollection ConfigureServices(IServiceCollection services, IConfiguration configuration, SysCon meta, string currentLayer)
        {
            var section1 = configuration.GetSection("ConfSetting1");
            var confSetting1 = section1.Get<ConfSetting1>();
            services.AddSingleton(confSetting1);

            var section2 = configuration.GetSection("ConfSetting2");
            var confSetting2 = section2.Get<ConfSetting2>();
            services.AddSingleton(confSetting2);

            return services;
        }

    }

}

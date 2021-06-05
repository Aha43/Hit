using Hit.Infrastructure.Attributes;
using Hit.Infrastructure.User;
using HitUnitTests.Worlds;

namespace HitUnitTests.Configurations
{
    [SysCon("System2_1")]
    [SysCon("System2_2")]
    public class SystemConfiguration2 : SystemConfigurationAdapter<World2>
    {
    }
}

using Hit.Infrastructure.Attributes;
using Hit.Infrastructure.User;
using HitUnitTests.Worlds;

namespace HitUnitTests.Configurations
{
    [SysCon("System1")]
    public class SystemConfiguration1 : SystemConfigurationAdapter<World1>
    {
    }
}

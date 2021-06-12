using Hit.Infrastructure.Attributes;
using Hit.Infrastructure.User;

namespace HitUnitTests.Configurations
{
    [SysCon("System3_1", Layers = "Layer2, Layer3")]
    [SysCon("System3_2", Layers = "Layer1")]
    public class SystemConfiguration3 : SystemConfigurationAdapter<World3>
    {
    }
}

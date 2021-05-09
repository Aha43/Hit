using Hit.Infrastructure.Attributes;
using Hit.Infrastructure.User;
using HitUnitTests.Worlds;

namespace HitUnitTests.Configurations
{
    [SysCon("System3_1", Layers = "Layer2, Layer3")]
    [SysCon("System3_2", Layers = "Layer1")]
    public class Configuration3 : SystemConfigurationAdapter<World3>
    {
    }
}

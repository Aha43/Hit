using Hit.Specification.User;

namespace HitUnitTests.Worlds
{
    public class World1Provider : IWorldProvider<World1>
    {
        public World1 Get() => new();
    }
}

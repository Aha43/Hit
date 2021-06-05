using Hit.Specification.User;

namespace HitUnitTests.Worlds
{
    public class World4 : IWorldProvider<World4>
    {
        public World4 Get() => new();
    }
}

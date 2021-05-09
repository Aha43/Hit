using Hit.Specification.User;

namespace HitUnitTests.Worlds
{
    public class World2Provider : IWorldProvider<World2>
    {
        public World2 Get() => new();
    }
}

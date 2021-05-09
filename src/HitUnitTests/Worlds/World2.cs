using Hit.Specification.User;

namespace HitUnitTests.Worlds
{
    public class World2 : IWorldProvider<World2>
    {
        public World2 Get() => new();
    }
}

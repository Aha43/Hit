using Hit.Specification.User;

namespace HitUnitTests.Worlds
{
    public class World5 : IWorldProvider<World5>
    {
        public World5 Get() => new();
    }
}

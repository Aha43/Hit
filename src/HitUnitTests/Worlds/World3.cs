using Hit.Specification.User;

namespace HitUnitTests.Worlds
{
    public class World3 : IWorldProvider<World3>
    {
        public World3 Get() => new();
    }
}

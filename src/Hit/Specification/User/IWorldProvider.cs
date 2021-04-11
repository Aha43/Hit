using Hit.Specification.Infrastructure;

namespace Hit.Specification.User
{
    public interface IWorldProvider<World> : IHitType<World>
    {
        World Get();
    }
}

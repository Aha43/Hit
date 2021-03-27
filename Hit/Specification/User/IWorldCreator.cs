namespace Hit.Specification.User
{
    public interface IWorldCreator<World> : IHitType<World>
    {
        World Create();
    }
}

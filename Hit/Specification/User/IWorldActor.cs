namespace Hit.Specification.User
{
    public interface IWorldActor<World> : IHitType<World>
    {
        void Act(World world);
    }
}

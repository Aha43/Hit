namespace Hit.Specification
{
    public interface IWorldActor<World> : IHitType<World>
    {
        void Act(World world);
    }
}

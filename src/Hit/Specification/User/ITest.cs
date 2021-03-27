namespace Hit.Specification.User
{
    public interface ITest<World> : IWorldActor<World>
    {
        IWorldActor<World> PreTestActor { get; }
        IWorldActor<World> PostTestActor { get; }
    }
}

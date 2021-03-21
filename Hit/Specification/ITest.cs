namespace Hit.Specification
{
    public interface ITest<IWorld> : IWorldActor<IWorld>
    {
        IWorldActor<IWorld> PreTest { get; }
        IWorldActor<IWorld> PostTest { get; }
    }
}

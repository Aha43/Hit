namespace Hit.Specification
{
    public interface IWorldActor<IWorld>
    {
        void Accept(IWorld world);
    }
}

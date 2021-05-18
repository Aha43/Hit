namespace Hit.Specification.Infrastructure
{
    public interface IUnitTestSourceGenerator<World>
    {
        string GenerateCode(IUnitTestsSpace<World> space);
    }
}

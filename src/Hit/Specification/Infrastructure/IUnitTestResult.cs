namespace Hit.Specification.Infrastructure
{
    public interface IUnitTestResult
    {
        string UnitTestsDescription { get; }
        string System { get; }
        string Layer { get; }
        string UnitTest { get; }
        ITestResultNode ResultHead { get; }
        bool Success();
        bool SystemAvailable { get; }
    }
}

namespace Hit.Specification.Infrastructure
{
    public interface IUnitTestResult
    {
        string System { get; }
        string UnitTestsDescription { get; }
        string UnitTest { get; }
        ITestResultNode ResultHead { get; }
        bool Success();
        bool SystemAvailable { get; }
    }
}

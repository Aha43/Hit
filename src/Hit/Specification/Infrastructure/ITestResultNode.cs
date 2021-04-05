namespace Hit.Specification.Infrastructure
{
    public interface ITestResultNode
    {
        ITestResult TestResult { get; }
        ITestResultNode Next { get; }
    }
}

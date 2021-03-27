namespace Hit.Specification.Infrastructure
{
    public interface ITestResult
    {
        string TestName { get; }
        TestStatus Status { get; }
        ITestFailure Failure { get; }
    }
}

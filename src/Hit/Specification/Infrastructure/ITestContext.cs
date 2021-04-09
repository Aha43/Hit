namespace Hit.Specification.Infrastructure
{
    public interface ITestContext<TheWorld>
    {
        TheWorld World { get; }
        ITestOptions Options { get; }
        ITestResult TestResult { get; }
        string SuiteName { get; }
        string TestRunName { get; }
        string ParentTestName { get; }
        string TestName { get; }
    }
}

using System;

namespace Hit.Specification.Infrastructure
{
    public interface ITestContext<TheWorld>
    {
        TheWorld World { get; }
        ITestOptions Options { get; }
        ITestResult TestResult { get; }
        string UnitTestsName { get; }
        string UnitTest { get; }
        string ParentTestName { get; }
        string TestName { get; }
    }
}

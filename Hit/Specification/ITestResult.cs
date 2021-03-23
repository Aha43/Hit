using System.Collections.Generic;

namespace Hit.Specification
{
    public interface ITestResult
    {
        string TestName { get; }
        TestStatus Status { get; }
        ITestFailure Failure { get; }
    }
}

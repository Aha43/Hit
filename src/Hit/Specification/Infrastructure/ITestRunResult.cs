using System.Collections.Generic;

namespace Hit.Specification.Infrastructure
{
    public interface ITestRunResult
    {
        string SuiteName { get; }
        string SuiteDescription { get; }
        string RunName { get; }
        IEnumerable<ITestResultNode> Results { get; }
        bool Success();
    }
}

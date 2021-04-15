using System.Collections.Generic;

namespace Hit.Specification.Infrastructure
{
    public interface IUnitTestResult
    {
        string SuiteName { get; }
        string SuiteDescription { get; }
        string UnitTest { get; }
        IEnumerable<ITestResultNode> Results { get; }
        bool Success();
    }
}

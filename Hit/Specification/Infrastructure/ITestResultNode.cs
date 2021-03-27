using System.Collections.Generic;

namespace Hit.Specification.Infrastructure
{
    public interface ITestResultNode
    {
        ITestResult TestResult { get; }
        IEnumerable<ITestResultNode> Children { get; }
    }
}

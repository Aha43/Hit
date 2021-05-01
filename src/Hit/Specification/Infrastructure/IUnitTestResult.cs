using System.Collections.Generic;

namespace Hit.Specification.Infrastructure
{
    public interface IUnitTestResult
    {
        string System { get; }
        string UnitTestsDescription { get; }
        string UnitTest { get; }
        IEnumerable<ITestResultNode> Results { get; }
        bool Success();
        bool SystemAvailable { get; }
    }
}

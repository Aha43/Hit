using System;

namespace Hit.Specification.Infrastructure
{
    public interface ITestContext<TheWorld>
    {
        TheWorld World { get; }
        ITestOptions Options { get; }
        ITestResult TestResult { get; }
        string System { get; }
        string Layer { get; }
        string UnitTest { get; }
        string ParentTestName { get; }
        string TestName { get; }

        void Log(string msg);
    }
}
